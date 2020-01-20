using UnityEngine;
using UnityEngine.UI;

// 各メッセージ表示
public class MessageController : MonoBehaviour
{
    public static MessageController instance;
    [SerializeField]
    private GameObject message, cancel;
    [SerializeField]
    private Text mtext;
    private string mode;
    [SerializeField]
    private CriAtomSource notice, click;
    [SerializeField]
    private Image canImg;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        canImg.sprite = GameManager.instance.SetAtlas.GetSprite("CANCEL");
    }

    public void ShowMessage(string e)
    {
        message.SetActive(true);
        notice.Play();
        cancel.SetActive(false);
        mtext.text = e;
    }

    public void ShowMessage(string e, string m)
    {
        message.SetActive(true);
        cancel.SetActive(true);
        mtext.text = e;
        mode = m;
        notice.Play();
    }

    public void OKClick()
    {
        click.Play();
        message.SetActive(false);
        switch (mode)
        {
            case "CSBack":
                GameManager.instance.NextScene("CourseSelect");
                break;
            case "Retry":
                if (StaminaManager.staminaRP.Value == 0)
                {
                    CancelClick();
                    ShowMessage("スタミナが\nたりません！");
                    return;
                }
                else
                {
                    --StaminaManager.staminaRP.Value;
                    GameManager.instance.NextScene("GameScene");
                }
                break;
            case "stamina":
                AdsManager.instance.RewardShow(mode);
                break;
            case "tuto":
                Tutorial.instance.TutoStart();
                break;
        }
        mode = "";
    }

    public void CancelClick()
    {
        click.Play();
        mode = "";
        message.SetActive(false);
    }
}
