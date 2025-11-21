using UnityEngine;

public enum AchievementType //업적 종류
{
    CoinAcquisition, // 코인 획득
    BuySomething, // 물건 구매
    UseItem // 아이템 사용
}

[CreateAssetMenu(fileName = "NewAachievement", menuName = "Game System/Achievement Data")]
public class AchievementData : ScriptableObject
{
    [Header("필수 정보")]
    [SerializeField]
    public string id;   // 고유 ID
    public string achievementName;  // 업적 이름
    public string description;  // 업적 설명

    [Tooltip("업적 해금에 필요한 값")]
    public int requiredCount; // 목표값

    [Header("로직 정보")]
    public AchievementType type;    // 업적 타입
    public bool isUnlocked; // 달성 여부

    [Header("보상 정보")]
    public int scoreReward; // 보상 점수

    // 업적 해금 상태를 외부에서 읽기 전용으로 접근
    public bool IsUnlocked => isUnlocked;

    /// <summary>
    /// 업적을 해금하고 상태를 변경합니다.
    /// </summary>
    public void Unlock()
    {
        if (!isUnlocked)
        {
            isUnlocked = true;
            Debug.Log($"업적 해금: {achievementName}");
            // 업적 해금 시 필요한 추가적인 로직을 여기에 추가
        }
    }
}
