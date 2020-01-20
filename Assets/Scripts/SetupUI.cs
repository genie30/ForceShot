using UnityEngine;
using UnityEngine.UI;

// 設定画面管理クラス
public class SetupUI : MonoBehaviour
{
    public static SetupUI instance;
    [SerializeField]
    private GameObject setup, objbgm, objse;
    [SerializeField]
    private Toggle tbgm, tse;
    [SerializeField]
    private Slider sbgm, sse;
    [SerializeField]
    private CriAtomSource click;
    [SerializeField]
    private Image resume, title;

    private void Awake()
    {
        instance = this;
        title.sprite = GameManager.instance.SetAtlas.GetSprite("SETUP");
        resume.sprite = GameManager.instance.SetAtlas.GetSprite("RESUME");
    }

    public void ShowSetup()
    {
        setup.SetActive(true);
        tbgm.isOn = GameManager.instance.setting.bgmon;
        objbgm.SetActive(tbgm.isOn);
        if (tbgm.isOn) sbgm.value = GameManager.instance.setting.bgm;
        tse.isOn = GameManager.instance.setting.seon;
        objse.SetActive(tse.isOn);
        if (tbgm.isOn) sse.value = GameManager.instance.setting.se;
    }

    public void CloseSetup()
    {
        click.Play();
        setup.SetActive(false);
    }

    public void BGMToggleChange()
    {
        objbgm.SetActive(tbgm.isOn);
        GameManager.instance.setting.bgmon = tbgm.isOn;
        if (!tbgm.isOn)
        {
            GameManager.instance.BGMChange(0f);
        }
        else
        {
            sbgm.value = GameManager.instance.setting.bgm;
            GameManager.instance.BGMChange(sbgm.value);
        }
    }

    public void SEToggleChange()
    {
        objse.SetActive(tse.isOn);
        GameManager.instance.setting.seon = tse.isOn;
        if (!tse.isOn)
        {
            GameManager.instance.SEChange(0f);
        }
        else
        {
            sbgm.value = GameManager.instance.setting.se;
            GameManager.instance.SEChange(sse.value);
        }
    }

    public void BGMSliderChange()
    {
        GameManager.instance.setting.bgm = sbgm.value;
        GameManager.instance.BGMChange(sbgm.value);
    }

    public void SESliderChange()
    {
        GameManager.instance.setting.se = sse.value;
        GameManager.instance.SEChange(sse.value);
    }
}
