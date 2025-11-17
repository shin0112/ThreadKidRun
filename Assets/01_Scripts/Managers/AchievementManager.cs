using UnityEngine;
using System.Collections.Generic;
using System;

public class AchievementManager : MonoBehaviour
{
    // 업적이 해금되었을 때 UI에 알리기 위한 이벤트
    public static event Action<AchievementData> OnAchievementUnlocked;

    [Tooltip("관리할 모든 AchievementData ScriptableObject 목록")]
    public List<AchievementData> allAchievements;

    // GameManager에서 호출되어 현재 습득한/사용된 개수를 확인
    public void CheckAchievements(int currentCount)
    {
        foreach (var achievement in allAchievements)
        {
            // 이미 해금된 업적은 다시 검사할 필요가 없음
            if (achievement.IsUnlocked)
            {
                continue;
            }

            // 해금 요구량을 충족하면 업적을 해금
            if (currentCount >= achievement.requiredCount)
            {
                achievement.Unlock();

                // UI 업데이트를 위해 이벤트 발생
                OnAchievementUnlocked?.Invoke(achievement);
            }
        }
    }
}