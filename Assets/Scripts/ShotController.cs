using System.Collections;
using UnityEngine;

// 各ショットの動作
public class ShotController : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 initPos;
    public Quaternion initRot;
    public ParticleSystem hit;
    public CriAtomSource se;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        se = gameObject.AddComponent<CriAtomSource>();
        se.cueSheet = "GameSE";
    }

    public void ShotReady()
    {
        rb.useGravity = false;
        GameManager.stateRP.Value = GameState.Fly;
        initRot = transform.localRotation;
    }

    // 前直線
    public IEnumerator StraightShot(int p, int a, int s)
    {
        LinerUp(p, a);
        yield return new WaitForSeconds(s);
    }

    // 横直線
    public IEnumerator SideShot(int p, int a, int s)
    {
        LinerSide(p, a);
        yield return new WaitForSeconds(s);
    }

    // 上曲げ
    public IEnumerator UpCorner(int p, int a, int s)
    {
        var sec = s / 2;
        LinerUp(p, 0);
        yield return new WaitForSeconds(sec);
        BallStop();
        LinerUp(p, a);
        yield return new WaitForSeconds(sec);
    }

    // 横曲げ
    public IEnumerator SideCorner(int p, int a, int s)
    {
        var sec = s / 2;
        LinerSide(p, 0);
        yield return new WaitForSeconds(sec);
        BallStop();
        LinerSide(p, a);
        yield return new WaitForSeconds(sec);
    }

    // 上スラローム
    public IEnumerator UpSlalom(int p, int a, int s)
    {
        var sec = s / 3;
        LinerUp(p, 0);
        yield return new WaitForSeconds(sec);
        BallStop();
        LinerUp(p, a);
        yield return new WaitForSeconds(sec);
        BallStop();
        LinerUp(p, 0);
        yield return new WaitForSeconds(sec);
    }

    // 横スラローム
    public IEnumerator SideSlalom(int p, int a, int s)
    {
        float sec = s / 3;
        LinerSide(p, 0);
        yield return new WaitForSeconds(sec);
        BallStop();
        LinerSide(p, a);
        yield return new WaitForSeconds(sec);
        BallStop();
        LinerSide(p, 0);
        yield return new WaitForSeconds(sec);
    }

    // 上弧
    public IEnumerator UpCurve(int p, int a, int s)
    {
        initRot = transform.localRotation;
        var targetPos = transform.position + (transform.forward * p);
        var rotTime = 0f;
        se.Play(10);
        hit.Play();
        while (rotTime <= s)
        {
            transform.RotateAround(targetPos, transform.right, (a * Time.deltaTime));
            rotTime += Time.deltaTime;
            yield return null;
        }
        transform.localRotation = initRot;
        if (RayCheck(initPos, transform.position)) GameManager.stateRP.Value = GameState.OB;
    }

    // 横弧
    public IEnumerator SideCurve(int p, int a, int s)
    {
        initRot = transform.localRotation;
        var targetPos = transform.position + (transform.right * p);
        if (a <= 0)
        {
            targetPos = transform.position + (transform.right * p * -1);
        }
        var rotTime = 0f;
        se.Play(10);
        hit.Play();
        while (rotTime <= s)
        {
            transform.RotateAround(targetPos, transform.up, (a * Time.deltaTime));
            rotTime += Time.deltaTime;
            yield return null;
        }
        transform.localRotation = initRot;
        if (RayCheck(initPos, transform.position)) GameManager.stateRP.Value = GameState.OB;
    }

    // ワープ
    public IEnumerator Warp(int p, int a, int s, float ymax)
    {
        var v = transform.position + transform.forward * p + transform.right * a + transform.up * s;
        while (RayCheck(transform.position, v))
        {
            v.y += 1;
            if (v.y > ymax) break;
        }
        yield return new WaitForSeconds(1);
        se.Play(10);
        hit.Play();
        transform.position = v;
        yield return new WaitForSeconds(1);
    }

    public void LinerUp(int p, int a)
    {
        var rot = p < 0 ? a : a *-1;
        transform.Rotate(transform.rotation.x + (rot), transform.rotation.y, transform.rotation.z);
        se.Play(10);
        hit.Play();
        rb.AddForce(transform.forward * p, ForceMode.Force);
    }

    public void LinerSide(int p, int a)
    {
        transform.Rotate(transform.rotation.x, transform.rotation.y + a, transform.rotation.z);
        se.Play(10);
        hit.Play();
        rb.AddForce(transform.forward * p, ForceMode.Force);
    }

    public void BallStop()
    {
        transform.localRotation = initRot;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;
    }

    public bool RayCheck(Vector3 start, Vector3 end)
    {
        var head = end - start;
        var dis = head.magnitude;
        var dir = head / dis;
        var itr = 0;
        var hit = Physics.RaycastAll(start, dir, dis);
        foreach (var item in hit)
        {
            if (item.collider.tag != "player" ||
                item.collider.tag != "hole" ||
                item.collider.tag != "Wall")
            {
                itr++;
            }
        }

        head = start - end;
        dir = head / dis;
        hit = Physics.RaycastAll(end, dir, dis);
        foreach (var item in hit)
        {
            if (item.collider.tag != "player" ||
                item.collider.tag != "hole" ||
                item.collider.tag != "Wall")
            {
                itr++;
            }
        }

        if (itr % 2 == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
