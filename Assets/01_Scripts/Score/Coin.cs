using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    [Header("점수 및 업적 설정")]
    public int scoreValue = 1;

    public TMP_Text textScore;

    void Start()
    {
        // UI 업데이트를 위해 이벤트 구독 (GameManager가 준비되었는지 확인 후)
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnScoreChanged += UpdateScoreDisplay;
            GameManager.Instance.OnAchievementUnlocked += ShowAchievementNotification;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnGetCoin();
            Destroy(this.gameObject);
        }
    }

    // 코인 획득 시 호출
    public void OnGetCoin()
    {
        if (GameManager.Instance == null) return;
        
        GameManager.Instance.AddScore(scoreValue); // 점수 추가
        GameManager.Instance.ProgressAchievement("Get_10_Coin", 1); // 업적 진행도 증가
    }

    // 아이템 사용 시 호출
    public void OnItemUsed()
    {
        if (GameManager.Instance == null) return;

        // 아이템 수집 업적 진행
        GameManager.Instance.ProgressAchievement("Use_5_Items", 1);
    }

    /// <summary>
    /// 이벤트 핸들러 (UI나 알림창 역할)
    /// </summary>
    private void UpdateScoreDisplay(int newScore)
    {
        // 실제 UI Text 컴포넌트를 업데이트하는 로직
        Debug.Log($"[Coin] 현재 점수: {newScore}");
        // scoreText.text = newScore.ToString(); 
        textScore.text = newScore.ToString();
    }

    private void ShowAchievementNotification(string achievementName)
    {
        // 업적 달성 시 화면에 팝업을 띄우는 로직
        Debug.Log($"[Coin] 업적 '{achievementName}'를 달성했습니다!");
    }

    private void OnDestroy()
    {
        // 오브젝트가 파괴될 때 이벤트 구독 해제 (메모리 누수 방지)
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnScoreChanged -= UpdateScoreDisplay;
            GameManager.Instance.OnAchievementUnlocked -= ShowAchievementNotification;
        }
    }
}
