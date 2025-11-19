using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameName.Data;

namespace GameName.Managers
{
    /// <summary>
    /// 캐릭터 스킨을 관리하는 매니저
    /// 스킨 선택, 저장, 불러오기, 적용을 담당합니다.
    /// </summary>
    public class CharacterSkinManager : MonoBehaviour
    {
        #region Singleton

        public static CharacterSkinManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            InitializeManager();
        }

        #endregion

        #region Fields

        [Header("=== 스킨 데이터 목록 ===")]
        [Tooltip("게임에 존재하는 모든 캐릭터 스킨 데이터")]
        [SerializeField] private List<CharacterSkinData> allSkins = new List<CharacterSkinData>();

        [Header("=== 현재 스킨 ===")]
        [Tooltip("현재 선택된 스킨")]
        [SerializeField] private CharacterSkinData currentSkin;

        [Header("=== 플레이어 설정 ===")]
        [Tooltip("게임 씬에서 캐릭터가 생성될 위치")]
        [SerializeField] private Transform playerSpawnPoint;

        [Tooltip("현재 활성화된 플레이어 캐릭터")]
        private GameObject currentPlayerInstance;

        // PlayerPrefs 키
        private const string SELECTED_SKIN_KEY = "SelectedSkinID";

        #endregion

        #region Initialization

        /// <summary>
        /// 매니저 초기화
        /// </summary>
        private void InitializeManager()
        {
            // 저장된 스킨 불러오기
            LoadSelectedSkin();

            Debug.Log("[CharacterSkinManager] 초기화 완료!");
        }

        #endregion

        #region Public Methods - 스킨 선택

        /// <summary>
        /// 스킨 ID로 스킨 선택
        /// </summary>
        /// <param name="skinID">선택할 스킨의 ID</param>
        /// <returns>성공 여부</returns>
        public bool SelectSkin(int skinID)
        {
            CharacterSkinData skin = GetSkinByID(skinID);

            if (skin == null)
            {
                Debug.LogError($"[CharacterSkinManager] ID {skinID}인 스킨을 찾을 수 없습니다!");
                return false;
            }

            // 스킨 선택
            currentSkin = skin;

            // PlayerPrefs에 저장
            SaveSelectedSkin(skinID);

            Debug.Log($"[CharacterSkinManager] '{skin.skinName}' 스킨 선택 완료!");

            return true;
        }

        /// <summary>
        /// 스킨 이름으로 스킨 선택
        /// </summary>
        public bool SelectSkinByName(string skinName)
        {
            CharacterSkinData skin = GetSkinByName(skinName);

            if (skin == null)
            {
                Debug.LogError($"[CharacterSkinManager] 이름이 '{skinName}'인 스킨을 찾을 수 없습니다!");
                return false;
            }

            return SelectSkin(skin.skinID);
        }

        #endregion

        #region Public Methods - 스킨 적용

        /// <summary>
        /// 현재 선택된 스킨을 플레이어에게 적용
        /// </summary>
        public void ApplyCurrentSkin()
        {
            if (currentSkin == null)
            {
                Debug.LogError("[CharacterSkinManager] 적용할 스킨이 선택되지 않았습니다!");
                return;
            }

            ApplySkin(currentSkin);
        }

        /// <summary>
        /// 특정 스킨을 플레이어에게 적용 (프리팹 교체)
        /// </summary>
        /// <param name="skinData">적용할 스킨 데이터</param>
        public void ApplySkin(CharacterSkinData skinData)
        {
            if (skinData == null || skinData.skinPrefab == null)
            {
                Debug.LogError("[CharacterSkinManager] 스킨 데이터 또는 프리팹이 없습니다!");
                return;
            }

            // 기존 플레이어 제거
            if (currentPlayerInstance != null)
            {
                Destroy(currentPlayerInstance);
            }

            // 스폰 위치 결정
            Vector3 spawnPosition = Vector3.zero;
            Quaternion spawnRotation = Quaternion.identity;

            if (playerSpawnPoint != null)
            {
                spawnPosition = playerSpawnPoint.position;
                spawnRotation = playerSpawnPoint.rotation;
            }

            // 새 캐릭터 생성
            currentPlayerInstance = Instantiate(skinData.skinPrefab, spawnPosition, spawnRotation);
            currentPlayerInstance.name = $"Player_{skinData.skinName}";

            // Player 태그 설정
            if (!currentPlayerInstance.CompareTag("Player"))
            {
                currentPlayerInstance.tag = "Player";
            }

            Debug.Log($"[CharacterSkinManager] '{skinData.skinName}' 스킨 적용 완료!");
        }

        #endregion

        #region Public Methods - 정보 조회

        /// <summary>
        /// 현재 선택된 스킨 반환
        /// </summary>
        public CharacterSkinData GetCurrentSkin()
        {
            return currentSkin;
        }

        /// <summary>
        /// 모든 스킨 목록 반환
        /// </summary>
        public List<CharacterSkinData> GetAllSkins()
        {
            return allSkins;
        }

        /// <summary>
        /// ID로 스킨 찾기
        /// </summary>
        public CharacterSkinData GetSkinByID(int skinID)
        {
            return allSkins.Find(skin => skin.skinID == skinID);
        }

        /// <summary>
        /// 이름으로 스킨 찾기
        /// </summary>
        public CharacterSkinData GetSkinByName(string skinName)
        {
            return allSkins.Find(skin => skin.skinName == skinName);
        }

        #endregion

        #region PlayerPrefs - 저장/불러오기

        /// <summary>
        /// 선택된 스킨 ID를 저장
        /// </summary>
        private void SaveSelectedSkin(int skinID)
        {
            PlayerPrefs.SetInt(SELECTED_SKIN_KEY, skinID);
            PlayerPrefs.Save();

            Debug.Log($"[CharacterSkinManager] 스킨 ID {skinID} 저장 완료!");
        }

        /// <summary>
        /// 저장된 스킨 불러오기
        /// </summary>
        private void LoadSelectedSkin()
        {
            // 저장된 스킨 ID 가져오기 (기본값: 0)
            int savedSkinID = PlayerPrefs.GetInt(SELECTED_SKIN_KEY, 0);

            // 해당 스킨 찾기
            CharacterSkinData skin = GetSkinByID(savedSkinID);

            if (skin != null)
            {
                currentSkin = skin;
                Debug.Log($"[CharacterSkinManager] 저장된 스킨 '{skin.skinName}' 불러오기 완료!");
            }
            else
            {
                // 저장된 스킨이 없으면 기본 스킨 사용
                CharacterSkinData defaultSkin = allSkins.Find(s => s.isDefaultSkin);

                if (defaultSkin != null)
                {
                    currentSkin = defaultSkin;
                    Debug.Log($"[CharacterSkinManager] 기본 스킨 '{defaultSkin.skinName}' 사용!");
                }
                else if (allSkins.Count > 0)
                {
                    currentSkin = allSkins[0];
                    Debug.LogWarning("[CharacterSkinManager] 기본 스킨이 없어 첫 번째 스킨 사용!");
                }
            }
        }

        #endregion

        #region Editor Helper

#if UNITY_EDITOR
        /// <summary>
        /// 에디터에서 스킨 목록 자동 수집 (테스트용)
        /// </summary>
        [ContextMenu("스킨 목록 자동 수집")]
        private void CollectAllSkins()
        {
            allSkins.Clear();

            // Assets 폴더에서 모든 CharacterSkinData 찾기
            string[] guids = UnityEditor.AssetDatabase.FindAssets("t:CharacterSkinData");

            foreach (string guid in guids)
            {
                string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
                CharacterSkinData skin = UnityEditor.AssetDatabase.LoadAssetAtPath<CharacterSkinData>(path);

                if (skin != null && !allSkins.Contains(skin))
                {
                    allSkins.Add(skin);
                }
            }

            // ID 순으로 정렬
            allSkins.Sort((a, b) => a.skinID.CompareTo(b.skinID));

            Debug.Log($"[CharacterSkinManager] {allSkins.Count}개의 스킨을 자동 수집했습니다!");
        }
#endif

        #endregion
    }
}
