using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // 현재 획득한 총 코인 개수
    private int totalCoinCount = 0;
    public int TotalCoinCount => totalCoinCount;

    [SerializeField]
    private AchievementManager achievementManager;

    //// 이벤트 통신
    public event Action<int> OnScoreChanged; // 점수가 변경될 때 외부에 알림
    //public event Action<string> OnAchievementUnlocked; // 업적이 잠금 해제될 때 외부에 알림

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환에도 유지
        }
        else
        {
            Destroy(gameObject);
        }

        LoadData();
    }

    // 코인을 획득할 때 호출되는 메서드
    public void EarnCoin(int amount)
    {
        if (amount <= 0) return;
        totalCoinCount += amount;
        OnScoreChanged?.Invoke(totalCoinCount);
        UnityEngine.Debug.Log($"코인 획득! 현재 총 코인: {totalCoinCount}");

        // 코인 개수가 변경될 때마다 업적 해금 조건을 검사
        if (achievementManager != null)
        {
            achievementManager.CheckAchievements(totalCoinCount);
        }
    }

    private void LoadData()
    {
        totalCoinCount = PlayerPrefs.GetInt("CurrentCoin", 0);
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("CurrentCoin", totalCoinCount);
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}

