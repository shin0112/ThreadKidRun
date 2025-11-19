using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class weather : MonoBehaviour
{
    public GameObject winterParticle;
    public Coin coin;
    private bool isScore = false;
    public Camera camera;
    
    void Start()
    {
        camera = Camera.main;
    }


    void Update()
    {

    }

    //public void Winter()
    //{
    //    if (coin.coinValue <= 20)
    //    {
    //        winterParticle.SetActive(false);
    //    }
    //    if (coin.coinValue <= 5)
    //    {
    //        winterParticle.SetActive(true);
    //    }
    //}
}
