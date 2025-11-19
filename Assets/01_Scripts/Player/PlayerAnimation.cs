using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rb;

    public float moveSpeed = 3f;
    public float jumpForce = 5f;
    public float slidespeed = 6f;
    public float slideDuration = 0.7f;

    private bool isMap = true;
    private bool isSliding = false;
    private float sliderTimer = 0f;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        bool hitmap = Physics.Raycast(transform.position, Vector3.down, 1.1f);
        if (hitmap) isMap = true;
        Move();
        Jump();
        Slide();
    }
    void Move()
    {
        Vector3 dir = Vector3.zero;
        if (Input.GetKey(KeyCode.A)) dir += Vector3.left;
        if (Input.GetKey(KeyCode.D)) dir += Vector3.right;
        
        if (dir != Vector3.zero)
        {
            transform.Translate(dir.normalized * moveSpeed * Time.deltaTime, Space.World);
            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetBool("isRun", false);
        }
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isMap)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.SetBool("isJump", true);
            isMap = false;

            StartCoroutine(ResetJumpBool());
        }
        IEnumerator ResetJumpBool()
        {
            yield return new WaitForSeconds(0.1f); // 한 프레임 후
            anim.SetBool("isJump", false);
        }
    }
    void Slide()
    {
        if (!isMap)
            return;
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isSliding)
        {
            isSliding = true;
            sliderTimer = slideDuration; 
            anim.SetTrigger("isSliding");
        }
        if (isSliding)
        {
            sliderTimer -= Time.deltaTime;
            if (sliderTimer <= 0f)
            {
                isSliding = false;
                anim.ResetTrigger("isSliding");
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {
            isMap = true;
            anim.SetBool("isJump", false);
        }
    }
}