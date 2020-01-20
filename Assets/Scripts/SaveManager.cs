using UnityEngine;
using System;

// データ管理クラス
public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    private DataBank db;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        db = DataBank.Open();
    }

    public void PlayerDataSave()
    {
        GameManager.instance.playerdata.exp = GameManager.expRP.Value;
        db.Store("player", GameManager.instance.playerdata);
        db.Save("player");
    }

    public void StaminaSave()
    {
        GameManager.instance.stamina.nowint = StaminaManager.staminaRP.Value;
        db.Store("stamina", GameManager.instance.stamina);
        db.Save("stamina");
    }

    public void SettingSave()
    {
        db.Store("setting", GameManager.instance.setting);
        db.Save("setting");
    }

    public void AllDataSave()
    {
        GameManager.instance.playerdata.exp = GameManager.expRP.Value;
        GameManager.instance.stamina.nowint = StaminaManager.staminaRP.Value;
        db.Store("player", GameManager.instance.playerdata);
        db.Store("stamina", GameManager.instance.stamina);
        db.Store("setting", GameManager.instance.setting);
        db.SaveAll();
    }

    public void SaveDataLoad()
    {
        db.Load<PlayerData>("player");
        if (db.ExistsKey("player"))
        {
            GameManager.instance.playerdata = db.Get<PlayerData>("player");
        }
        else
        {
            GameManager.instance.playerdata = new PlayerData
            {
                level = 1,
                exp = 0,
                nextexp = 100,
                stage = 1,
                mpow = 20,
                mang = 30,
                msec = 5
            };
        }

        db.Load<Stamina>("stamina");
        if (db.ExistsKey("stamina"))
        {
            GameManager.instance.stamina = db.Get<Stamina>("stamina");
        }
        else
        {
            GameManager.instance.stamina = new Stamina
            {
                nowint = 3,
                maxint = 3,
                restint = 0,
                lasttime = DateTime.Now.ToString()
            };
        }

        db.Load<Settings>("setting");
        if (db.ExistsKey("setting"))
        {
            GameManager.instance.setting = db.Get<Settings>("setting");
        }
        else
        {
            GameManager.instance.setting = new Settings
            {
                bgm = 1,
                bgmon = true,
                se = 1,
                seon = true,
                lang = Application.systemLanguage.ToString()
            };
        }
    }
}

[Serializable]
public class PlayerData
{
    public int level;
    public int exp;
    public int nextexp;
    public int stage;
    public int mpow;
    public int mang;
    public int msec;
    public int[,] programList;
    public bool shotsave;
    public override string ToString()
    {
        return $"{ base.ToString() } { JsonUtility.ToJson(this) }";
    }
}

[Serializable]
public class Stamina
{
    public int maxint, nowint, restint;
    public string lasttime;
    public override string ToString()
    {
        return $"{ base.ToString() } { JsonUtility.ToJson(this) }";
    }
}

[Serializable]
public class Settings
{
    public float bgm, se;
    public bool bgmon, seon;
    public string lang;

    public override string ToString()
    {
        return $"{ base.ToString() } { JsonUtility.ToJson(this) }";
    }
}

