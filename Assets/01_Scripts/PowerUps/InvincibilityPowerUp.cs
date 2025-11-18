using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameName.PowerUps
{
    /// <summary>
    /// 무적 파워업: 장애물과 충돌해도 게임오버되지 않음
    /// </summary>
    public class InvincibilityPowerUp : PowerUpBase
    {
        #region Fields

        [Header("=== Invincibility Settings ===")]
        [Tooltip("무적 효과를 적용할 플레이어")]
        [SerializeField] private GameObject player;

        // 플레이어 컨트롤러 스크립트 참조 (나중에 추가 예정)
        // private PlayerController playerController;

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
                    Debug.LogError("[InvincibilityPowerUp] Player 태그를 가진 오브젝트를 찾을 수 없습니다!");
                }
            }

            // 나중에 PlayerController 스크립트 가져오기
            // if (player != null)
            // {
            //     playerController = player.GetComponent<PlayerController>();
            // }
        }

        #endregion

        #region PowerUpBase Implementation

        /// <summary>
        /// 무적 효과 활성화
        /// </summary>
        protected override void Activate()
        {
            if (player == null)
            {
                Debug.LogError("[InvincibilityPowerUp] 플레이어가 연결되지 않아 무적을 활성화할 수 없습니다!");
                return;
            }

            // TODO: 팀원이 PlayerController 완성하면 주석 해제
            // if (playerController != null)
            // {
            //     playerController.SetInvincible(true);
            // }

            // 임시: 레이어 변경으로 충돌 무시 (대안 방법)
            player.layer = LayerMask.NameToLayer("InvinciblePlayer");

            Debug.Log("[InvincibilityPowerUp] 무적 활성화! 플레이어가 장애물을 무시합니다.");

            // 시각적 피드백 (옵션)
            ChangePlayerColor(Color.yellow);
        }

        /// <summary>
        /// 무적 효과 비활성화
        /// </summary>
        protected override void Deactivate()
        {
            if (player == null) return;

            // TODO: 팀원이 PlayerController 완성하면 주석 해제
            // if (playerController != null)
            // {
            //     playerController.SetInvincible(false);
            // }

            // 임시: 레이어 원래대로 복구
            player.layer = LayerMask.NameToLayer("Player");

            Debug.Log("[InvincibilityPowerUp] 무적 해제! 정상 충돌로 복구됩니다.");

            // 시각적 피드백 복구
            ResetPlayerColor();
        }

        #endregion

        #region Visual Feedback

        /// <summary>
        /// 플레이어 색상 변경 (무적 표시)
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
    }
}
