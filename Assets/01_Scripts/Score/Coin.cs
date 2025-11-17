using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    [Tooltip("이 코인을 획득했을 때 증가할 점수")]
    public int coinValue = 1;

    private void OnCollisionEnter(Collision collision)
    {
        // 플레이어 태그와 충돌했는지 확인 (플레이어에게 "Player" 태그가 있어야 합니다.)
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.EarnCoin(coinValue);
            }
            Destroy(gameObject);
        }
    }
}
