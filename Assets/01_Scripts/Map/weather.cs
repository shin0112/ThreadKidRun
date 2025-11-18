using System.Collections;
using System.Collections.Generic;
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

    public void Winter()
    {
        if (coin.coinValue <= 20)
        {
            winterParticle.SetActive(false);
        }
        if (coin.coinValue <= 5)
        {
            winterParticle.SetActive(true);
        }
    }

    public void WinterCameraInterrupt()
    {
        if (winterParticle.activeSelf)
        {
            Vector2[] CameraDirections = new Vector2[4]
        {
            new Vector2(Screen.width * 0.25f, Screen.height * 0.75f),
            new Vector2(Screen.width * 0.75f, Screen.height * 0.75f),
            new Vector2(Screen.width * 0.25f, Screen.height * 0.25f),
            new Vector2(Screen.width * 0.75f, Screen.height * 0.25f)

        };
            int cameraRandom = Random.Range(0, 3);
            Vector3 snow = CameraDirections[cameraRandom];

        }

    }
}
