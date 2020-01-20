using System;
using UnityEngine;
using UniRx;

// スタミナ管理
public class StaminaManager : MonoBehaviour
{
    public static StaminaManager instance;
    const int recoverTime = 30 * 60; // 回復時間(秒)
    public static ReactiveProperty<int> staminaRP = new ReactiveProperty<int>();
    private int rest;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // 1秒ごとに回復時間を更新して0になったらスタミナ回復+カウントリセット
        Observable.Interval(TimeSpan.FromSeconds(1)).Where(x => staminaRP.Value < GameManager.instance.stamina.maxint)
            .Subscribe(_ => StaminaRecover()).AddTo(this);
        staminaRP.DistinctUntilChanged().Subscribe(_ => StaminaCheck()).AddTo(this);
    }

    // 休止中の経過時間からスタミナ回復反映
    public void StaminaDef()
    {
        staminaRP.Value = GameManager.instance.stamina.nowint;
        rest = GameManager.instance.stamina.restint;

        if (staminaRP.Value >= GameManager.instance.stamina.maxint) return;

        var timestring = GameManager.instance.stamina.lasttime;
        DateTime datetime = DateTime.Parse(timestring);
        TimeSpan span = DateTime.Now - datetime;
        var spantime = span.TotalSeconds;
        int recoverStaminaNum = (int)(spantime / recoverTime);
        int resttime = (int)(spantime % recoverTime);

        staminaRP.Value += recoverStaminaNum;
        if (rest >= resttime)
        {
            rest -= resttime;
        }
        else
        {
            staminaRP.Value++;
            rest = recoverTime + rest - resttime;
        }
        StaminaCheck();
    }

    // 回復時処理
    private void StaminaRecover()
    {
        --rest;
        if(rest == 0)
        {
            rest = recoverTime;
            ++staminaRP.Value;
        }
        GameManager.instance.stamina.restint = rest;
    }

    private void StaminaCheck()
    {
        if (staminaRP.Value < GameManager.instance.stamina.maxint && rest == 0)
        {
            rest = recoverTime;
            GameManager.instance.stamina.restint = rest;
        }
        else if (staminaRP.Value >= GameManager.instance.stamina.maxint)
        {
            staminaRP.Value = GameManager.instance.stamina.maxint;
            rest = 0;
            GameManager.instance.stamina.restint = rest;
        }
        GameManager.instance.stamina.nowint = staminaRP.Value;
    }
}
