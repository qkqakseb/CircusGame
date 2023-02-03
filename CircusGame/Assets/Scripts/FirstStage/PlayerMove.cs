using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private float playSpeed = 10f; // �÷��̾� ���ǵ�
    public float jumpForce = default; // ���� ��

    private int jumpCount = default; // ���� ���� Ƚ��
    private bool isGround = false; // ���� ��Ҵ��� Ȯ��
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
        // ������ٵ� ����
        playerRigid = gameObject.GetComponent<Rigidbody2D>();
        //// �ִϸ��̼� ����
        //LionAni = gameObject.GetComponent<Animator>();
        // �ڽĿ� �ִ� �ִϸ��̼��� ������ �Դ�.
        PlayerAni = transform.GetChild(0).GetComponent<Animator>();
        LionAni = transform.GetChild(1).GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // isGoalCk�� isDie �� false �� move,jump �� �������� �ʰ� �ϱ�
        if (!isGoalCk && !isDie)
        {
            Move();
            Jump();
        }
        //Debug.Log($"movecheck : {isGoalCk}  , {isDie}");


      
          
    }

    // Move ��, �� �̵��ϱ�
    private void Move()
    {
        // <- ����Ű�� ������  ���������̵�
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //playerRigid.AddForce(new Vector2(-playSpeed, 0f), ForceMode2D.Impulse);
            playerRigid.velocity = new Vector2(-1 * playSpeed, 0f);
        }
        // <- ����Ű�� ������ �̵� ���� �ʰ� �ϱ�
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            playerRigid.velocity = Vector2.zero;
        }

        // -> ����Ű�� ���� �� ���������� �̵�
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //playerRigid.AddForce(new Vector2(playSpeed, 0f), ForceMode2D.Impulse);
            playerRigid.velocity = new Vector2(1 * playSpeed, 0f);
        }
        // -> ����Ű�� ������ �̵� ���� �ʰ� �ϱ�
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            playerRigid.velocity = Vector2.zero;
        }
    }

    // ��ư �Է�(�����)
    // ���� ��ư 
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


    // ������ ��ư
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

    // ���� ��ư
    public void OnJumpButtonClick()
    {
        if (!isGoalCk && !isDie)
        {
            if (Input.GetMouseButtonDown(0) && jumpCount < 1)
            {
                // ������ ���� �ʰ� �����(1���� ����)
                jumpCount++;

                playerRigid.AddForce(new Vector2(0, jumpForce));
            }
        }

    }

    // jump �ϱ�
    public void Jump()
    {
        // �����̽��ٸ� ������ �� ���� && jump 1���� �ϱ�
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 1)
        {
            // ������ ���� �ʰ� �����(1���� ����)
            jumpCount++;

            playerRigid.AddForce(new Vector2(0, jumpForce));
        }
        
    }

    // �浹�ϴ��� Ȯ���ϱ�
    public void OnCollisionEnter2D(Collision2D collision)
    {
        // Ground��� �̸��� tag�� �浹�ϸ�
        if (collision.gameObject.tag == "Ground")
        {
            LionAni.SetBool("Grounded", true);
            // �ʱ�ȭ (�ٽ� �����ϰ� �����)
            jumpCount = 0;
        }

        // Gogal ���� ������ SecondScene �̸��� ������ �Ѿ��.
        if (collision.gameObject.tag == "GoalBoxCollider")
        {
            // ���ӵ� �ʱ�ȭ
            isGoalCk = true;
            playerRigid.velocity = Vector2.zero;

            // �÷��̾� ��ǥ�� Goal ��ǥ�� �����Ѵ�.
            transform.localPosition = new Vector2(12040f, -90f);
            // ������ �Լ� �ҷ�����
            StartCoroutine(Goal());
        }

        if (collision.gameObject.tag == "Obstacle")
        {
            Die();
        }
    }

    // �����̸� �ֱ� ���� ���� �Լ�
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

        // �÷��̾ �� �����̰� �Ѵ�.
        // ���ӵ� �ʱ�ȭ
        isDie = true;
        playerRigid.velocity = Vector2.zero;

        SceneManager.LoadScene("GameOver");

    }

   
}
