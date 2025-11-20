using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameName.PowerUps
{
    /// <summary>
    /// 스피드 부스트 파워업: 플레이어 이동 속도 증가
    /// </summary>
    public class SpeedBoostPowerUp : PowerUpBase
    {
        #region Fields

        [Header("=== Speed Boost Settings ===")]
        [Tooltip("속도 부스트를 적용할 플레이어")]
        [SerializeField] private GameObject player;

        // ============================================
        // PlayerAnimation 스크립트 참조 추가
        // ============================================
        private PlayerAnimation playerAnimation;

        // 원래 속도를 저장 (복구용)
        private float originalSpeed;

        [Header("=== Optional Effects ===")]
        [Tooltip("속도 부스트 시 활성화할 트레일 효과")]
        [SerializeField] private TrailRenderer trailRenderer;

        #endregion

        #region Initialization

        protected override void Awake()
        {
            base.Awake(); // 부모 클래스 Awake 호출

            // 플레이어 자동 찾기 (연결 안 되어 있으면)
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");

                if (player == null)
                {
                    Debug.LogError("[SpeedBoostPowerUp] Player 태그를 가진 오브젝트를 찾을 수 없습니다!");
                }
            }

            // ============================================
            // PlayerAnimation 스크립트 찾기
            // GetComponentInChildren 사용으로 자식에서도 찾기
            // ============================================
            if (player != null)
            {
                playerAnimation = player.GetComponentInChildren<PlayerAnimation>();

                if (playerAnimation == null)
                {
                    Debug.LogError("[SpeedBoostPowerUp] PlayerAnimation 스크립트를 찾을 수 없습니다!");
                }
                else
                {
                    Debug.Log("[SpeedBoostPowerUp] PlayerAnimation 스크립트 연결 완료!");
                }
            }

            // 트레일 자동 찾기
            if (trailRenderer == null && player != null)
            {
                trailRenderer = player.GetComponentInChildren<TrailRenderer>();
            }
        }

        #endregion

        #region PowerUpBase Implementation

        /// <summary>
        /// 속도 부스트 효과 활성화
        /// </summary>
        protected override void Activate()
        {
            if (player == null)
            {
                Debug.LogError("[SpeedBoostPowerUp] 플레이어가 연결되지 않아 속도 부스트를 활성화할 수 없습니다!");
                return;
            }

            // ============================================
            // PlayerAnimation의 moveSpeed 증가
            // ============================================
            if (playerAnimation != null)
            {
                originalSpeed = playerAnimation.moveSpeed;
                playerAnimation.moveSpeed *= powerUpData.effectValue;

                Debug.Log($"[SpeedBoostPowerUp] 속도 부스트 활성화! 원래 속도: {originalSpeed} → 새 속도: {playerAnimation.moveSpeed} (배율: {powerUpData.effectValue}x)");
            }
            else
            {
                Debug.LogWarning("[SpeedBoostPowerUp] PlayerAnimation이 없어 속도 변경을 건너뜁니다.");
            }

            // 시각적 피드백
            ChangePlayerColor(Color.cyan);
            EnableTrailEffect();
        }

        /// <summary>
        /// 속도 부스트 효과 비활성화
        /// </summary>
        protected override void Deactivate()
        {
            if (player == null) return;

            // ============================================
            // PlayerAnimation의 moveSpeed 복구
            // ============================================
            if (playerAnimation != null)
            {
                playerAnimation.moveSpeed = originalSpeed;
                Debug.Log($"[SpeedBoostPowerUp] 속도 부스트 해제! 원래 속도로 복구: {originalSpeed}");
            }

            // 시각적 피드백 복구
            ResetPlayerColor();
            DisableTrailEffect();
        }

        #endregion

        #region Visual Feedback

        /// <summary>
        /// 플레이어 색상 변경 (속도 부스트 표시)
        /// </summary>
        private void ChangePlayerColor(Color color)
        {
            Renderer renderer = player.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = color;
            }

            // 자식 오브젝트도 색상 변경
            Renderer[] childRenderers = player.GetComponentsInChildren<Renderer>();
            foreach (Renderer childRenderer in childRenderers)
            {
                childRenderer.material.color = color;
            }
        }

        /// <summary>
        /// 플레이어 색상 원래대로 복구
        /// </summary>
        private void ResetPlayerColor()
        {
            ChangePlayerColor(Color.white);
        }

        #endregion

        #region Optional Trail Effect

        /// <summary>
        /// 트레일 효과 활성화
        /// </summary>
        private void EnableTrailEffect()
        {
            if (trailRenderer != null)
            {
                trailRenderer.enabled = true;
                trailRenderer.startColor = Color.cyan;
                trailRenderer.endColor = new Color(0, 1, 1, 0); // 투명한 cyan
            }
        }

        /// <summary>
        /// 트레일 효과 비활성화
        /// </summary>
        private void DisableTrailEffect()
        {
            if (trailRenderer != null)
            {
                trailRenderer.enabled = false;
            }
        }

        #endregion
    }
}
