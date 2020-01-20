using UnityEngine;
using UnityEngine.Advertisements;
using UniRx;

// UnityAds(Android用のみ)
public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    public static AdsManager instance;
    private string mode = "";
    public static ReactiveProperty<bool> rewardCheck = new ReactiveProperty<bool>(false);
    private float tbgm, tse;
    [SerializeField]
    private GameObject dummy;
    public bool intershow = true;

#if UNITY_ANDROID
    private const string gameID = "3223185";
    private const bool testMode = false;
#endif

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Advertisement.Initialize(gameID, testMode);
    }

    // リワード広告再生
    public void RewardShow(string m)
    {
        tbgm = CriAtom.GetCategoryVolume(0);
        tse = CriAtom.GetCategoryVolume(1);
        mode = m;
        if (Advertisement.IsReady("rewardedVideo"))
        {
            GameManager.instance.BGMChange(0f);
            GameManager.instance.SEChange(0f);
            dummy.SetActive(true);
            Advertisement.Show("rewardedVideo");
        }
    }

    // 通常広告再生
    public void InterstitialShow()
    {
        if (GameManager.instance.NextSceneName == "CourseSelect" && Advertisement.IsReady("Interstitial") && intershow)
        {
            Advertisement.Show("Interstitial");
        }
        intershow = true;
    }

    // リワード広告の再生チェック
    private void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            if(mode == "stamina")
            {
                StaminaManager.staminaRP.Value++;
            }
            else if (mode == "exp")
            {
                rewardCheck.Value = true;
                intershow = false;
            }
        }
        GameManager.instance.BGMChange(tbgm);
        GameManager.instance.SEChange(tse);
        dummy.SetActive(false);
        mode = "";
    }

    public void OnUnityAdsReady(string placementId)
    {
    }

    public void OnUnityAdsDidError(string message)
    {
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        HandleShowResult(showResult);
    }
}
