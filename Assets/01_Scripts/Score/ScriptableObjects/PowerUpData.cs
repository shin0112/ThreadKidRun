using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameName.Data
{
    /// 파워업 아이템의 모든 데이터를 담는 ScriptableObject
    [CreateAssetMenu(fileName = "NewPowerUp", menuName = "Game/PowerUp Data", order = 1)]
    public class PowerUpData : ScriptableObject
    {
        [Header("=== 기본 정보 ===")]
        [Tooltip("파워업 이름")]
        public string powerUpName;

        [Tooltip("파워업 타입")]
        public PowerUpType powerUpType;

        [Header("=== 효과 설정 ===")]
        [Tooltip("효과 지속 시간 (초)")]
        [Range(1f, 30f)]
        public float duration = 5f;

        [Tooltip("효과 값 (속도 배율, 점프 높이 등)")]
        [Range(0.1f, 10f)]
        public float effectValue = 1.5f;

        [Header("=== 시각/사운드 ===")]
        [Tooltip("UI에 표시될 아이콘")]
        public Sprite icon;

        [Tooltip("파워업 활성화 사운드")]
        public SoundData activationSound;

        [Tooltip("파워업 이펙트 프리팹")]
        public GameObject particleEffect;

        [Header("=== 설명 ===")]
        [TextArea(3, 5)]
        [Tooltip("파워업 설명 (선택사항)")]
        public string description;
    }

    /// 파워업 타입 열거형
    public enum PowerUpType
    {
        Invincibility,  // 무적
        SpeedBoost,     // 속도 증가
        Magnet,         // 자석 (아이템 자동 수집)
        HighJump        // 높은 점프
    }
}
