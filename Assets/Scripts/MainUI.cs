using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

// ゲーム画面メインUI
public class MainUI : MonoBehaviour
{
    [SerializeField]
    private GameObject main, prog, pause, shotButton;
    [SerializeField]
    private Text shotcount;
    [SerializeField]
    private Image pImg, sImg;

    private void Start()
    {
        // 各言語用UI読み込み
        pImg.sprite = GameManager.instance.SetAtlas.GetSprite("PROGRAM");
        sImg.sprite = GameManager.instance.SetAtlas.GetSprite("SHOT");
        // ショット数表示用の変数監視
        this.UpdateAsObservable().Select(x => GameManager.instance.shotCount).DistinctUntilChanged().Select(x => "Shot" + GameManager.instance.shotCount)
            .SubscribeToText(shotcount).AddTo(this);
    }

    public void ProgramClick()
    {
        prog.SetActive(true);
        main.SetActive(false);
    }
    public void ShotClick()
    {
        if (GameManager.stateRP.Value == GameState.Ready)
        {
            GameManager.stateRP.Value = GameState.Shot;
            shotButton.SetActive(false);
            main.SetActive(false);
        }
    }
    public void PauseClick()
    {
        main.SetActive(false);
        pause.SetActive(true);
    }
    public void SetupClick()
    {
        SetupUI.instance.ShowSetup();
    }
}
