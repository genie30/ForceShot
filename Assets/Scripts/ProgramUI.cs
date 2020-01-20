using UnityEngine;
using UnityEngine.UI;

// ショットプログラム画面
public class ProgramUI : MonoBehaviour
{
    [SerializeField]
    private GameObject main, prog, shotButton;
    public GameObject[] node;
    [SerializeField]
    private Toggle toggle;
    [SerializeField]
    private Image title, set, cancel;

    private void Awake()
    {
        var sa = GameManager.instance.SetAtlas;
        title.sprite = sa.GetSprite("SHOTPROGRAM");
        set.sprite = sa.GetSprite("SET");
        cancel.sprite = sa.GetSprite("CANCEL");
        NodeSet();
    }

    // レベルに応じてノード表示
    private void NodeSet()
    {
        for (int i = 2; i >= 5; i++)
        {
            node[i].SetActive(false);
        }
        var lv = GameManager.instance.playerdata.level;
        if (lv >= 15) node[2].SetActive(true);
        if (lv >= 30) node[2].SetActive(true);
        if (lv >= 45) node[2].SetActive(true);
    }

    public void SetClick()
    {
        for(int i = 0; i < 5; i++)
        {
            if(node[i].activeSelf)
            {
                var nc = node[i].GetComponent<NodeController>();
                nc.ProgramSave();
            }
            if (GameManager.instance.shotProgram[0, 0] != 0)
            {
                GameManager.instance.playerdata.shotsave = toggle.isOn;
                if (toggle.isOn)
                {
                    GameManager.instance.playerdata.programList = GameManager.instance.shotProgram;
                }
                GameManager.stateRP.Value = GameState.Ready;
            }
        }
        UIClose();
    }

    public void CancelClick()
    {
        UIClose();
    }

    private void UIClose()
    {
        main.SetActive(true);
        if(GameManager.stateRP.Value == GameState.Ready)
        {
            shotButton.SetActive(true);
        }
        prog.SetActive(false);
    }
}
