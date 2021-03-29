using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Animator animator;

    private int vert;
    private int horz;
    private int moveSpeed = 2;
    private int motionChangeSpeed = 2;

    public void Update()
    {
        vert = int.Parse(Input.GetAxisRaw("Vertical").ToString());
        horz = int.Parse(Input.GetAxisRaw("Horizontal").ToString());

        AimWalkMotion();
        ThrowMotion();
        HitMotion();
    }

    public void FixedUpdate()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        int speed = vert * moveSpeed;
        gameObject.transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void AimWalkMotion()
    {
        float progress = animator.GetFloat("AimWalk");

        if (vert == 0 && progress > 0f)
        {
            animator.SetFloat("AimWalk", progress - Time.deltaTime * motionChangeSpeed);
        }
        if (vert != 0 && progress < 1f)
        {
            animator.SetFloat("AimWalk", progress + Time.deltaTime * motionChangeSpeed);
        }
    }

    private void ThrowMotion()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Throw", true);
        }
        else
        {
            animator.SetBool("Throw", false);
        }
    }

    private void HitMotion()
    {
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetBool("Hit1", true);
        }
        else
        {
            animator.SetBool("Hit1", false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            animator.SetBool("Hit2", true);
        }
        else
        {
            animator.SetBool("Hit2", false);
        }
    }
}
