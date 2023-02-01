using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private const float PLAYER_STEP_ON_Y_ANGLE_MIN = 0.7f;  //!< 45�� ����

    public float jumpForce = default; // ���� ��

    private int jumpCount = default; // ���� ���� Ƚ��
    private bool isGround = false; // ���� ��Ҵ���
    private bool isDead = false;     // ��� ����
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
        // ��� �� ó���� �� �̻� �������� �ʰ� ����
        if (isDead == true) { return;}

        // �����̽��� �������� && �ִ� ���� Ƚ��(2)�� �������� �ʾҴٸ�
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 1)
        {
            jumpCount++;

            // ����Ű ������ ���� �������� ������ ����
            //playerRigid.velocity = Vector2.zero;


            // ������ٵ� �������� �� �ֱ�
            playerRigid.AddForce(new Vector2(0, jumpForce));
        }  // if : �÷��̾ ���� �� ��

        // ���� ����Ű�� ������ ���� �����δ�.
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            playerRigid.velocity = new Vector2( -1 * 5f,  0f);
        }
        else if(Input.GetKeyUp(KeyCode.LeftArrow))     // ���ڵ� ���� �����ؾߵȴ�!!!!!!
        {
            // ���� ����Ű�� ���� ���� ������ �����.(�����̵�� ���� �� �������� ����.)
            playerRigid.velocity = Vector2.zero;
            //playerRigid.velocity = new Vector2(0, playerRigid.velocity.y);
        }

        // ������ ����Ű�� ������ ���� �����δ�.
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            playerRigid.velocity = new Vector2(1 * 5f, 0f);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            // ������ ����Ű�� ���� ���� ������ �����.
            playerRigid.velocity = Vector2.zero;

        }

    }

    private void Die()
    {
        // �ִϸ������� Die Ʈ���� �Ķ���͸� ��
        playerAni.SetTrigger("Die");

        // �ӵ��� ���η� ����
        playerRigid.velocity = Vector2.zero;
        // ��� ���¸� true�� ����
        isDead = true;
        GFunc.Log(isDead);
    }

    //! �ٴڿ� ��Ҵ��� üũ�ϴ� �Լ�
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PLAYER_STEP_ON_Y_ANGLE_MIN < collision.contacts[0].normal.y)
        {
            isGround = true;
            jumpCount = 0;
        }       // if: 45�� ���� �ϸ��� ���� ���� ���
    }       // OnCollisionEnter2D()

    //! �ٴڿ��� ������� üũ�ϴ� �Լ�
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;
    }       // OnCollisionExit2D()
}
