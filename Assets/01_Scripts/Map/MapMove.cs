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


    [Header("Option")]
    public float speed;//맵스피드
    public bool isMove = false;
    private bool isFirst = false;
    private bool isGameOver = false;

    public void GameOver()
    {
        isMove = false;
        isGameOver = true;
    }

    void Start()
    {

        Spawn();
        Spawn();
        Spawn();
        Spawn();
        Spawn();

    }


    void Update()
    {
        if (!isGameOver && Input.GetKeyDown(KeyCode.Space))
        {
            isMove = true;
        }

        Move();
        //SpawnPivot();
        DestroyMap();
    }

    public void Move()
    {

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    isMove = true;
        //}
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
        //lastPivot.transform.position += new Vector3(0, 0, 24);
        Transform newtransfrom = piece.transform.Find("LastPivot");
        if (newtransfrom != null)
        {
            lastPivot = newtransfrom;
        }

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
        for (int i = 0; i < mapLists.Count; i++)
        {
            if (mapLists.Count >= deathZone.position.z)
            {
                GameObject piece = mapLists[i];
                if (piece != null && piece.transform.position.z <= deathZone.position.z)
                {
                    mapLists.Remove(piece);
                    Destroy(piece);

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