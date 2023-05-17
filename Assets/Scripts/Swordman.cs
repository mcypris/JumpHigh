using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordman : PlayerController
{
    public float jumpValue = 0.0f;
    public bool canJump = true;
    private float moveInput;

    private void Start()
    {
        m_CapsulleCollider  = this.transform.GetComponent<CapsuleCollider2D>();
        m_Anim = this.transform.Find("model").GetComponent<Animator>();
        m_rigidbody = this.transform.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        checkInput();

        moveInput = Input.GetAxisRaw("Horizontal");

        if(jumpValue == 0.0f && isGrounded)
        {
            m_rigidbody.velocity = new Vector2(moveInput * MoveSpeed, m_rigidbody.velocity.y);
        }

        if(Input.GetKey("space") && isGrounded && canJump)
        {
            jumpValue += 0.1f;
        }

        if(jumpValue >= 15f && isGrounded)
        {
            float tempx = moveInput * MoveSpeed;
            float tempy = jumpValue;
            m_rigidbody.velocity = new Vector2(tempx, tempy);
            Invoke("ResetJump", 0.2f);
            canJump = false;
        }

        if(Input.GetKeyDown("space") && isGrounded && canJump)
        {
            m_rigidbody.velocity = new Vector2(0.0f, m_rigidbody.velocity.y);
        }

        if(Input.GetKeyUp("space"))
        {
            if(isGrounded)
            {
                m_rigidbody.velocity = new Vector2(moveInput * MoveSpeed, jumpValue);
                jumpValue = 0.0f;
                canJump = false;
            }
            canJump = true;
        }
    }

    void ResetJump()
    {
        canJump = true;
        jumpValue = 0;
    }

    public void checkInput()
    {
        m_MoveX = Input.GetAxis("Horizontal");

        GroundCheckUpdate();

        if (m_MoveX == 0)
        {
            if (!OnceJumpRayCheck)
            {
                m_Anim.Play("Idle");
            }
        }
        else
        {
            m_Anim.Play("Run");
        }

        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && jumpValue == 0)
        {
            if (!Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {    
                Filp(false);
            }
        }

        else if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && jumpValue == 0)
        {
            if (!Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {    
                Filp(true);
            }
        }
    }

    protected override void LandingEvent()
    {
        if (!m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {    
            m_Anim.Play("Idle");
        }
    }
}
