using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using GameName.Managers;

public class Coin : MonoBehaviour
{
    [Tooltip("이 코인을 획득했을 때 증가할 점수")]
    public int coinValue = 1;

    private void OnCollisionEnter(Collision collision)
    {
        // 플레이어 태그와 충돌했는지 확인 (플레이어에게 "Player" 태그가 있어야 합니다.)
        if (collision.gameObject.CompareTag("Player"))
        {
            // 코인획득시 효과음 재생
            // ============================================
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX("SFX_CoinGet");
            }
            // ============================================
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(coinValue);
            }
            Destroy(gameObject);
        }
    }
}
