using GameName.Data;
using GameName.PowerUps;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameName.Managers
{
    /// <summary>
    /// 모든 파워업을 관리하는 싱글톤 매니저
    /// 파워업 활성화/비활성화 및 상태 추적
    /// </summary>
    public class PowerUpManager : MonoBehaviour
    {
        #region Singleton

        public static PowerUpManager Instance { get; private set; }

        private void Awake()
        {
            // 싱글톤 패턴 구현
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion

        #region Fields

        [Header("=== PowerUp Components ===")]
        [Tooltip("플레이어 오브젝트")]
        [SerializeField] private GameObject player;

        [Tooltip("무적 파워업 컴포넌트")]
        [SerializeField] private InvincibilityPowerUp invincibilityPowerUp;

        [Tooltip("스피드 부스트 파워업 컴포넌트")]
        [SerializeField] private SpeedBoostPowerUp speedBoostPowerUp;

        // 파워업 타입별 매핑 (빠른 검색용)
        private Dictionary<PowerUpType, PowerUpBase> powerUpDictionary;

        // 현재 활성화된 파워업 목록
        private HashSet<PowerUpType> activePowerUps = new HashSet<PowerUpType>();

        #endregion

        #region Events

        /// <summary>
        /// 파워업 활성화 시 발생하는 이벤트
        /// UI에서 구독하여 아이콘 표시 등에 사용
        /// </summary>
        public event Action<PowerUpType, float> OnPowerUpActivated;

        /// <summary>
        /// 파워업 비활성화 시 발생하는 이벤트
        /// UI에서 구독하여 아이콘 제거 등에 사용
        /// </summary>
        public event Action<PowerUpType> OnPowerUpDeactivated;

        #endregion

        #region Initialization

        private void Start()
        {
            InitializePowerUps();
        }

        /// <summary>
        /// 파워업 Dictionary 초기화
        /// </summary>
        private void InitializePowerUps()
        {
            powerUpDictionary = new Dictionary<PowerUpType, PowerUpBase>();

            // 무적 파워업 등록
            if (invincibilityPowerUp != null)
            {
                powerUpDictionary.Add(PowerUpType.Invincibility, invincibilityPowerUp);
            }
            else
            {
                Debug.LogWarning("[PowerUpManager] InvincibilityPowerUp이 연결되지 않았습니다!");
            }

            // 스피드 부스트 등록
            if (speedBoostPowerUp != null)
            {
                powerUpDictionary.Add(PowerUpType.SpeedBoost, speedBoostPowerUp);
            }
            else
            {
                Debug.LogWarning("[PowerUpManager] SpeedBoostPowerUp이 연결되지 않았습니다!");
            }

            Debug.Log($"[PowerUpManager] {powerUpDictionary.Count}개의 파워업 초기화 완료!");
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 파워업 활성화 (외부에서 호출)
        /// </summary>
        /// <param name="powerUpType">활성화할 파워업 타입</param>
        public void ActivatePowerUp(PowerUpType powerUpType)
        {
            // Dictionary에서 파워업 찾기
            if (!powerUpDictionary.TryGetValue(powerUpType, out PowerUpBase powerUp))
            {
                Debug.LogError($"[PowerUpManager] {powerUpType} 파워업을 찾을 수 없습니다!");
                return;
            }

            // 파워업 실행
            powerUp.ExecutePowerUp();

            // 활성화 목록에 추가
            activePowerUps.Add(powerUpType);

            // UI 이벤트 발생
            OnPowerUpActivated?.Invoke(powerUpType, powerUp.GetDuration());

            Debug.Log($"[PowerUpManager] {powerUpType} 파워업 활성화!");
        }

        /// <summary>
        /// 파워업 비활성화 알림 (PowerUpBase에서 호출)
        /// </summary>
        /// <param name="powerUpType">비활성화된 파워업 타입</param>
        public void NotifyPowerUpDeactivated(PowerUpType powerUpType)
        {
            // 활성화 목록에서 제거
            activePowerUps.Remove(powerUpType);

            // UI 이벤트 발생
            OnPowerUpDeactivated?.Invoke(powerUpType);

            Debug.Log($"[PowerUpManager] {powerUpType} 파워업 비활성화!");
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// 특정 파워업이 활성화 중인지 확인
        /// </summary>
        public bool IsPowerUpActive(PowerUpType powerUpType)
        {
            return activePowerUps.Contains(powerUpType);
        }

        /// <summary>
        /// 현재 활성화된 모든 파워업 가져오기
        /// </summary>
        public HashSet<PowerUpType> GetActivePowerUps()
        {
            return new HashSet<PowerUpType>(activePowerUps);
        }

        /// <summary>
        /// 모든 파워업 강제 중단 (게임오버 시 사용)
        /// </summary>
        public void StopAllPowerUps()
        {
            foreach (var powerUp in powerUpDictionary.Values)
            {
                powerUp.ForceStop();
            }

            activePowerUps.Clear();

            Debug.Log("[PowerUpManager] 모든 파워업 강제 중단!");
        }

        #endregion
    }
}
