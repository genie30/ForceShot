using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class Tutorial : MonoBehaviour
{
    public static Tutorial instance;
    [SerializeField]
    private GameObject panel, image,prog, msg;
    [SerializeField]
    private RectTransform msgRect;
    [SerializeField]
    private Text text;
    [SerializeField]
    private Dropdown dropdown,drop2;
    private Image img;
    [SerializeField]
    private SpriteAtlas spAt;
    private bool move;
    [SerializeField]
    private Slider pow, ang, sec, pow2, ang2, sec2;

    private void Start()
    {
        instance = this;
        img = image.GetComponent<Image>();
        if (GameManager.instance.playerdata.exp != 0 && GameManager.instance.playerdata.level != 1)
        {
            Destroy(this);
        }
    }
    
    public void TutoStart()
    {
        StartCoroutine(TutoTask());
    }

    private IEnumerator TutoTask()
    {
        TextShow("まずプログラムボタンを\n押してください", 2);
        image.SetActive(true);
        image.transform.localPosition = new Vector3(-68.4f, -319.08f, 0f);
        while (!Input.GetMouseButton(0))
        {
            yield return null;
        }
        image.SetActive(false);
        TextClose();

        while (!prog.activeSelf)
        {
            yield return null;
        }
        TextShow("プルダウンから\nラインショット(上下)\nを選択してください", 3);
        image.SetActive(true);
        image.transform.localPosition = new Vector3(-68.4f, 211.2f, 0f);
        while (!Input.GetMouseButton(0))
        {
            yield return null;
        }
        image.SetActive(false);
        TextClose();

        while (dropdown.value != 1)
        {
            yield return null;
        }
        TextShow("スライダーをスワイプして\nパラメーターを設定できます", 2);
        image.SetActive(true);
        image.transform.localPosition = new Vector3(-53.5f, 168.9f, 0f);
        move = true;
        StartCoroutine(LRShift());
        while (!Input.GetMouseButton(0))
        {
            yield return null;
        }
        TextClose();
        while (!Input.GetMouseButtonUp(0))
        {
            yield return null;
        }
        TextShow("力を13、秒を5に\n設定してください", 2);
        while (!Input.GetMouseButton(0))
        {
            yield return null;
        }
        TextClose();
        move = false;

        while (pow.value != 13 || sec.value != 5 || ang.value != 0)
        {
            yield return null;
        }
        while (!Input.GetMouseButtonUp(0))
        {
            yield return null;
        }
        TextShow("セットボタンを押してください", 2);
        image.SetActive(true);
        image.transform.localPosition = new Vector3(-66.5f, -321.5f, 0);
        while (!Input.GetMouseButton(0))
        {
            yield return null;
        }
        TextClose();
        image.SetActive(false);

        while (prog.activeSelf)
        {
            yield return null;
        }
        TextShow("プログラムセットしてから\nショットボタンを押すと\nボールが動きます", 3);
        image.SetActive(true);
        image.transform.localPosition = new Vector3(103.5f, -321.5f, 0);
        while (!Input.GetMouseButton(0))
        {
            yield return null;
        }
        TextClose();
        while (!Input.GetMouseButtonUp(0))
        {
            yield return null;
        }
        TextShow("ボールは矢印方向に進みます\n何もない場所をタップすると\nタップした場所を向きます", 3);
        while (!Input.GetMouseButton(0))
        {
            yield return null;
        }
        TextClose();
        while (!Input.GetMouseButtonUp(0))
        {
            yield return null;
        }
        TextShow("いまはそのままの向きで\nショットボタンを押してください", 2);
        while (!Input.GetMouseButton(0))
        {
            yield return null;
        }
        TextClose();
        image.SetActive(false);

        while(GameManager.stateRP.Value != GameState.Fly)
        {
            yield return null;
        }
        while(GameManager.stateRP.Value != GameState.Stay)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);

        TextShow("次のプログラムを設定します", 1);
        image.SetActive(true);
        image.transform.localPosition = new Vector3(-68.4f, -319.08f, 0f);
        while (!Input.GetMouseButton(0))
        {
            yield return null;
        }
        image.SetActive(false);
        TextClose();

        while (!prog.activeSelf)
        {
            yield return null;
        }
        TextShow("プログラム1,2ともに\nラインショット(上下)\nを選択してください", 3);
        while (!Input.GetMouseButton(0))
        {
            yield return null;
        }
        TextClose();

        while (dropdown.value != 1 || drop2.value != 1)
        {
            yield return null;
        }
        while (!Input.GetMouseButtonUp(0))
        {
            yield return null;
        }
        TextShow("プログラム1で、力を20、\n角度を30、秒を5に\n設定してください", 3);
        while (!Input.GetMouseButton(0))
        {
            yield return null;
        }
        TextClose();

        while(pow.value != 20 || ang.value != 30 || sec.value != 5)
        {
            yield return null;
        }
        while (!Input.GetMouseButtonUp(0))
        {
            yield return null;
        }
        TextShow("プログラム2で、力を3、\n角度を-30、秒を3に\n設定してください", 3);
        while (!Input.GetMouseButton(0))
        {
            yield return null;
        }
        TextClose();

        while(pow2.value != 3 || ang2.value != -30 || sec2.value != 3)
        {
            yield return null;
        }
        while (!Input.GetMouseButtonUp(0))
        {
            yield return null;
        }
        TextShow("設定できたらセットして\n向きを変えずに\nショットしてみてください", 3);
        while (!Input.GetMouseButton(0))
        {
            yield return null;
        }
        TextClose();

        while (GameManager.stateRP.Value != GameState.Fly)
        {
            yield return null;
        }
        while (GameManager.stateRP.Value != GameState.Stay)
        {
            yield return null;
        }
        TextShow("最後のショットの力を\n弱くするとボールの転がりを\nおさえられます", 3);
        while (!Input.GetMouseButton(0))
        {
            yield return null;
        }
        TextClose();
        while (!Input.GetMouseButtonUp(0))
        {
            yield return null;
        }
        TextShow("あとはボールがホールにふれれば\nステージクリアです。\nレベルが上がればプログラム数や\nショットパターンが増えます。\n内容はヘルプでご確認ください。", 5);
        while (!Input.GetMouseButton(0))
        {
            yield return null;
        }
        TextClose();
    }

    private void TextShow(string txt, int line)
    {
        var height = line * 30 + 40;
        panel.SetActive(true);
        msgRect.sizeDelta = new Vector2(350, height);
        text.text = txt;
        msg.SetActive(true);
    }

    private void TextClose()
    {
        panel.SetActive(false);
        msg.SetActive(false);
    }

    private IEnumerator LRShift()
    {
        var defpos = image.transform.localPosition;
        while (move)
        {
            if (move)
            {
                img.sprite = spAt.GetSprite("LeftSwipe");
                for (var L = 0; L > -30; L--)
                {
                    yield return null;
                    image.transform.localPosition += new Vector3(-1f, 0, 0);
                }
                image.transform.localPosition = defpos;

                img.sprite = spAt.GetSprite("RightSwipe");
                for (var R = 0; R < 30; R++)
                {
                    yield return null;
                    image.transform.localPosition += new Vector3(1f, 0, 0);
                }
                image.transform.localPosition = defpos;
            }
            yield return null;
        }
        image.SetActive(false);
        img.sprite = spAt.GetSprite("Tap");
    }
}
