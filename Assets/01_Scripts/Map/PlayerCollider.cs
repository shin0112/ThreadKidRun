using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollider : MonoBehaviour
{
    public MapMove mapMove;
    public GameObject gameOverUi;
    public bool isCoinCollision = false;
    public LayerMask layerMask;
    //public Ray ray;
    //public Coin coin;
    void Start()
    {
        //Ray ray = new Ray(transform.position, Vector3.forward * 0.5f);
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
            //gameOverUi.SetActive(true);
            StartCoroutine(nameof(CollderGameOver));
        }
    }
    
    //public void CoinCollision()
    //{
    //    RaycastHit hit;
    //    if(Physics.Raycast(transform.position,transform.forward,out hit,layerMask))
    //    {
    //        coin.
    //    }
    //}


    IEnumerator CollderGameOver()
    {
        yield return new WaitForSeconds(1f);
        
        Time.timeScale = 0f;
    }
}
//플레이어가 태그 obstacle과 부딪쳤을 때
//코루틴을 이용해서 일시정지