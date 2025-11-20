using System.Collections;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public MapMove mapMove;
    public bool isCoinCollision = false;
    public LayerMask layerMask;
    public Animator Animator;
    private PlayerAnimation _playerAnimation;

    //public Ray ray;
    //public Coin coin;

    // ============================================
    // 무적 상태 플래그 추가
    // InvincibilityPowerUp에서 이 값을 true/false로 설정
    // ============================================
    [HideInInspector]
    public bool isInvincible = false;

    void Start()
    {
        //Ray ray = new Ray(transform.position, Vector3.forward * 0.5f);

        if (!TryGetComponent<CustomizingController>(out var _customizingController))
        {
            Logger.LogWarning("커스터마이징 컨트롤러 초기화 실패");
        }
        if (!TryGetComponent(out _playerAnimation))
        {
            Logger.LogWarning("플레이어 애니메이션 초기화 실패");
        }

        Animator = _customizingController.GetAnimator();

        if (mapMove == null)
        {
            mapMove = FindObjectOfType<MapMove>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");

        // ============================================
        // 무적 상태일 때는 장애물 충돌 무시
        // ============================================
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // 튜토리얼 중일 경우
            if (!GameManager.Instance.FinishedTutorial)
            {
                StartCoroutine("TutorialCollisonAnimation");
                mapMove.BackForTutorial();
                return;
            }

            if (isInvincible)
            {
                Debug.Log("[PlayerCollider] 무적 상태! 장애물 충돌 무시");
                return; // 무적 상태면 게임오버 처리 하지 않음
            }

            // 무적이 아닐 때만 게임오버 처리
            mapMove.GameOver();
            _playerAnimation.IsGameOver = true;

            Animator.Play("Death_A");
            _playerAnimation.Death();
            StartCoroutine(nameof(CollderGameOver));
        }
    }

    IEnumerator CollderGameOver()
    {
        yield return new WaitForSeconds(1f);
        UIManager.Instance.ShowGameOverWindow();
        //Time.timeScale = 0f;
    }

    /// <summary>
    /// 튜토리얼 중에 충돌할 경우에 실행될 애니메이션
    /// </summary>
    /// <returns></returns>
    IEnumerator TutorialCollisonAnimation()
    {
        _playerAnimation.Death();
        yield return new WaitForSeconds(1.5f);
        Animator.SetBool("isDie", false);
    }
}
//플레이어가 태그 obstacle과 부딪쳤을 때
//코루틴을 이용해서 일시정지