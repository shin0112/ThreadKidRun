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
    public Animator Animator;
    //public Ray ray;
    //public Coin coin;
    void Start()
    {
        //Ray ray = new Ray(transform.position, Vector3.forward * 0.5f);
    }


    void Update()
    {
        CollisionRay();


    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("collision");
    //    if (collision.gameObject.CompareTag("Obstacle"))
    //    {
    //        mapMove.isMove = false;
    //        Animator.Play("Death_A");
    //        StartCoroutine(nameof(CollderGameOver));
    //    }
    //}
    public void CollisionRay()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 2f, layerMask))
        {

            mapMove.isMove = false;
            Animator.Play("Death_A");
            StartCoroutine(nameof(CollderGameOver));

        }
    }



    IEnumerator CollderGameOver()
    {
        yield return new WaitForSeconds(1f);
        UIManager.Instance.ShowGameOverWindow();
        Time.timeScale = 0f;
    }

    
}
