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

        // 플레이어 컨트롤러 스크립트 참조 (나중에 추가 예정)
        // private PlayerController playerController;

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

            // 나중에 PlayerController 스크립트 가져오기
            // if (player != null)
            // {
            //     playerController = player.GetComponent<PlayerController>();
            // }

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

            // TODO: 팀원이 PlayerController 완성하면 주석 해제
            // if (playerController != null)
            // {
            //     originalSpeed = playerController.moveSpeed;
            //     playerController.moveSpeed *= powerUpData.effectValue;
            // }

            Debug.Log($"[SpeedBoostPowerUp] 속도 부스트 활성화! (배율: {powerUpData.effectValue}x)");

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

            // TODO: 팀원이 PlayerController 완성하면 주석 해제
            // if (playerController != null)
            // {
            //     playerController.moveSpeed = originalSpeed;
            // }

            Debug.Log("[SpeedBoostPowerUp] 속도 부스트 해제! 원래 속도로 복구됩니다.");

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
