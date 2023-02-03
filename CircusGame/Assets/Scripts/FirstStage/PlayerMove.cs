using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private float playSpeed = 10f; // 플레이어 스피드
    public float jumpForce = default; // 점프 힘

    private int jumpCount = default; // 누적 점프 횟수
    private bool isGround = false; // 땅에 닿았는지 확인
    public bool isDie = false;
    private bool isGoalCk = false;
    

    #region Player's component
    private Rigidbody2D playerRigid = default;
    private Animator PlayerAni = default;
    private Animator LionAni = default;
 
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // 리지드바디 선언
        playerRigid = gameObject.GetComponent<Rigidbody2D>();
        //// 애니메이션 선언
        //LionAni = gameObject.GetComponent<Animator>();
        // 자식에 있는 애니메이션을 가지고 왔다.
        PlayerAni = transform.GetChild(0).GetComponent<Animator>();
        LionAni = transform.GetChild(1).GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // isGoalCk와 isDie 이 false 면 move,jump 가 움직이지 않게 하기
        if (!isGoalCk && !isDie)
        {
            Move();
            Jump();
        }
        //Debug.Log($"movecheck : {isGoalCk}  , {isDie}");


      
          
    }

    // Move 좌, 우 이동하기
    private void Move()
    {
        // <- 방향키를 누를때  왼쪽으로이동
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //playerRigid.AddForce(new Vector2(-playSpeed, 0f), ForceMode2D.Impulse);
            playerRigid.velocity = new Vector2(-1 * playSpeed, 0f);
        }
        // <- 방향키를 땠을때 이동 하지 않게 하기
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            playerRigid.velocity = Vector2.zero;
        }

        // -> 방향키를 누를 때 오른쪽으로 이동
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //playerRigid.AddForce(new Vector2(playSpeed, 0f), ForceMode2D.Impulse);
            playerRigid.velocity = new Vector2(1 * playSpeed, 0f);
        }
        // -> 방향키를 떘을때 이동 하지 않게 하기
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            playerRigid.velocity = Vector2.zero;
        }
    }

    // 버튼 입력(모바일)
    // 왼쪽 버튼 
    public void OnLeftButtonClick()
    {
        if (!isGoalCk && !isDie)
        {
            playerRigid.velocity = new Vector2(-1 * playSpeed, 0f);
        }
    }
    public void OnLeftButtonUp()
    {
        playerRigid.velocity = Vector2.zero;
    }


    // 오른쪽 버튼
    public void OnRightButtonClick()
    {
        
        if (!isGoalCk && !isDie)
        {
            playerRigid.velocity = new Vector2(1 * playSpeed, 0f);
        }
    }
    public void OnRightButtonUp()
    {
        if (!isGoalCk && !isDie)
        {
            playerRigid.velocity = Vector2.zero;
        }
    }

    // 점프 버튼
    public void OnJumpButtonClick()
    {
        if (!isGoalCk && !isDie)
        {
            if (Input.GetMouseButtonDown(0) && jumpCount < 1)
            {
                // 점프를 하지 않게 만든다(1번만 점프)
                jumpCount++;

                playerRigid.AddForce(new Vector2(0, jumpForce));
            }
        }

    }

    // jump 하기
    public void Jump()
    {
        // 스페이스바를 눌렀을 때 점프 && jump 1번만 하기
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 1)
        {
            // 점프를 하지 않게 만든다(1번만 점프)
            jumpCount++;

            playerRigid.AddForce(new Vector2(0, jumpForce));
        }
        
    }

    // 충돌하는지 확인하기
    public void OnCollisionEnter2D(Collision2D collision)
    {
        // Ground라는 이름의 tag와 충돌하면
        if (collision.gameObject.tag == "Ground")
        {
            LionAni.SetBool("Grounded", true);
            // 초기화 (다시 점프하게 만든다)
            jumpCount = 0;
        }

        // Gogal 위에 닿으면 SecondScene 이름의 씬으로 넘어간다.
        if (collision.gameObject.tag == "GoalBoxCollider")
        {
            // 가속도 초기화
            isGoalCk = true;
            playerRigid.velocity = Vector2.zero;

            // 플레이어 좌표를 Goal 좌표로 고정한다.
            transform.localPosition = new Vector2(12040f, -90f);
            // 딜레이 함수 불러오기
            StartCoroutine(Goal());
        }

        if (collision.gameObject.tag == "Obstacle")
        {
            Die();
        }
    }

    // 딜레이를 주기 위해 만든 함수
    IEnumerator Goal()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.CurrentStage++;
        GameManager.Instance.tmpTextChange();
        SceneManager.LoadScene("SecondStage");
    }



    public void Die()
    {
        PlayerAni.SetTrigger("Die");
        LionAni.SetTrigger("Die");

        // 플레이어가 못 움직이게 한다.
        // 가속도 초기화
        isDie = true;
        playerRigid.velocity = Vector2.zero;

        SceneManager.LoadScene("GameOver");

    }

   
}
