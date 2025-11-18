using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameName.Data;
using GameName.Managers;

namespace GameName.PowerUps
{
    /// <summary>
    /// 모든 파워업의 기본 클래스
    /// 각 파워업은 이 클래스를 상속받아 Activate/Deactivate 구현
    /// </summary>
    public abstract class PowerUpBase : MonoBehaviour
    {
        #region Fields

        [Header("=== PowerUp Data ===")]
        [Tooltip("이 파워업의 데이터 (ScriptableObject)")]
        [SerializeField] protected PowerUpData powerUpData;

        // 파워업 활성화 상태
        protected bool isActive = false;

        // 현재 진행 중인 코루틴 참조
        protected Coroutine powerUpCoroutine;

        #endregion

        #region Initialization

        protected virtual void Awake()
        {
            // PowerUpData 검증
            if (powerUpData == null)
            {
                Debug.LogError($"[PowerUpBase] {gameObject.name}에 PowerUpData가 연결되지 않았습니다!");
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 파워업 실행 (외부에서 호출)
        /// </summary>
        public void ExecutePowerUp()
        {
            // 이미 활성화 중이면 중복 실행 방지
            if (isActive)
            {
                Debug.LogWarning($"[PowerUpBase] {powerUpData.powerUpName}이 이미 활성화 중입니다!");
                return;
            }

            // 코루틴으로 타이머 시작
            powerUpCoroutine = StartCoroutine(PowerUpRoutine());
        }

        #endregion

        #region Coroutine

        /// <summary>
        /// 파워업 타이머 코루틴
        /// </summary>
        private System.Collections.IEnumerator PowerUpRoutine()
        {
            // 활성화 시작
            isActive = true;

            // 사운드 재생
            if (powerUpData.activationSound != null && AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX(powerUpData.activationSound.soundName);
            }

            // 파티클 효과 생성 (있으면)
            if (powerUpData.particleEffect != null)
            {
                Instantiate(powerUpData.particleEffect, transform.position, Quaternion.identity);
            }

            // 효과 활성화 (자식 클래스 구현)
            Activate();

            Debug.Log($"[PowerUpBase] {powerUpData.powerUpName} 활성화! (지속시간: {powerUpData.duration}초)");

            // 지속 시간만큼 대기
            yield return new WaitForSeconds(powerUpData.duration);

            // 효과 비활성화 (자식 클래스 구현)
            Deactivate();

            Debug.Log($"[PowerUpBase] {powerUpData.powerUpName} 비활성화!");

            // 활성화 상태 해제
            isActive = false;
            powerUpCoroutine = null;
        }

        #endregion

        #region Abstract Methods

        /// <summary>
        /// 파워업 효과 활성화 (자식 클래스에서 구현)
        /// </summary>
        protected abstract void Activate();

        /// <summary>
        /// 파워업 효과 비활성화 (자식 클래스에서 구현)
        /// </summary>
        protected abstract void Deactivate();

        #endregion

        #region Optional Methods

        /// <summary>
        /// 파워업 강제 중단 (예: 플레이어 사망 시)
        /// </summary>
        public void ForceStop()
        {
            if (powerUpCoroutine != null)
            {
                StopCoroutine(powerUpCoroutine);
                powerUpCoroutine = null;
            }

            if (isActive)
            {
                Deactivate();
                isActive = false;
                Debug.Log($"[PowerUpBase] {powerUpData.powerUpName} 강제 중단!");
            }
        }

        #endregion
    }
}
