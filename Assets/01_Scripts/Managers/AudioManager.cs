using GameName.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameName.Managers
{
    /// <summary>
    /// 게임의 모든 사운드를 관리하는 싱글톤 매니저
    /// BGM과 SFX를 재생하고 볼륨을 조절합니다.
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        #region Singleton

        public static AudioManager Instance { get; private set; }

        private void Awake()
        {
            // 싱글톤 패턴 구현
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeAudioManager();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion

        #region Audio Sources

        [Header("=== Audio Sources ===")]
        [Tooltip("BGM 재생용 AudioSource")]
        [SerializeField] private AudioSource bgmSource;

        [Tooltip("SFX 재생용 AudioSource")]
        [SerializeField] private AudioSource sfxSource;

        #endregion

        #region Sound Data

        [Header("=== Sound Data ===")]
        [Tooltip("게임에서 사용할 모든 SoundData 목록")]
        [SerializeField] private List<SoundData> soundDataList = new List<SoundData>();

        // 빠른 검색을 위한 Dictionary
        private Dictionary<string, SoundData> soundDictionary;

        #endregion

        #region Volume Settings

        [Header("=== Volume Settings ===")]
        [Range(0f, 1f)]
        [SerializeField] private float masterVolume = 1f;

        [Range(0f, 1f)]
        [SerializeField] private float bgmVolume = 0.8f;

        [Range(0f, 1f)]
        [SerializeField] private float sfxVolume = 1f;

        #endregion

        #region Initialization

        /// <summary>
        /// AudioManager 초기화
        /// </summary>
        private void InitializeAudioManager()
        {
            // AudioSource가 없으면 자동 생성
            CreateAudioSources();

            // SoundData를 Dictionary로 변환
            InitializeSoundDictionary();

            // 저장된 볼륨 설정 로드
            LoadVolumeSettings();

            Debug.Log("[AudioManager] 초기화 완료!");
        }

        /// <summary>
        /// AudioSource 컴포넌트 생성
        /// </summary>
        private void CreateAudioSources()
        {
            // BGM AudioSource
            if (bgmSource == null)
            {
                GameObject bgmObject = new GameObject("BGM_AudioSource");
                bgmObject.transform.SetParent(transform);
                bgmSource = bgmObject.AddComponent<AudioSource>();
                bgmSource.playOnAwake = false;
                bgmSource.loop = true; // BGM은 기본적으로 루프
            }

            // SFX AudioSource
            if (sfxSource == null)
            {
                GameObject sfxObject = new GameObject("SFX_AudioSource");
                sfxObject.transform.SetParent(transform);
                sfxSource = sfxObject.AddComponent<AudioSource>();
                sfxSource.playOnAwake = false;
                sfxSource.loop = false;
            }
        }

        /// <summary>
        /// SoundData 리스트를 Dictionary로 변환
        /// </summary>
        private void InitializeSoundDictionary()
        {
            soundDictionary = new Dictionary<string, SoundData>();

            foreach (SoundData data in soundDataList)
            {
                if (data == null)
                {
                    Debug.LogWarning("[AudioManager] SoundData 리스트에 null 값이 있습니다!");
                    continue;
                }

                if (string.IsNullOrEmpty(data.soundName))
                {
                    Debug.LogWarning($"[AudioManager] SoundData에 이름이 없습니다: {data.name}");
                    continue;
                }

                if (soundDictionary.ContainsKey(data.soundName))
                {
                    Debug.LogWarning($"[AudioManager] 중복된 사운드 이름: {data.soundName}");
                    continue;
                }

                soundDictionary.Add(data.soundName, data);
            }

            Debug.Log($"[AudioManager] {soundDictionary.Count}개의 사운드 로드 완료!");
        }

        #endregion

        #region BGM Control

        /// <summary>
        /// BGM 재생
        /// </summary>
        /// <param name="soundName">재생할 사운드 이름</param>
        public void PlayBGM(string soundName)
        {
            if (soundDictionary.TryGetValue(soundName, out SoundData data))
            {
                if (data.soundType != SoundType.BGM)
                {
                    Debug.LogWarning($"[AudioManager] {soundName}은 BGM이 아닙니다!");
                    return;
                }

                if (data.audioClip == null)
                {
                    Debug.LogWarning($"[AudioManager] {soundName}의 AudioClip이 없습니다!");
                    return;
                }

                bgmSource.clip = data.audioClip;
                bgmSource.volume = data.volume * bgmVolume * masterVolume;
                bgmSource.pitch = data.pitch;
                bgmSource.loop = data.loop;
                bgmSource.Play();

                Debug.Log($"[AudioManager] BGM 재생: {soundName}");
            }
            else
            {
                Debug.LogWarning($"[AudioManager] 사운드를 찾을 수 없음: {soundName}");
            }
        }

        /// <summary>
        /// BGM 정지
        /// </summary>
        public void StopBGM()
        {
            bgmSource.Stop();
            Debug.Log("[AudioManager] BGM 정지");
        }

        /// <summary>
        /// BGM 일시정지
        /// </summary>
        public void PauseBGM()
        {
            bgmSource.Pause();
        }

        /// <summary>
        /// BGM 재개
        /// </summary>
        public void ResumeBGM()
        {
            bgmSource.UnPause();
        }

        #endregion

        #region SFX Control

        /// <summary>
        /// SFX 재생 (한 번만 재생)
        /// </summary>
        /// <param name="soundName">재생할 사운드 이름</param>
        public void PlaySFX(string soundName)
        {
            if (soundDictionary.TryGetValue(soundName, out SoundData data))
            {
                if (data.soundType != SoundType.SFX)
                {
                    Debug.LogWarning($"[AudioManager] {soundName}은 SFX가 아닙니다!");
                    return;
                }

                if (data.audioClip == null)
                {
                    Debug.LogWarning($"[AudioManager] {soundName}의 AudioClip이 없습니다!");
                    return;
                }

                // PlayOneShot: 동시에 여러 SFX 재생 가능
                sfxSource.pitch = data.pitch;
                sfxSource.PlayOneShot(data.audioClip, data.volume * sfxVolume * masterVolume);

                Debug.Log($"[AudioManager] SFX 재생: {soundName}");
            }
            else
            {
                Debug.LogWarning($"[AudioManager] 사운드를 찾을 수 없음: {soundName}");
            }
        }

        #endregion

        #region Volume Control

        /// <summary>
        /// 마스터 볼륨 설정
        /// </summary>
        public void SetMasterVolume(float volume)
        {
            masterVolume = Mathf.Clamp01(volume);
            UpdateAllVolumes();
            SaveVolumeSettings();

            Debug.Log($"[AudioManager] 마스터 볼륨: {masterVolume}");
        }

        /// <summary>
        /// BGM 볼륨 설정
        /// </summary>
        public void SetBGMVolume(float volume)
        {
            bgmVolume = Mathf.Clamp01(volume);
            UpdateBGMVolume();
            SaveVolumeSettings();

            Debug.Log($"[AudioManager] BGM 볼륨: {bgmVolume}");
        }

        /// <summary>
        /// SFX 볼륨 설정
        /// </summary>
        public void SetSFXVolume(float volume)
        {
            sfxVolume = Mathf.Clamp01(volume);
            SaveVolumeSettings();

            Debug.Log($"[AudioManager] SFX 볼륨: {sfxVolume}");
        }

        /// <summary>
        /// 모든 볼륨 업데이트
        /// </summary>
        private void UpdateAllVolumes()
        {
            UpdateBGMVolume();
            // SFX는 PlayOneShot으로 재생되므로 다음 재생 시 적용됨
        }

        /// <summary>
        /// BGM 볼륨만 업데이트 (현재 재생 중인 BGM에 즉시 적용)
        /// </summary>
        private void UpdateBGMVolume()
        {
            if (bgmSource.clip != null)
            {
                // 현재 재생 중인 BGM의 SoundData를 찾아서 볼륨 적용
                foreach (var soundData in soundDataList)
                {
                    if (soundData.audioClip == bgmSource.clip)
                    {
                        bgmSource.volume = soundData.volume * bgmVolume * masterVolume;
                        break;
                    }
                }
            }
        }

        #endregion

        #region Save/Load Settings

        /// <summary>
        /// 볼륨 설정 저장
        /// </summary>
        private void SaveVolumeSettings()
        {
            PlayerPrefs.SetFloat("MasterVolume", masterVolume);
            PlayerPrefs.SetFloat("BGMVolume", bgmVolume);
            PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
            PlayerPrefs.Save();
        }

        /// <summary>
        /// 볼륨 설정 로드
        /// </summary>
        private void LoadVolumeSettings()
        {
            masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
            bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 0.8f);
            sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

            UpdateAllVolumes();
        }

        #endregion

        #region Utility

        /// <summary>
        /// BGM이 재생 중인지 확인
        /// </summary>
        public bool IsBGMPlaying()
        {
            return bgmSource.isPlaying;
        }

        /// <summary>
        /// 현재 볼륨 값 가져오기
        /// </summary>
        public float GetMasterVolume() => masterVolume;
        public float GetBGMVolume() => bgmVolume;
        public float GetSFXVolume() => sfxVolume;

        internal void PlaySFX(object soundName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
