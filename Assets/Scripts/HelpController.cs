using UnityEngine;
using UnityEngine.UI;

// ヘルプウィンドウ表示用
public class HelpController : MonoBehaviour
{
    public static HelpController instance;
    [SerializeField]
    private GameObject helpUI;
    [SerializeField]
    private CriAtomSource click;
    [SerializeField]
    private Image close, title;

    private void Awake()
    {
        instance = this;
        title.sprite = GameManager.instance.SetAtlas.GetSprite("HELP");
        close.sprite = GameManager.instance.SetAtlas.GetSprite("CLOSE");
    }

    public void ShowHelp()
    {
        helpUI.SetActive(true);
    }

    public void CloseHelp()
    {
        click.Play();
        helpUI.SetActive(false);
    }
}
