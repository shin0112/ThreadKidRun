using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 맵 생성 시 PowerUp 아이템을 랜덤하게 스폰하는 매니저
/// MapMove와 독립적으로 작동합니다.
/// </summary>
public class PowerUpSpawner : MonoBehaviour
{
    #region Singleton

    public static PowerUpSpawner Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region Fields

    [Header("=== PowerUp Prefabs ===")]
    [Tooltip("스폰할 PowerUp 프리팹 리스트")]
    [SerializeField] private List<GameObject> powerUpPrefabs = new List<GameObject>();

    [Header("=== Spawn Settings ===")]
    [Tooltip("PowerUp 스폰 확률 (0~100%)")]
    [Range(0f, 100f)]
    [SerializeField] private float spawnChance = 30f;

    [Tooltip("맵 위에서 PowerUp이 스폰될 최소 X 위치")]
    [SerializeField] private float spawnMinX = -3f;

    [Tooltip("맵 위에서 PowerUp이 스폰될 최대 X 위치")]
    [SerializeField] private float spawnMaxX = 3f;

    [Tooltip("맵 바닥에서 PowerUp의 높이 (Y축)")]
    [SerializeField] private float spawnHeight = 1f;

    [Tooltip("맵 앞쪽에서 PowerUp이 스폰될 최소 Z 오프셋")]
    [SerializeField] private float spawnMinZ = 2f;

    [Tooltip("맵 앞쪽에서 PowerUp이 스폰될 최대 Z 오프셋")]
    [SerializeField] private float spawnMaxZ = 20f;

    [Header("=== References ===")]
    [Tooltip("moveMap Transform (PowerUp의 부모가 될 오브젝트)")]
    [SerializeField] private Transform moveMapTransform;

    #endregion

    #region Public Methods

    /// <summary>
    /// MapMove에서 호출: 맵 생성 시 PowerUp 스폰 시도
    /// </summary>
    /// <param name="spawnedMap">생성된 맵 오브젝트</param>
    public void TrySpawnPowerUpOnMap(GameObject spawnedMap)
    {
        // PowerUp 프리팹이 없으면 스폰하지 않음
        if (powerUpPrefabs == null || powerUpPrefabs.Count == 0)
        {
            Debug.LogWarning("[PowerUpSpawner] PowerUp 프리팹이 설정되지 않았습니다!");
            return;
        }

        // 확률 체크: 0~100 사이 랜덤값이 설정한 확률보다 작으면 스폰
        float randomValue = Random.Range(0f, 100f);
        if (randomValue > spawnChance)
        {
            return; // 확률에 걸리지 않으면 스폰 안 함
        }

        // PowerUp 스폰 실행
        SpawnPowerUp(spawnedMap);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// PowerUp을 맵 위의 랜덤한 위치에 생성
    /// </summary>
    /// <param name="mapPiece">생성된 맵 조각</param>
    private void SpawnPowerUp(GameObject mapPiece)
    {
        // 랜덤하게 PowerUp 프리팹 선택
        GameObject selectedPowerUp = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Count)];

        // 맵 위의 랜덤한 위치 계산
        Vector3 spawnPosition = CalculateSpawnPosition(mapPiece);

        // PowerUp 생성
        GameObject spawnedPowerUp = Instantiate(selectedPowerUp, spawnPosition, Quaternion.identity);

        // moveMap의 자식으로 설정 (맵과 함께 이동하도록)
        if (moveMapTransform != null)
        {
            spawnedPowerUp.transform.SetParent(moveMapTransform);
        }
        else
        {
            Debug.LogWarning("[PowerUpSpawner] moveMapTransform이 설정되지 않았습니다!");
        }

        Debug.Log($"[PowerUpSpawner] PowerUp 스폰: {selectedPowerUp.name} at {spawnPosition}");
    }

    /// <summary>
    /// 맵 위의 랜덤한 스폰 위치 계산
    /// </summary>
    /// <param name="mapPiece">맵 조각</param>
    /// <returns>스폰 위치 (Vector3)</returns>
    private Vector3 CalculateSpawnPosition(GameObject mapPiece)
    {
        Vector3 spawnPosition = mapPiece.transform.position;

        // X축: 좌우 랜덤 위치
        spawnPosition.x += Random.Range(spawnMinX, spawnMaxX);

        // Y축: 높이 설정
        spawnPosition.y += spawnHeight;

        // Z축: 맵 앞쪽의 랜덤 위치
        spawnPosition.z += Random.Range(spawnMinZ, spawnMaxZ);

        return spawnPosition;
    }

    #endregion

    #region Utility Methods

    /// <summary>
    /// 스폰 확률 변경 (외부에서 호출 가능)
    /// </summary>
    public void SetSpawnChance(float newChance)
    {
        spawnChance = Mathf.Clamp(newChance, 0f, 100f);
        Debug.Log($"[PowerUpSpawner] 스폰 확률 변경: {spawnChance}%");
    }

    /// <summary>
    /// 현재 스폰 확률 반환
    /// </summary>
    public float GetSpawnChance()
    {
        return spawnChance;
    }

    #endregion

    #region Editor Helper

#if UNITY_EDITOR
    /// <summary>
    /// 에디터에서 스폰 범위 시각화
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (moveMapTransform == null) return;

        Gizmos.color = Color.yellow;

        // 스폰 영역 표시
        Vector3 center = moveMapTransform.position;
        center.y += spawnHeight;

        Vector3 size = new Vector3(
            spawnMaxX - spawnMinX,
            0.1f,
            spawnMaxZ - spawnMinZ
        );

        Gizmos.DrawWireCube(center, size);
    }
#endif

    #endregion
}
