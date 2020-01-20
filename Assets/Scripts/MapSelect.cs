using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// コースセレクト
public class MapSelect : MonoBehaviour
{
    private ScrollRect sr;
    private float scInit, scPos;
    [SerializeField]
    private Button leftb, rightb;
    [SerializeField]
    private Image lefti, righti, sn1, sn2;
    [SerializeField]
    private CriAtomSource click;

    public List<GameObject> stage = new List<GameObject>();

    private void Awake()
    {
        sr = GetComponent<ScrollRect>();
    }

    private void Start()
    {
        StageSelect();
        MoveEnd();
    }

    private void StageSelect()
    {
        var st = GameManager.instance.playerdata.stage;
        for (int i = 0; i < st; i++)
        {
            stage[i].SetActive(true);
        }
        scInit = 1f / (st - 1);
        scPos = scInit * (GameManager.instance.setHole - 1);
        sr.horizontalNormalizedPosition = scPos;
    }

    public void LeftClick()
    {
        click.Play();
        scPos -= scInit;
        sr.horizontalNormalizedPosition = scPos;
        GameManager.instance.setHole -= 1;
        MoveEnd();
    }

    public void RightClick()
    {
        click.Play();
        scPos += scInit;
        sr.horizontalNormalizedPosition = scPos;
        GameManager.instance.setHole += 1;
        MoveEnd();
    }

    private void MoveEnd()
    {
        // ステージ送りボタンの有効無効判断
        LeftEnable(GameManager.instance.setHole != 1);
        RightEnable(GameManager.instance.setHole != GameManager.instance.playerdata.stage);

        // ステージ番号の画像読み込み
        var str = GameManager.instance.setHole.ToString("D2");
        sn1.sprite = GameManager.instance.ComAtlas.GetSprite("W" + str.Substring(0, 1));
        sn2.sprite = GameManager.instance.ComAtlas.GetSprite("W" + str.Substring(1, 1));
    }

    private void LeftEnable(bool b)
    {
        if (b)
        {
            lefti.sprite = GameManager.instance.ComAtlas.GetSprite("MapButtonTrue");
            leftb.enabled = true;
        }
        else
        {
            lefti.sprite = GameManager.instance.ComAtlas.GetSprite("MapButtonFalse");
            leftb.enabled = false;
        }
    }

    private void RightEnable(bool b)
    {
        if (b)
        {
            righti.sprite = GameManager.instance.ComAtlas.GetSprite("MapButtonTrue");
            rightb.enabled = true;
        }
        else
        {
            righti.sprite = GameManager.instance.ComAtlas.GetSprite("MapButtonFalse");
            rightb.enabled = false;
        }
    }
}
