using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameName.Data
{
    /// 게임의 모든 사운드 데이터를 담는 ScriptableObject
    /// BGM과 SFX를 구분하여 관리합니다.
    [CreateAssetMenu(fileName = "NewSoundData", menuName = "Game/Sound Data", order = 0)]
    public class SoundData : ScriptableObject
    {
        [Header("=== 기본 정보 ===")]
        [Tooltip("사운드를 식별할 고유한 이름")]
        public string soundName;

        [Tooltip("BGM(배경음악) 또는 SFX(효과음) 선택")]
        public SoundType soundType;

        [Header("=== 오디오 설정 ===")]
        [Tooltip("재생할 오디오 클립")]
        public AudioClip audioClip;

        [Tooltip("재생 볼륨 (0 ~ 1)")]
        [Range(0f, 1f)]
        public float volume = 1f;

        [Tooltip("반복 재생 여부")]
        public bool loop = false;

        [Tooltip("재생 속도 (0.5 ~ 2)")]
        [Range(0.5f, 2f)]
        public float pitch = 1f;
    }

    /// 사운드 타입 열거형
    public enum SoundType
    {
        BGM,    // 배경음악
        SFX     // 효과음
    }
}
