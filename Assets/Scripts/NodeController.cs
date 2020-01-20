using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ゲーム内ショットプログラムのノード操作
public class NodeController : MonoBehaviour
{
    [SerializeField]
    private Dropdown drop;
    [SerializeField]
    private Slider sliderpow, sliderang, slidersec;
    [SerializeField]
    private Image image;
    [SerializeField]
    private Text pmin, pmax, pname, pval, amin, amax, aname, aval, smin, smax, sname, sval, nodename;
    [SerializeField]
    private int nodeNo;

    private List<string> arr;

    private void Awake()
    {
        DropListChange();
    }

    private void Start()
    {
        ArrSet();
        nodename.text = "プログラム" + (nodeNo + 1);
    }

    // 表示画像読み込み
    private void ArrSet()
    {
        arr = new List<string>();
        arr.Add("上ライン");
        arr.Add("横ライン");
        arr.Add("上カーブ");
        arr.Add("横カーブ");
        arr.Add("上スラローム");
        arr.Add("横スラローム");
        arr.Add("縦円");
        arr.Add("横円");
        arr.Add("ワープ");
        arr.Add("力");
        arr.Add("角度");
        arr.Add("秒");
        arr.Add("距離");
        arr.Add("速度");
        arr.Add("上下");
        arr.Add("左右");
    }

    // ショット種リスト
    private void DropListChange()
    {
        List<string> list = new List<string>();
        drop.ClearOptions();

        list.Add("なし");
        list.Add("ラインショット(上下)");
        list.Add("ラインショット(左右)");
        var lv = GameManager.instance.playerdata.level;
        if (lv >= 5) list.Add("コーナーショット(上下)");
        if (lv >= 10) list.Add("コーナーショット(左右)");
        if (lv >= 20) list.Add("スラロームショット(上下)");
        if (lv >= 25) list.Add("スラロームショット(左右)");
        if (lv >= 35) list.Add("ラウンドショット(縦)");
        if (lv >= 40) list.Add("ラウンドショット(横)");
        if (lv >= 50) list.Add("ワープ");

        drop.AddOptions(list);
        drop.RefreshShownValue();
    }

    // ショット種リスト変更時
    public void DDChanged()
    {
        switch (drop.value)
        {
            case 0:
                ItemUnActive();
                break;
            case 1:
                ItemActivate();
                image.sprite = GameManager.instance.ComAtlas.GetSprite(arr[(drop.value - 1)]);
                LineSliderSet();
                pname.text = arr[9];
                aname.text = arr[10];
                sname.text = arr[11];
                break;
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
                ItemActivate();
                image.sprite = GameManager.instance.ComAtlas.GetSprite(arr[(drop.value - 1)]);
                SliderSet();
                pname.text = arr[9];
                aname.text = arr[10];
                sname.text = arr[11];
                break;
            case 7:
            case 8:
                ItemActivate();
                image.sprite = GameManager.instance.ComAtlas.GetSprite(arr[(drop.value - 1)]);
                SliderSet();
                pname.text = arr[12];
                aname.text = arr[13];
                sname.text = arr[11];
                break;
            case 9:
                ItemActivate();
                image.sprite = GameManager.instance.ComAtlas.GetSprite(arr[(drop.value - 1)]);
                WarpSliderSet();
                pname.text = arr[12];
                aname.text = arr[14];
                sname.text = arr[15];
                break;
        }
    }

    private void ItemUnActive()
    {
        image.gameObject.SetActive(false);
        sliderpow.gameObject.SetActive(false);
        sliderang.gameObject.SetActive(false);
        slidersec.gameObject.SetActive(false);
    }

    private void ItemActivate()
    {
        if (!image.IsActive())
        {
            image.gameObject.SetActive(true);
            sliderpow.gameObject.SetActive(true);
            sliderang.gameObject.SetActive(true);
            slidersec.gameObject.SetActive(true);
        }
    }

    private void SliderSet()
    {
        var one = 1;

        var pow = GameManager.instance.playerdata.mpow;
        pmin.text = one.ToString();
        pmax.text = pow.ToString();
        sliderpow.minValue = one;
        sliderpow.maxValue = pow;
        sliderpow.value = one;
        pval.text = sliderpow.value.ToString();

        var ang = GameManager.instance.playerdata.mang;
        amin.text = (ang * -1).ToString();
        amax.text = ang.ToString();
        sliderang.minValue = ang * -1;
        sliderang.maxValue = ang;
        sliderang.value = 0;
        aval.text = sliderang.value.ToString();

        var sec = GameManager.instance.playerdata.msec;
        smin.text = one.ToString();
        smax.text = sec.ToString();
        slidersec.minValue = one;
        slidersec.maxValue = sec;
        slidersec.value = one;
        sval.text = slidersec.value.ToString();
    }

    private void LineSliderSet()
    {
        var one = 1;
        var minas = Mathf.FloorToInt(GameManager.instance.playerdata.mpow / 10) * -1;

        var pow = GameManager.instance.playerdata.mpow;
        pmin.text = minas.ToString();
        pmax.text = pow.ToString();
        sliderpow.minValue = minas;
        sliderpow.maxValue = pow;
        sliderpow.value = one;
        pval.text = sliderpow.value.ToString();

        var ang = GameManager.instance.playerdata.mang;
        amin.text = (ang * -1).ToString();
        amax.text = ang.ToString();
        sliderang.minValue = ang * -1;
        sliderang.maxValue = ang;
        sliderang.value = 0;
        aval.text = sliderang.value.ToString();

        var sec = GameManager.instance.playerdata.msec;
        smin.text = one.ToString();
        smax.text = sec.ToString();
        slidersec.minValue = one;
        slidersec.maxValue = sec;
        slidersec.value = one;
        sval.text = slidersec.value.ToString();
    }

    private void WarpSliderSet()
    {
        var one = 1;

        var pow = GameManager.instance.playerdata.mpow;
        pmin.text = (pow * -1).ToString();
        pmax.text = pow.ToString();
        sliderpow.minValue = pow * -1;
        sliderpow.maxValue = pow;
        sliderpow.value = one;
        pval.text = sliderpow.value.ToString();

        var ang = GameManager.instance.playerdata.mang;
        amin.text = (ang * -1).ToString();
        amax.text = ang.ToString();
        sliderang.minValue = ang * -1;
        sliderang.maxValue = ang;
        sliderang.value = 0;
        aval.text = sliderang.value.ToString();

        smin.text = (ang * -1).ToString();
        smax.text = ang.ToString();
        slidersec.minValue = ang * -1;
        slidersec.maxValue = ang;
        slidersec.value = 0;
        sval.text = slidersec.value.ToString();
    }

    public void PowSliderChanged()
    {
        pval.text = sliderpow.value.ToString();
    }

    public void AngSliderChanged()
    {
        aval.text = sliderang.value.ToString();
    }

    public void SecSliderChanged()
    {
        sval.text = slidersec.value.ToString();
    }

    // 各設定値を処理部へ引き渡し
    public void ProgramSave()
    {
        GameManager.instance.shotProgram[nodeNo, 0] = drop.value;
        if (drop.value == 0)
        {
            GameManager.instance.shotProgram[nodeNo, 1] = 0;
            GameManager.instance.shotProgram[nodeNo, 2] = 0;
            GameManager.instance.shotProgram[nodeNo, 3] = 0;
        }
        else
        {
            GameManager.instance.shotProgram[nodeNo, 1] = (int)sliderpow.value;
            GameManager.instance.shotProgram[nodeNo, 2] = (int)sliderang.value;
            GameManager.instance.shotProgram[nodeNo, 3] = (int)slidersec.value;
        }
    }
}
