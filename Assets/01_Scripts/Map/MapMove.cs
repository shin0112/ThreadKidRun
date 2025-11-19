using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMove : MonoBehaviour
{
    [SerializeField] private Transform moveMap;//움직임 관여
    [SerializeField] private Transform deathZone;//부딪쳤을 때 게임삭제
    [SerializeField] private List<GameObject> mapLists;//맵리스트
    [SerializeField] private List<GameObject> mapListPrefab;//맵생성
    [SerializeField] private Transform lastPivot;//재생성 위치
    [SerializeField] private Transform spawnPivot;//맵을 스폰하는 위치
    [SerializeField] private GameObject firstPivot;//첫번째 맵 뒤에 스폰할 위치
    [SerializeField] private GameObject firstMap;

    [Header("Option")]
    [SerializeField] private float speed;//맵스피드
    public bool isMove = true;
    private bool isFirst = false;
    void Start()
    {
        //GameObject firstBasictMap = firstMap;
        //if (firstMap != null && isFirst)
        //{
        //    Vector3 first = firstBasictMap.transform.position + new Vector3(0, 0, 24);
        //    Instantiate(mapListPrefab[0], firstBasictMap.transform.position, Quaternion.identity);
        //    isFirst = true;
        //}

        Spawn();
        Spawn();
        Spawn();
        Spawn();
        Spawn();

    }


    void Update()
    {
        Move();
        //SpawnPivot();
        //DestroyMap();
    }

    public void Move()
    {
        if (isMove == true)
        {
            Vector3 pos = moveMap.transform.position;
            pos.z -= speed * Time.deltaTime;
            moveMap.transform.position = pos;
        }

    }
    public void Spawn()
    {

        GameObject prefabs = mapListPrefab[Random.Range(0, mapListPrefab.Count)];


        GameObject piece = Instantiate(prefabs, lastPivot.position, Quaternion.identity);
        lastPivot.transform.position += new Vector3(0, 0, 24);

        piece.transform.SetParent(moveMap);
        if (mapLists.Count > 1)
        {
            GameObject Piece = mapLists[mapLists.Count - 1];

        }

        mapLists.Add(piece);

        // ============================================
        // PowerUpSpawner 호출
        // ============================================
        if (PowerUpSpawner.Instance != null)
        {
            PowerUpSpawner.Instance.TrySpawnPowerUpOnMap(piece);
        }

    }


    public void DestroyMap()
    {
        for (int i = 0; i < mapListPrefab.Count; i++)
        {
            if(mapListPrefab.Count >= deathZone.position.z)
            {
                GameObject piece = mapLists[i];
                if (piece != null && piece.transform.position.z <= deathZone.position.z)
                {
                    mapLists.Remove(piece);
                    Destroy(piece);
                    //Spawn();
                }
            }
            

        }


    }

}
//오브젝트 움직임
//맵을움직이는 오브젝트를 움직임
//움직이는 오브젝트 속도
//부딪쳤을 때 오브젝트 삭제
//맵 리스트
//맵 생성 리스트