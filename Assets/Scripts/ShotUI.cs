using UnityEngine;
using UniRx;

// ショットUI
public class ShotUI : MonoBehaviour
{
    public GameObject shotUI;
    private void Start()
    {
        GameManager.stateRP.DistinctUntilChanged().Subscribe(_ => UIShow(_)).AddTo(this);
    }

    private void UIShow(GameState state)
    {
        switch (state)
        {
            case GameState.Shot:
            case GameState.Fly:
                shotUI.SetActive(true);
                break;
            default:
                Time.timeScale = 1;
                shotUI.SetActive(false);
                break;
        }
    }

    public void SkipClick()
    {
        Time.timeScale += 3;
    }
}
