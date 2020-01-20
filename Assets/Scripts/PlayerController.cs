using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;

public class PlayerController : MonoBehaviour
{
    private ShotController sc;
    [SerializeField]
    private GameObject mainUI, arrow, result, mcam, scam, hole, obwh, ob, wh;
    [SerializeField]
    private ParticleSystem Hanabi;
    [SerializeField]
    private TrailRenderer trail;
    [Range(0, 4)]
    private int count;
    private float xmin, xmax, ymin, ymax, zmin, zmax;
 
    private Plane plane = new Plane();

    private void Awake()
    {
        sc = GetComponent<ShotController>();
        GameManager.instance.shotCount = 1;
    }

    private void Start()
    {
        StageSet();
        // ステート監視してショット向き変更処理
        this.UpdateAsObservable().Select(x => GameManager.stateRP.Value).Where(x => x == GameState.Stay || x == GameState.Ready)
            .Where(x => Input.GetMouseButtonDown(0)).Subscribe(_ => BallRotation()).AddTo(this);
        GameManager.stateRP.Value = GameState.Stay;
        // ステート変更監視
        GameManager.stateRP.DistinctUntilChanged().Subscribe(_ => StateChange()).AddTo(this);
        // 水に入ったかの監視　入ったら音を鳴らす
        this.OnCollisionEnterAsObservable().Skip(1).Where(x => x.collider.tag != "Water").Subscribe(_ => sc.se.Play(1)).AddTo(this);
        // 初回プレイ時チュートリアル表示
        if (GameManager.instance.playerdata.exp == 0 && GameManager.instance.playerdata.level == 1) MessageController.instance.ShowMessage("チュートリアルを\n始めますか？", "tuto");
    }

    private void StateChange()
    {
        switch (GameManager.stateRP.Value)
        {
            case GameState.Stay:
                BallStay();
                break;
            case GameState.Shot:
                BallShot();
                break;
            case GameState.HoleIn:
                HoleIN();
                break;
            case GameState.Clear:
                Observable.Return(Unit.Default).Delay(System.TimeSpan.FromSeconds(3)).Subscribe(_ => result.SetActive(true)).AddTo(this);
                break;
            case GameState.Water:
                sc.se.Play(13);
                WaterHazard();
                break;
            case GameState.OB:
                sc.se.Play(8);
                OB();
                break;
        }
    }

    private void StageSet()
    {
        var stage = GameManager.instance.setHole - 1;
        var playerarr = StageList.instance.playerList[stage];
        var holearr = StageList.instance.holeList[stage];
        var wallarr = StageList.instance.wallList[stage];
        var ball = gameObject.transform;
        var holet = hole.transform;
        var wall = GameObject.FindWithTag("Wall").transform;

        ball.position = new Vector3(playerarr[0], playerarr[1], playerarr[2]);
        holet.position = new Vector3(holearr[0], holearr[1], holearr[2]);
        holet.Rotate(0, holearr[3], 0);
        wall.position = new Vector3(wallarr[0], wallarr[1], wallarr[2]);
        wall.localScale = new Vector3(wallarr[3], wallarr[4], wallarr[5]);

        xmin = wall.position.x - (wall.localScale.x / 2);
        xmax = wall.position.x + (wall.localScale.x / 2);
        ymin = wall.position.y - (wall.localScale.y / 2);
        ymax = wall.position.y + (wall.localScale.y / 2);
        zmin = wall.position.z - (wall.localScale.z / 2);
        zmax = wall.position.z + (wall.localScale.z / 2);

        LookReset();
        trail.Clear();
        CameraController.instance.CameraReset();
    }

    private void LookReset()
    {
        var target = hole.transform.position;
        target = new Vector3(target.x, transform.position.y, target.z);
        transform.LookAt(target);
    }

    private void BallRotation()
    {
        var current = EventSystem.current;
        var eventData = new PointerEventData(current)
        {
            position = Input.mousePosition
        };
        var result = new List<RaycastResult>();
        current.RaycastAll(eventData, result);
        var isExists = 0 < result.Count;
        if (!isExists)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            plane.SetNormalAndPosition(Vector3.up, transform.localPosition);
            if (Input.GetMouseButton(0) && plane.Raycast(ray, out var distance))
            {
                var lookPoint = ray.GetPoint(distance);
                transform.LookAt(lookPoint);
            }
        }
    }

    // ボールの動作停止
    private void BallStay()
    {
        sc.BallStop();
        LookReset();
        trail.Clear();
        count = 0;
        sc.initPos = transform.position;
        mainUI.SetActive(true);
        arrow.SetActive(true);
    }

    // ショットプログラムを読み込みショット動作
    private void BallShot()
    {
        arrow.SetActive(false);
        sc.rb.useGravity = false;
        GameManager.stateRP.Value = GameState.Fly;

        var selector = GameManager.instance.shotProgram[count, 0];
        var pow = GameManager.instance.shotProgram[count, 1];
        var ang = GameManager.instance.shotProgram[count, 2];
        var sec = GameManager.instance.shotProgram[count, 3];
        IEnumerator coroutine = null;

        switch (selector)
        {
            case 1: //StraightShot
                coroutine = sc.StraightShot(pow, ang, sec);
                break;
            case 2: //SideShot
                coroutine = sc.SideShot(pow, ang, sec);
                break;
            case 3: //UpCorner
                coroutine = sc.UpCorner(pow, ang, sec);
                break;
            case 4: //SideCorner
                coroutine = sc.SideCorner(pow, ang, sec);
                break;
            case 5: //UpSlalom
                coroutine = sc.UpSlalom(pow, ang, sec);
                break;
            case 6: //SideSlalom
                coroutine = sc.SideSlalom(pow, ang, sec);
                break;
            case 7: //UpCurve
                coroutine = sc.UpCurve(pow, ang, sec);
                break;
            case 8: //SideCurve
                coroutine = sc.SideCurve(pow, ang, sec);
                break;
            case 9: //Warp
                coroutine = sc.Warp(pow, ang, sec, ymax);
                break;
        }
        sc.initRot = transform.localRotation;
        Observable.FromCoroutine(() => coroutine).Where(_ => GameManager.stateRP.Value == GameState.Fly).Subscribe(_ => ShotNext()).AddTo(this);
    }

    private void HoleIN()
    {
        sc.BallStop();
        scam.SetActive(true);
        mcam.SetActive(false);
        Hanabi.Play();
        sc.se.Play(4);
        GameManager.stateRP.Value = GameState.Clear;
    }

    private void WaterHazard()
    {
        sc.BallStop();
        obwh.SetActive(true);
        wh.SetActive(true);
        StartCoroutine(OBWHClose());
    }

    private void OB()
    {
        count = 6;
        sc.BallStop();
        obwh.SetActive(true);
        ob.SetActive(true);
        StartCoroutine(OBWHClose());
    }

    private IEnumerator OBWHClose()
    {
        GameManager.instance.shotCount += 2;
        yield return new WaitForSeconds(3);
        ob.SetActive(false);
        wh.SetActive(false);
        obwh.SetActive(false);
        transform.position = sc.initPos;
        GameManager.stateRP.Value = GameState.Stay;
    }

    // 次のショットプログラム動作
    private void ShotNext()
    {
        count++;
        var trp = transform.position;
        if (trp.x < xmin || trp.x > xmax || trp.y < ymin || trp.y > ymax || trp.z < zmin || trp.z > zmax)
        {
            GameManager.stateRP.Value = GameState.OB;
            return;
        }

        if (count < 5)
        {
            if (GameManager.instance.shotProgram[count, 0] != 0)
            {
                sc.BallStop();
                GameManager.stateRP.Value = GameState.Shot;
                return;
            }
        }
        var coroutine = ShotEnd();
        StartCoroutine(coroutine);
    }

    // プログラム終了時の動作
    private IEnumerator ShotEnd()
    {
        sc.rb.useGravity = true;
        yield return new WaitForSeconds(1);
        while (sc.rb.velocity.magnitude > 0.001f)
        {
            yield return new WaitForSeconds(1);
        }
        GameManager.instance.shotProgram = new int[5, 4];
        count = 0;
        var trp = transform.position;
        if (trp.x < xmin || trp.x > xmax || trp.y < ymin || trp.y > ymax || trp.z < zmin || trp.z > zmax)
        {
            GameManager.stateRP.Value = GameState.OB;
        }
        if (GameManager.stateRP.Value != GameState.Fly) yield break;
        GameManager.stateRP.Value = GameState.Stay;
        GameManager.instance.shotCount++;
    }
}
