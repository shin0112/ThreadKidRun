using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("코인 저장 값 초기화")]
    public bool isReset; //디버깅용

    public static GameManager Instance { get; private set; }

    // 현재 획득한 총 코인 개수
    private int totalCoinCount = 0;
    public int TotalCoinCount
    {
        get { return totalCoinCount; }
        private set
        {
            if (value < 0)
            {
                Logger.Log("코인 개수 부족");
                return;
            }

            totalCoinCount = value;
            Logger.Log($"현재 코인 개수: {totalCoinCount}");

            UIManager.Instance.CoinUI.UpdateCoinText(totalCoinCount);
        }
    }

    private int currentScore = 0; //현재 스코어
    private int getItemCount = 0; //획득한 아이템 수

    [SerializeField]
    private AchievementManager achievementManager;

    [SerializeField]
    private CoinUI coinUI;

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

    public void AddScore(int amount)
    {
        if (amount <= 0) return;
        currentScore += amount;
        OnScoreChanged?.Invoke(currentScore);
        EarnCoin(amount);
        UnityEngine.Debug.Log($"코인 획득! 현재 스코어: {currentScore}");
    }

    // 코인을 획득할 때 호출되는 메서드
    public void EarnCoin(int amount)
    {
        if (amount < 0) return;
        totalCoinCount += amount;
        UnityEngine.Debug.Log($"코인 획득! 현재 총 코인: {totalCoinCount}");
        UIManager.Instance.CoinUI.UpdateCoinText(totalCoinCount); //코인 UI에 반영

        // 코인 개수가 변경될 때마다 업적 해금 조건을 검사
        if (achievementManager != null)
        {
            achievementManager.CheckAchievements(totalCoinCount, AchievementType.CoinAcquisition);
        } // <- 업적 해금 방법. 다른 업적(아이템 사용, 아이템 구매하는) 함수에 위의 코드를 가져다 쓰면 됨
    }

    public void EarnItem(int amount)
    {
        if (amount < 0) return;
        getItemCount += amount;
        UnityEngine.Debug.Log($"아이템 사용! 현재 사용한 아이템 수: {getItemCount}");
        if (achievementManager != null)
        {
            achievementManager.CheckAchievements(getItemCount, AchievementType.UseItem);
        }
    }

    private void LoadData()
    {
        if (isReset)
        {
            totalCoinCount = 0;
            return;
        }
        totalCoinCount = PlayerPrefs.GetInt("CurrentCoin", 0);
        getItemCount = PlayerPrefs.GetInt("CurrentUseItem", 0);
        EarnCoin(0);
        EarnItem(0);
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("CurrentCoin", totalCoinCount);
        PlayerPrefs.SetInt("CurrentUseItem", getItemCount);
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    public bool CheckSpendCoinAndGetSkin(int amount)
    {
        Logger.Log($"코인 {amount}개 사용");
        TotalCoinCount -= amount;
        return true;
    }

    public void ResetCoin()
    {
        TotalCoinCount = 0;
    }
}

