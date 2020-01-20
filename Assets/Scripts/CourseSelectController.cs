using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.Advertisements;


public class CourseSelectController : MonoBehaviour
{
    [SerializeField]
    private Image stamina, m1, m2,  s1, s2, bg, start;
    [SerializeField]
    private Text tlv, texp;
    [SerializeField]
    private CriAtomSource click;
    [SerializeField]
    private GameObject stPlus;

    private void Start()
    {
        StaminaRefresh();
        StateDrow();
        bg.sprite = GameManager.instance.SetAtlas.GetSprite("STAGESELECT");
        start.sprite = GameManager.instance.SetAtlas.GetSprite("STARTBUTTON");
        Observable.Interval(TimeSpan.FromSeconds(1)).Where(x => StaminaManager.staminaRP.Value < GameManager.instance.stamina.maxint)
            .Subscribe(_ => RestTimeRefresh()).AddTo(this);
        StaminaManager.staminaRP.DistinctUntilChanged().Subscribe(_ => StaminaRefresh()).AddTo(this);
        if (!Advertisement.IsReady("rewardedVideo"))
        {
            stPlus.SetActive(false);
        }
    }

    private void StateDrow()
    {
        tlv.text = "Lv " + GameManager.instance.playerdata.level;
        texp.text = "Exp " + GameManager.instance.playerdata.exp + " / " + GameManager.instance.playerdata.nextexp; 
    }

    public void StartClick()
    {
        if(StaminaManager.staminaRP.Value > 0)
        {
            --StaminaManager.staminaRP.Value;
            GameManager.instance.NextScene("GameScene");
        }
        else
        {
            string e = "スタミナが\nたりません!";
            MessageController.instance.ShowMessage(e);
        }
    } 

    public void HelpClick()
    {
        click.Play();
        HelpController.instance.ShowHelp();
    }

    public void SetupClick()
    {
        click.Play();
        SetupUI.instance.ShowSetup();
    }

    public void HartPlus()
    {
        if (GameManager.instance.stamina.nowint < GameManager.instance.stamina.maxint)
        {
            MessageController.instance.ShowMessage("動画を再生して\nスタミナを回復\nしますか？", "stamina");
        }
        else
        {
            MessageController.instance.ShowMessage("スタミナが\nまんたんです");
        }
    }

    private void RestTimeRefresh()
    {
        var mstr = (GameManager.instance.stamina.restint / 60).ToString("D2");
        var sstr = (GameManager.instance.stamina.restint % 60).ToString("D2");
        m1.sprite = GameManager.instance.ComAtlas.GetSprite("B" + mstr.Substring(0, 1));
        m2.sprite = GameManager.instance.ComAtlas.GetSprite("B" + mstr.Substring(1, 1));
        s1.sprite = GameManager.instance.ComAtlas.GetSprite("B" + sstr.Substring(0, 1));
        s2.sprite = GameManager.instance.ComAtlas.GetSprite("B" + sstr.Substring(1, 1));
    }

    private void StaminaRefresh()
    {
        stamina.sprite = GameManager.instance.ComAtlas.GetSprite("B" + StaminaManager.staminaRP.Value);
        RestTimeRefresh();
    }
}
