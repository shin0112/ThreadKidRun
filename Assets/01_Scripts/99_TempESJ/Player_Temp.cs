using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Temp : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = transform.forward * moveVertical + transform.right * moveHorizontal;
        Vector3 targetPosition = rb.position + movement * moveSpeed * Time.deltaTime;

        rb.MovePosition(targetPosition);
    }
}
