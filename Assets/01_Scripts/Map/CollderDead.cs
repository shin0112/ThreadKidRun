using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollderDead : MonoBehaviour
{
    public MapMove mapMove;
    public GameObject Player;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("DeadZone"))
        {
            mapMove.DestroyMap();
        }
    }


}
