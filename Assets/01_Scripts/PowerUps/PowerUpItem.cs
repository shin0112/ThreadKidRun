using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameName.Data;
using GameName.Managers;

namespace GameName.PowerUps
{
    /// <summary>
    /// 필드에 배치되는 파워업 아이템
    /// 플레이어가 충돌하면 파워업을 활성화합니다.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class PowerUpItem : MonoBehaviour
    {
        #region Fields

        [Header("=== PowerUp Settings ===")]
        [Tooltip("이 아이템의 파워업 타입")]
        [SerializeField] private PowerUpType powerUpType;

        [Header("=== Visual Settings ===")]
        [Tooltip("Y축 회전 속도")]
        [SerializeField] private float rotationSpeed = 100f;

        [Tooltip("위아래 움직임 활성화")]
        [SerializeField] private bool enableBobbing = true;

        [Tooltip("위아래 움직임 높이")]
        [SerializeField] private float bobbingHeight = 0.3f;

        [Tooltip("위아래 움직임 속도")]
        [SerializeField] private float bobbingSpeed = 2f;

        // 위아래 움직임을 위한 초기 위치
        private Vector3 startPosition;

        [Header("=== Sound Settings ===")]
        [Tooltip("아이템 획득 시 재생할 사운드 이름")]
        [SerializeField] private string pickupSoundName = "SFX_CoinGet";

        [Header("=== Particle Effect ===")]
        [Tooltip("획득 시 생성할 파티클 효과 (옵션)")]
        [SerializeField] private GameObject pickupParticle;

        #endregion

        #region Initialization

        private void Start()
        {
            // 초기 위치 저장 (위아래 움직임 기준점)
            startPosition = transform.position;

            // Collider를 Trigger로 설정 확인
            Collider col = GetComponent<Collider>();
            if (col != null)
            {
                col.isTrigger = true;
            }
            else
            {
                Debug.LogWarning($"[PowerUpItem] {gameObject.name}에 Collider가 없습니다!");
            }
        }

        #endregion

        #region Update - Visual Effects

        private void Update()
        {
            // 1. Y축 회전 (빙글빙글)
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

            // 2. 위아래 움직임 (둥실둥실)
            if (enableBobbing)
            {
                float newY = startPosition.y + Mathf.Sin(Time.time * bobbingSpeed) * bobbingHeight;
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            }
        }

        #endregion

        #region Collision Detection

        /// <summary>
        /// 플레이어와 충돌 시 파워업 활성화
        /// </summary>
        private void OnTriggerEnter(Collider other)
        {
            // 플레이어 태그 확인
            if (other.CompareTag("Player"))
            {
                // 파워업 획득 처리
                PickupPowerUp();
                GameManager.Instance.EarnItem(1); //업적 해금을 위한 획득 정보를 알림
            }
        }

        #endregion

        #region Pickup Logic

        /// <summary>
        /// 파워업 획득 처리
        /// </summary>
        private void PickupPowerUp()
        {
            // 1. PowerUpManager를 통해 파워업 활성화
            if (PowerUpManager.Instance != null)
            {
                PowerUpManager.Instance.ActivatePowerUp(powerUpType);
                Debug.Log($"[PowerUpItem] {powerUpType} 파워업 획득!");
            }
            else
            {
                Debug.LogError("[PowerUpItem] PowerUpManager가 씬에 없습니다!");
            }

            // 2. 획득 사운드 재생 (SFX_CoinGet)
            PlayPickupSound();

            // 3. 파티클 효과 생성 (있으면)
            SpawnPickupParticle();

            // 4. 아이템 파괴
            Destroy(gameObject);
        }

        #endregion

        #region Audio & Effects

        /// <summary>
        /// 획득 사운드 재생 (SFX_CoinGet)
        /// </summary>
        private void PlayPickupSound()
        {
            if (AudioManager.Instance != null && !string.IsNullOrEmpty(pickupSoundName))
            {
                AudioManager.Instance.PlaySFX(pickupSoundName);
            }
        }

        /// <summary>
        /// 획득 파티클 효과 생성
        /// </summary>
        private void SpawnPickupParticle()
        {
            if (pickupParticle != null)
            {
                Instantiate(pickupParticle, transform.position, Quaternion.identity);
            }
        }

        #endregion

        #region Editor Helper

#if UNITY_EDITOR
        /// <summary>
        /// 에디터에서 아이템 범위 시각화
        /// </summary>
        private void OnDrawGizmos()
        {
            // 파워업 타입별로 다른 색상으로 기즈모 표시
            Gizmos.color = powerUpType switch
            {
                PowerUpType.Invincibility => Color.yellow,
                PowerUpType.SpeedBoost => Color.cyan,
                PowerUpType.Magnet => Color.magenta,
                PowerUpType.HighJump => Color.green,
                _ => Color.white
            };

            Gizmos.DrawWireSphere(transform.position, 0.5f);
        }
#endif

        #endregion
    }
}
