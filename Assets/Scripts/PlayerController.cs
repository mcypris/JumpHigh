using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerController :MonoBehaviour
{
    public bool isGrounded = false;
    public bool OnceJumpRayCheck = false;

    public bool Is_DownJump_GroundCheck = false;
    protected float m_MoveX;
    public Rigidbody2D m_rigidbody;
    protected CapsuleCollider2D m_CapsulleCollider;
    protected Animator m_Anim;

    [Header("[Setting]")]
    public float MoveSpeed = 6;

    protected void AnimUpdate()
    {
        if (m_MoveX == 0)
        {
            if (!OnceJumpRayCheck)
                m_Anim.Play("Idle");
        }
        else
        {
            m_Anim.Play("Run");
        }
    }

    protected void Filp(bool bLeft)
    {
        transform.localScale = new Vector3(bLeft ? 1 : -1, 1, 1);
    }

    IEnumerator GroundCapsulleColliderTimmerFuc()
    {
        yield return new WaitForSeconds(0.3f);
        m_CapsulleCollider.enabled = true;
    }

    Vector2 RayDir = Vector2.down;

    float PretmpY;
    float GroundCheckUpdateTic = 0;
    float GroundCheckUpdateTime = 0.01f;
    protected void GroundCheckUpdate()
    {
        if (!OnceJumpRayCheck) return;

        GroundCheckUpdateTic += Time.deltaTime;

        if (GroundCheckUpdateTic > GroundCheckUpdateTime)
        {
            GroundCheckUpdateTic = 0;

            if (PretmpY == 0)
            {
                PretmpY = transform.position.y;
                return;
            }

            float reY = transform.position.y - PretmpY;

            if (reY <= 0)
            {
                if (isGrounded)
                {
                    LandingEvent();
                    OnceJumpRayCheck = false;
                }
            }
            PretmpY = transform.position.y;
        }
    }
    protected abstract void LandingEvent();
}
