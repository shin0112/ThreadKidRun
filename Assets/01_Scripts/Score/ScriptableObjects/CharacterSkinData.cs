using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameName.Data
{
    /// <summary>
    /// 캐릭터 스킨 데이터를 담는 ScriptableObject
    /// </summary>
    [CreateAssetMenu(fileName = "NewCharacterSkin", menuName = "Game/Character Skin Data", order = 2)]
    public class CharacterSkinData : ScriptableObject
    {
        [Header("=== 기본 정보 ===")]
        [Tooltip("스킨 고유 ID (중복 불가)")]
        public string skinID;

        [Tooltip("스킨 표시 이름")]
        public string skinName;

        [Header("=== 모델 ===")]
        [Tooltip("캐릭터 스킨 프리팹")]
        public GameObject skinPrefab;

        [Tooltip("UI 프리뷰용 이미지")]
        public Sprite previewSprite;

        [Header("=== 잠금 설정 ===")]
        [Tooltip("기본 스킨 여부 (기본 스킨은 처음부터 해금)")]
        public bool isDefaultSkin = false;

        [Tooltip("해금에 필요한 코인")]
        [Range(0, 10000)]
        public int unlockCost = 100;

        [Header("=== 설명 ===")]
        [TextArea(2, 4)]
        public string description;
    }
}
