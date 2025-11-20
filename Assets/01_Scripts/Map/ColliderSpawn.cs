using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSpawn : MonoBehaviour
{
    public MapMove mapMove;
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("충돌");
        if (other.CompareTag("Map"))
        {
            mapMove.Spawn();

        }
    }

  
}
