using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class AchievementUI : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("이 UI가 나타내는 AchievementData (인스펙터에서 연결)")]
    public AchievementData linkedAchievement;

    [Tooltip("색상을 변경할 UI 이미지 컴포넌트")]
    public Image imageToChange;

    [Header("Color Settings")]
    public Color lockedColor = Color.gray; // 잠금 상태일 때의 색상
    public Color unlockedColor = Color.yellow; // 해금 상태일 때의 색상

    [Header("업적 이름")]
    public TextMeshProUGUI titleText;

    [Header("업적 설명")]
    public TextMeshProUGUI descriptionText;

    private void Start()
    {
        titleText.text = linkedAchievement.achievementName;
        descriptionText.text = linkedAchievement.description;
    }

    private void OnEnable()
    {
        // AchievementManager의 이벤트에 구독합니다.
        AchievementManager.OnAchievementUnlocked += HandleAchievementUnlocked;

        // 초기 상태 설정
        if (imageToChange != null && linkedAchievement != null)
        {
            imageToChange.color = linkedAchievement.IsUnlocked ? unlockedColor : lockedColor;
        }
    }

    private void OnDisable()
    {
        // 이벤트 구독을 해지합니다.
        AchievementManager.OnAchievementUnlocked -= HandleAchievementUnlocked;
    }

    // 업적 해금 이벤트가 발생했을 때 호출됩니다.
    private void HandleAchievementUnlocked(AchievementData unlockedData)
    {
        // 이벤트로 전달된 업적 데이터가 현재 이 UI가 연결된 업적 데이터와 일치하는지 확인합니다.
        if (unlockedData == linkedAchievement)
        {
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if (imageToChange != null)
        {
            // 이미지 색상을 해금 상태 색상으로 변경
            imageToChange.color = unlockedColor;

            // 해금 시 애니메이션, 소리 재생 등의 추가 로직을 여기에 삽입
            Debug.Log($"UI 업데이트: {linkedAchievement.achievementName} 해금됨.");
        }
    }
}