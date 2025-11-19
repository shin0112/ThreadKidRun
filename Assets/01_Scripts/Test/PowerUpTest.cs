using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameName.Data;
using GameName.Managers;

/// <summary>
/// 파워업 테스트용 임시 스크립트
/// 키보드 입력으로 파워업 활성화
/// </summary>
public class PowerUpTest : MonoBehaviour
{
    private void Update()
    {
        // I 키: 무적 파워업
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (PowerUpManager.Instance != null)
            {
                PowerUpManager.Instance.ActivatePowerUp(PowerUpType.Invincibility);
            }
        }

        // S 키: 스피드 부스트
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (PowerUpManager.Instance != null)
            {
                PowerUpManager.Instance.ActivatePowerUp(PowerUpType.SpeedBoost);
            }
        }
    }
}
