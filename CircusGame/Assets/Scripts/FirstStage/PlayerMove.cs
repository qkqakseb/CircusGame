using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private const float PLAYER_STEP_ON_Y_ANGLE_MIN = 0.7f;  //!< 45도 각도

    public float jumpForce = default; // 점프 힘

    private int jumpCount = default; // 누적 점프 횟수
    private bool isGround = false; // 땅에 닿았는지
    private bool isDead = false;     // 사망 상태
    private float backgrMove = default;

    #region Player's component
    private Rigidbody2D playerRigid = default;
    private Animator playerAni = default;
 
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        playerRigid = gameObject.GetComponentMust<Rigidbody2D>();
        playerAni = gameObject.GetComponentMust<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // 사망 시 처리를 더 이상 진행하지 않고 종료
        if (isDead == true) { return;}

        // 스페이스를 눌렀으며 && 최대 점프 횟수(2)에 도달하지 않았다면
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 1)
        {
            jumpCount++;

            // 점프키 누르는 순간 움직임을 완전히 멈춤
            //playerRigid.velocity = Vector2.zero;


            // 리지드바디에 위쪽으로 힘 주기
            playerRigid.AddForce(new Vector2(0, jumpForce));
        }  // if : 플레이어가 점프 할 때

        // 왼쪽 방향키를 누르는 순간 움직인다.
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            playerRigid.velocity = new Vector2( -1 * 5f,  0f);
        }
        else if(Input.GetKeyUp(KeyCode.LeftArrow))     // 이코드 문제 수정해야된다!!!!!!
        {
            // 왼쪽 방향키를 때는 순간 완전히 멈춘다.(슬라이드로 가는 거 막을려고 쓴다.)
            playerRigid.velocity = Vector2.zero;
            //playerRigid.velocity = new Vector2(0, playerRigid.velocity.y);
        }

        // 오른쪽 방향키를 누르는 순간 움직인다.
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            playerRigid.velocity = new Vector2(1 * 5f, 0f);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            // 오른쪽 방향키를 때는 순간 완전히 멈춘다.
            playerRigid.velocity = Vector2.zero;

        }

    }

    private void Die()
    {
        // 애니메이터의 Die 트리거 파라미터를 셋
        playerAni.SetTrigger("Die");

        // 속도를 제로로 변경
        playerRigid.velocity = Vector2.zero;
        // 사망 상태를 true로 변경
        isDead = true;
        GFunc.Log(isDead);
    }

    //! 바닥에 닿았는지 체크하는 함수
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PLAYER_STEP_ON_Y_ANGLE_MIN < collision.contacts[0].normal.y)
        {
            isGround = true;
            jumpCount = 0;
        }       // if: 45도 보다 완만한 땅을 밟은 경우
    }       // OnCollisionEnter2D()

    //! 바닥에서 벗어났는지 체크하는 함수
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;
    }       // OnCollisionExit2D()
}
