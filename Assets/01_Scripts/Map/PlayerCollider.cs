using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public MapMove mapMove;
    
    void Start()
    {
        Ray ray = new Ray(transform.position, Vector3.forward * 0.5f);
    }


    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            mapMove.isMove = false;
            Debug.Log(mapMove.isMove);
            //StartCoroutine(nameof(CollderGameOver));
        }
    }
    

    IEnumerator CollderGameOver()
    {
        yield return new WaitForSeconds(0f);

        //Time.timeScale = 0f;
    }
}
//플레이어가 태그 obstacle과 부딪쳤을 때
//코루틴을 이용해서 일시정지