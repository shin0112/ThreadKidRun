using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMove : MonoBehaviour
{
    [SerializeField] private Transform moveMap;//움직임 관여
    [SerializeField] private Transform deathZone;//부딪쳤을 때 게임삭제
    [SerializeField] private List<MapList> mapLists;//맵리스트
    [SerializeField] private List<GameObject> mapListPrefab;//맵생성
    [SerializeField] private Transform LastPivot;//재생성 위치

    [Header("Option")]
    [SerializeField] private float speed;//맵스피드
    
    void Start()
    {
        
    }

    
    void Update()
    {
        Move();
    }

    public void Move()
    {
        Vector3 pos = moveMap.transform.position;
        pos.z -= speed * Time.deltaTime;
        moveMap.transform.position = pos;
    }
    public void Spawn()
    {
        
    }

    public void DestroyMap()
    {

    }
}
//오브젝트 움직임
//맵을움직이는 오브젝트를 움직임
//움직이는 오브젝트 속도
//부딪쳤을 때 오브젝트 삭제
//맵 리스트
//맵 생성 리스트