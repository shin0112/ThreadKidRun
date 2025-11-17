using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("점수 관리")]
    [SerializeField] private int currentScore = 0;
    [SerializeField] private int highScore = 0;

    // 업적 진행 상황 저장 (업적 이름, 현재 진행도)
    private Dictionary<string, int> achievementProgress = new Dictionary<string, int>();

    // 이벤트 통신
    public event Action<int> OnScoreChanged; // 점수가 변경될 때 외부에 알림
    public event Action<string> OnAchievementUnlocked; // 업적이 잠금 해제될 때 외부에 알림

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

        // 데이터 로드 및 업적 초기화 (실제 게임에서는 저장된 데이터를 로드해야 함)
        LoadData();
        InitializeAchievements();
    }

    private void InitializeAchievements()
    {
        // 예시 업적 목록 초기화
        achievementProgress["Get_10_Coin"] = 0; //코인 10개 획득 (현재 0)
        achievementProgress["Use_5_Items"] = 0; //아이템 5개 사용 (현재 0)
    }

    /// <summary>
    /// 점수를 추가하고 최고 점수를 갱신합니다.
    /// </summary>
    public void AddScore(int amount)
    {
        if (amount <= 0) return;

        currentScore += amount;

        // 최고 점수 갱신
        if (currentScore > highScore)
        {
            highScore = currentScore;
        }

        // 점수 변경 이벤트를 발생시켜 UI나 다른 시스템에 알립니다.
        OnScoreChanged?.Invoke(currentScore);

        // 점수 추가가 업적 조건에 영향을 줄 수 있다면 여기서 체크
        // CheckScoreBasedAchievements(currentScore);
    }

    /// <summary>
    /// 특정 업적의 진행도를 증가시킵니다.
    /// </summary>
    public void ProgressAchievement(string achievementName, int progressAmount = 1)
    {
        if (!achievementProgress.ContainsKey(achievementName))
        {
            Debug.LogError($"업적 이름이 잘못되었습니다: {achievementName}");
            return;
        }

        achievementProgress[achievementName] += progressAmount;

        // 업적 달성 여부 확인
        if (achievementName == "Get_10_Coin" && achievementProgress[achievementName] >= 10)
        {
            UnlockAchievement(achievementName);
        }
        if (achievementName == "Use_5_Items" && achievementProgress[achievementName] >= 5)
        {
            UnlockAchievement(achievementName);
        }
        // 다른 업적 조건들도 여기에 추가...

        Debug.Log($"[GM] 업적 [{achievementName}] 진행도: {achievementProgress[achievementName]}");
    }

    private void UnlockAchievement(string achievementName)
    {
        // 이미 달성했는지 체크하는 로직 추가 필요

        Debug.Log($"[GM] 업적 달성: {achievementName}");

        // 업적 달성 이벤트를 발생시켜 UI나 알림 시스템에 알립니다.
        OnAchievementUnlocked?.Invoke(achievementName);

        // 달성 후에는 진행도를 무한정 올리지 않도록 막거나, 별도의 "Unlocked" 상태를 표시해야 합니다.
    }


    private void LoadData()
    {
        // PlayerPrefs 등을 사용하여 실제 데이터를 로드합니다.
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
        // 기타 데이터 저장
    }

    // 게임 종료 시 데이터 저장
    private void OnApplicationQuit()
    {
        SaveData();
    }
}

