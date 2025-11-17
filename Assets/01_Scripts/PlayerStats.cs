using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int hp = 100;

    public bool isGodMode = false;


    public void InitPlayerData()
    {
        hp = 100;
        isGodMode = false;
        Debug.Log("플레이어 데이터 초기화");
    }
}
