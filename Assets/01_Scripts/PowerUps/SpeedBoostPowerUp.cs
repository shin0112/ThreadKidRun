using UnityEngine;

namespace GameName.PowerUps
{
    /// <summary>
    /// 스피드 부스트 파워업: 맵 이동 속도 증가 (게임 진행 속도 증가)
    /// </summary>
    public class SpeedBoostPowerUp : PowerUpBase
    {
        #region Fields

        [Header("=== Speed Boost Settings ===")]
        [Tooltip("속도 부스트를 적용할 플레이어 (시각 효과용)")]
        [SerializeField] private GameObject player;

        // ============================================
        // MapMove 스크립트 참조 - 맵 이동 속도 제어
        // ============================================
        private MapMove mapMove;

        // 원래 맵 속도를 저장 (복구용)
        private float originalMapSpeed;

        [Header("=== Optional Effects ===")]
        [Tooltip("속도 부스트 시 활성화할 트레일 효과")]
        [SerializeField] private TrailRenderer trailRenderer;

        #endregion

        #region Initialization

        protected override void Awake()
        {
            base.Awake(); // 부모 클래스 Awake 호출

            // 플레이어 자동 찾기 (시각 효과용)
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");

                if (player == null)
                {
                    Debug.LogWarning("[SpeedBoostPowerUp] Player 태그를 가진 오브젝트를 찾을 수 없습니다!");
                }
            }

            // ============================================
            // MapMove 스크립트 찾기 - 맵 이동 속도 제어를 위해
            // ============================================
            mapMove = FindObjectOfType<MapMove>();

            if (mapMove == null)
            {
                Debug.LogError("[SpeedBoostPowerUp] MapMove 스크립트를 찾을 수 없습니다! 스피드 부스트가 작동하지 않습니다!");
            }
            else
            {
                Debug.Log("[SpeedBoostPowerUp] MapMove 스크립트 연결 완료!");
            }

            // 트레일 자동 찾기
            if (trailRenderer == null && player != null)
            {
                trailRenderer = player.GetComponentInChildren<TrailRenderer>();
            }
        }

        public void Init(GameObject playerGo)
        {
            player = playerGo;
            mapMove = FindObjectOfType<MapMove>();
        }

        #endregion

        #region PowerUpBase Implementation

        /// <summary>
        /// 속도 부스트 효과 활성화 - 맵 이동 속도 증가
        /// </summary>
        protected override void Activate()
        {
            // ============================================
            // MapMove의 speed 증가 (맵이 다가오는 속도 증가)
            // ============================================
            if (mapMove != null)
            {
                originalMapSpeed = mapMove.speed;
                mapMove.speed *= powerUpData.effectValue;

                Debug.Log($"[SpeedBoostPowerUp] 맵 속도 증가! 원래 속도: {originalMapSpeed} → 새 속도: {mapMove.speed} (배율: {powerUpData.effectValue}x)");
            }
            else
            {
                Debug.LogError("[SpeedBoostPowerUp] MapMove가 없어 속도 부스트를 적용할 수 없습니다!");
                return;
            }

            // 시각적 피드백
            if (player != null)
            {
                ChangePlayerColor(Color.cyan);
                EnableTrailEffect();
            }
        }

        /// <summary>
        /// 속도 부스트 효과 비활성화 - 원래 맵 속도로 복구
        /// </summary>
        protected override void Deactivate()
        {
            // ============================================
            // MapMove의 speed 복구
            // ============================================
            if (mapMove != null)
            {
                mapMove.speed = originalMapSpeed;
                Debug.Log($"[SpeedBoostPowerUp] 맵 속도 복구! 원래 속도: {originalMapSpeed}");
            }

            // 시각적 피드백 복구
            if (player != null)
            {
                ResetPlayerColor();
                DisableTrailEffect();
            }
        }

        #endregion

        #region Visual Feedback

        /// <summary>
        /// 플레이어 색상 변경 (속도 부스트 표시)
        /// </summary>
        private void ChangePlayerColor(Color color)
        {
            if (player == null) return;

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
