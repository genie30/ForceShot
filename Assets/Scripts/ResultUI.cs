using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class ResultUI : MonoBehaviour
{
    [SerializeField]
    private List<Image> stars, imgs;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private GameObject retry, cs, expboost;
    private int getExp, rewardexp;
    [SerializeField]
    private CriAtomSource star;

    private void Awake()
    {
        var sa = GameManager.instance.SetAtlas;
        imgs[0].sprite = sa.GetSprite("HOLEIN");
        imgs[1].sprite = sa.GetSprite("RETRY");
        imgs[2].sprite = sa.GetSprite("RTCourseSelect");
        ShowButton(false);
    }

    private void Start()
    {
        // EXP取得処理
        Observable.FromCoroutine(() => ExpProcess()).Subscribe(_ => ShowButton(true)).AddTo(this);
        // Reward広告表示
        AdsManager.rewardCheck.DistinctUntilChanged().Where(x => x == true).Subscribe(_ => StartCoroutine(ExpReward())).AddTo(this);
    }

    IEnumerator ExpReward()
    {
        var ex = 0;
        while (rewardexp != 0 && GameManager.expRP.Value < 99999999)
        {
            if (rewardexp > 20)
            {
                ex = 10;
            }
            else
            {
                ex = 1;
            }
            rewardexp -= ex;
            GameManager.expRP.Value += ex;
            slider.value = (float)GameManager.expRP.Value / GameManager.instance.playerdata.nextexp;
            yield return null;
        }
        if(GameManager.expRP.Value > 99999999)
        {
            GameManager.instance.playerdata.exp = 99999999;
        }
        else
        {
            GameManager.instance.playerdata.exp = GameManager.expRP.Value;
        }
        ShowButton(true);
        AdsManager.rewardCheck.Value = false;
    }

    // 取得ExpをExpバーに反映
    IEnumerator ExpProcess()
    {
        var temp = StageList.instance.etcList[GameManager.instance.setHole - 1].ToArray();
        getExp = temp[3];

        for (int i = 0; i < 3; i++)
        {
            if (temp[i] >= GameManager.instance.shotCount)
            {
                stars[i].sprite = GameManager.instance.ComAtlas.GetSprite("StarOn");
                star.Play();
                getExp += 10 * (i + 1);
            }
            yield return new WaitForSeconds(0.3f);
        }

        rewardexp = getExp * 2;
        var ex = 0;
        while (getExp != 0 && GameManager.expRP.Value < 99999999)
        {
            if (getExp > 20)
            {
                ex = 10;
            }
            else
            {
                ex = 1;
            }
            getExp -= ex;
            GameManager.expRP.Value += ex;
            slider.value = (float)GameManager.expRP.Value / GameManager.instance.playerdata.nextexp;
            yield return null;
        }
        if(GameManager.expRP.Value > 99999999)
        {
            GameManager.instance.playerdata.exp = 99999999;
        }
        else
        {
            GameManager.instance.playerdata.exp = GameManager.expRP.Value;
        }
        GameManager.instance.StageNext();
    }

    private void ShowButton(bool b)
    {
        retry.SetActive(b);
        cs.SetActive(b);
        if (!AdsManager.rewardCheck.Value) expboost.SetActive(b);
    }

    public void OnRetryClick()
    {
        if(StaminaManager.staminaRP.Value > 0)
        {
            string e = "このコースを\nもういちど\nプレイします。\nよろしいですか？";
            MessageController.instance.ShowMessage(e, "Retry");
        }
        else
        {
            string e = "スタミナが\nたりません!";
            MessageController.instance.ShowMessage(e);
        }
    }

    public void OnCSClick()
    {
        GameManager.instance.SetHoleNext();
        GameManager.instance.NextScene("CourseSelect");
    }

    public void ExpBoost()
    {
        AdsManager.instance.RewardShow("exp");
        ShowButton(false);
    }
}
