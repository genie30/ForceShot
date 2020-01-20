using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UniRx;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerData playerdata; // ゲームデータ
    public Stamina stamina; // スタミナデータ
    public Settings setting; // 設定データ

    public int[,] shotProgram = new int[5, 4]; // ゲーム中のショットプログラム
    public int shotCount, setHole;

    // ゲームステート監視用
    public static ReactiveProperty<GameState> stateRP = new ReactiveProperty<GameState>(GameState.Stay);
    public static ReactiveProperty<int> expRP = new ReactiveProperty<int>();

    [SerializeField]
    private CriAtomSource lvup; // レベルアップ時SE
    public string NextSceneName; // シーン遷移用
    private const int maxStage = 20; // ゲームステージ数

    public SpriteAtlas comAtlas, enAtlas, jpAtlas; // 各言語用UI
    public SpriteAtlas ComAtlas { get { return comAtlas; } }
    public SpriteAtlas SetAtlas { get; private set; }

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        SDLoad(); // セーブデータロード
        if (setting.lang == "Japanese")
        {
            AtlasLanguage("jp");
        }
        else
        {
            AtlasLanguage("en");
        }
    }

    private void Start()
    {
        setHole = playerdata.stage;

        // expの変動を監視
        expRP.DistinctUntilChanged().Where(x => playerdata.level != 50).Where(x => expRP.Value >= playerdata.nextexp).Subscribe(_ => LevelUP()).AddTo(this);
        // 設定データ読み込み音量調整
        BGMChange(setting.bgm);
        SEChange(setting.se);
    }

    public void AtlasLanguage(string lang)
    {
        if (lang == "jp") SetAtlas = jpAtlas;
        if(lang == "en") SetAtlas = enAtlas;
    }

    public void NextScene(string name)
    {
        // 遷移先シーン名保存してロード画面表示
        NextSceneName = name;
        SceneManager.LoadSceneAsync("Loading", LoadSceneMode.Single);
    }

    private void OnApplicationQuit()
    {
        stamina.lasttime = DateTime.Now.ToString();
        SaveManager.instance.AllDataSave();
    }

    private void SDLoad()
    {
        SaveManager.instance.SaveDataLoad();
        StaminaManager.instance.StaminaDef();
        expRP.Value = playerdata.exp;
    }

    public void StageNext()
    {
        if (playerdata.stage == setHole && playerdata.stage < maxStage)
        {
            playerdata.stage++;
        }
    }

    public void SetHoleNext()
    {
        if(setHole < maxStage)
        {
            setHole++;
        }
    }

    private void LevelUP()
    {
        // レベルアップ処理　Max50
        if (playerdata.level >= 50) return;
        lvup.Play();
        expRP.Value -= playerdata.nextexp;
        playerdata.level++;
        if(playerdata.level == 50)
        {
            playerdata.nextexp = 99999999;
        }
        else
        {
            playerdata.nextexp = playerdata.nextexp + 100;
        }

        if (playerdata.level == 2) playerdata.mang++;
        var powup = playerdata.level % 10;
        switch (powup)
        {
            case 1:
            case 3:
            case 6:
            case 8:
                playerdata.mang++;
                break;
            case 2:
            case 4:
            case 7:
            case 9:
                playerdata.mpow++;
                playerdata.mang++;
                break;
            case 5:
                playerdata.mpow++;
                playerdata.mang += 2;
                stamina.maxint++;
                break;
            case 0:
                playerdata.mpow++;
                playerdata.mang += 2;
                playerdata.msec++;
                break;
        }
    }

    public void BGMChange(float v)
    {
        CriAtom.SetCategoryVolume(0, v);
    }

    public void SEChange(float v)
    {
        CriAtom.SetCategoryVolume(1, v);
    }
}

// ゲームステート
public enum GameState
{
    Stay,
    Ready,
    Shot,
    Fly,
    Clear,
    Water,
    OB,
    HoleIn,
    Result,
}
