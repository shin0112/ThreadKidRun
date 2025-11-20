using GameName.Managers;
using System.Collections;
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

    private bool _isGameOver;
    public bool IsGameOver
    {
        get { return _isGameOver; }
        set { _isGameOver = value; }
    }

    private CapsuleCollider _capsuleCollider;

    void Start()
    {
        if (!TryGetComponent<CustomizingController>(out var _customizingController))
        {
            Logger.LogWarning("커스터마이징 컨트롤러 초기화 실패");
        }
        if (!TryGetComponent(out _capsuleCollider))
        {
            Logger.LogWarning("콜라이더 초기화 실패");
        }

        anim = _customizingController.GetAnimator();
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
        float clampedX = Mathf.Clamp(transform.position.x, -5f, 5.6f);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isMap && !_isGameOver)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.SetBool("isJump", true);
            isMap = false;

            StartCoroutine(ResetJumpBool());

            //점프 시 SFX 재생
            // ============================================
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX("SFX_Jump");
            }
            // ============================================

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
            anim.SetBool("isSliding", true);

            _capsuleCollider.direction = 2;
            Vector3 colliderCenter = _capsuleCollider.center;
            colliderCenter = new Vector3(0, 0.75f, -0.6f);
            _capsuleCollider.center = colliderCenter;
        }
        if (isSliding)
        {
            sliderTimer -= Time.deltaTime;
            if (sliderTimer <= 0f)
            {
                isSliding = false;
                anim.SetBool("isSliding", false);

                _capsuleCollider.direction = 1;
                Vector3 colliderCenter = _capsuleCollider.center;
                colliderCenter = new Vector3(0, 1.25f, 0);
                _capsuleCollider.center = colliderCenter;
            }
        }
    }

    public void Death()
    {
        anim.SetBool("isDie", true);
        anim.SetBool("isSliding", false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {
            isMap = true;
            anim.SetBool("isJump", false);
        }
    }

    public void UpdateAnimator(Animator anim)
    {
        this.anim = anim;
    }
}