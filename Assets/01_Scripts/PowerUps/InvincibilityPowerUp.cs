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

        // ============================================
        // PlayerCollider 스크립트 참조 추가
        // ============================================
        private PlayerCollider playerCollider;

        // ============================================
        // 원래 레이어 저장 (복구용)
        // ============================================
        private int originalLayer;
        private List<int> originalChildLayers = new List<int>();

        #endregion

        #region Initialization

        protected override void Awake()
        {
            base.Awake(); // 부모 클래스 Awake 호출
        }

        public void Init(GameObject playerGo)
        {
            // 플레이어 자동 찾기 (연결 안 되어 있으면)
            if (player == null)
            {
                player = playerGo;

                if (player == null)
                {
                    Debug.LogError("[InvincibilityPowerUp] Player 태그를 가진 오브젝트를 찾을 수 없습니다!");
                }
            }

            // ============================================
            // PlayerCollider 스크립트 찾기
            // GetComponentInChildren 사용으로 자식에서도 찾기
            // ============================================
            if (player != null)
            {
                playerCollider = player.GetComponent<PlayerCollider>();

                if (playerCollider == null)
                {
                    Debug.LogWarning("[InvincibilityPowerUp] PlayerCollider 스크립트를 찾을 수 없습니다!");
                }
                else
                {
                    Debug.Log("[InvincibilityPowerUp] PlayerCollider 스크립트 연결 완료!");
                }
            }
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

            // ============================================
            // PlayerCollider의 무적 플래그 활성화
            // ============================================
            if (playerCollider != null)
            {
                playerCollider.isInvincible = true;
                Debug.Log("[InvincibilityPowerUp] PlayerCollider 무적 플래그 활성화!");
            }

            // ============================================
            // Player와 모든 자식 오브젝트의 레이어 변경
            // ============================================
            ChangeLayerRecursively(player, "InvinciblePlayer");

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

            // ============================================
            // PlayerCollider의 무적 플래그 비활성화
            // ============================================
            if (playerCollider != null)
            {
                playerCollider.isInvincible = false;
                Debug.Log("[InvincibilityPowerUp] PlayerCollider 무적 플래그 비활성화!");
            }

            // ============================================
            // Player와 모든 자식 오브젝트의 레이어 복구
            // ============================================
            RestoreLayerRecursively(player);

            Debug.Log("[InvincibilityPowerUp] 무적 해제! 정상 충돌로 복구됩니다.");

            // 시각적 피드백 복구
            ResetPlayerColor();
        }

        #endregion

        #region Layer Management

        /// <summary>
        /// 오브젝트와 모든 자식의 레이어를 재귀적으로 변경
        /// </summary>
        private void ChangeLayerRecursively(GameObject obj, string layerName)
        {
            if (obj == null) return;

            // 원래 레이어 저장
            originalChildLayers.Clear();
            originalLayer = obj.layer;

            // 현재 오브젝트 레이어 변경
            int newLayer = LayerMask.NameToLayer(layerName);
            if (newLayer == -1)
            {
                Debug.LogWarning($"[InvincibilityPowerUp] '{layerName}' 레이어를 찾을 수 없습니다! 기본 레이어를 사용합니다.");
                // InvinciblePlayer 레이어가 없으면 Default 레이어 사용
                newLayer = 0;
            }

            obj.layer = newLayer;

            // 모든 자식 오브젝트의 레이어도 변경
            foreach (Transform child in obj.GetComponentsInChildren<Transform>(true))
            {
                originalChildLayers.Add(child.gameObject.layer);
                child.gameObject.layer = newLayer;
            }

            Debug.Log($"[InvincibilityPowerUp] {obj.name}과 {originalChildLayers.Count}개의 자식 오브젝트 레이어 변경 완료!");
        }

        /// <summary>
        /// 오브젝트와 모든 자식의 레이어를 원래대로 복구
        /// </summary>
        private void RestoreLayerRecursively(GameObject obj)
        {
            if (obj == null) return;

            // 현재 오브젝트 레이어 복구
            obj.layer = originalLayer;

            // 모든 자식 오브젝트의 레이어 복구
            Transform[] children = obj.GetComponentsInChildren<Transform>(true);
            for (int i = 0; i < children.Length && i < originalChildLayers.Count; i++)
            {
                children[i].gameObject.layer = originalChildLayers[i];
            }

            originalChildLayers.Clear();
            Debug.Log($"[InvincibilityPowerUp] {obj.name}과 자식 오브젝트들의 레이어 복구 완료!");
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
