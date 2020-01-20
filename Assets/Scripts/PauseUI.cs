using UnityEngine;
using UnityEngine.UI;

// ゲーム中のポーズ画面操作
public class PauseUI : MonoBehaviour
{
    [SerializeField]
    private GameObject pause, main;
    [SerializeField]
    private Image title, rt, rtry, help, resume;

    private void Awake()
    {
        var sa = GameManager.instance.SetAtlas;
        title.sprite = sa.GetSprite("PAUSE");
        rt.sprite = sa.GetSprite("RTCourseSelect");
        rtry.sprite = sa.GetSprite("RETRY");
        help.sprite = sa.GetSprite("HELP");
        resume.sprite = sa.GetSprite("RESUME");
    }

    public void CSClick()
    {
        MessageController.instance.ShowMessage("使ったスタミナは\n戻りませんが\nコースセレクトに\n移動しますか？", "CSBack");
    }

    public void RetryClick()
    {
        MessageController.instance.ShowMessage("スタミナを使って\nこのコースを\nはじめから\nやりなおしますか？", "Retry");
    }
    
    public void HelpClick()
    {
        HelpController.instance.ShowHelp();
    }

    public void ResumeClick()
    {
        pause.SetActive(false);
        main.SetActive(true);
    }
}
