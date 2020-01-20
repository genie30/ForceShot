using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx;

// ロード画面用
public class LoadingController : MonoBehaviour
{
    public GameObject next;
    public Slider slider;
    private AsyncOperation async;

    private void Awake()
    {
        Observable.FromCoroutine(() => NextSceneLoad()).Subscribe().AddTo(this);
    }

    // 読み込み状況をバーに表示し、完了したら遷移ボタン表示
    private IEnumerator NextSceneLoad()
    {
        async = SceneManager.LoadSceneAsync(GameManager.instance.NextSceneName, LoadSceneMode.Single);
        async.allowSceneActivation = false;
        AdsManager.instance.InterstitialShow();
        SaveManager.instance.AllDataSave();
        System.GC.Collect();
        Resources.UnloadUnusedAssets();
        while (async.progress < 0.9f)
        {
            slider.value = async.progress;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        slider.value = 1;
        next.SetActive(true);
    }

    // 遷移ボタンの動作
    public void NextScene()
    {
        async.allowSceneActivation = true;
    }
}
