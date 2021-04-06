using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public MouseCtrl mouseCtrl;
    public Animator animator;
    public GameObject holdingObj;
    public bool isActing = false;
    public bool isHolding = false;

    private int vert;
    private int horz;
    private float mouseX;

    private const int moveSpeed = 2;
    private const int rotSpeed = 2;
    private const int motionChangeSpeed = 3;

    public void Update()
    {
        CheckActing();
        GetInput();
        WalkMotion();
        AimWalkMotion();
        ThrowMotion();
        LeftMouseBtnClick();
    }

    public void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void GetInput()
    {
        vert = int.Parse(Input.GetAxisRaw("Vertical").ToString());
        horz = int.Parse(Input.GetAxisRaw("Horizontal").ToString());
        mouseX = Input.GetAxisRaw("Mouse X");
    }

    private void MovePlayer()
    {
        int vertSpeed = vert * moveSpeed;
        int horzSpeed = horz * moveSpeed;

        gameObject.transform.Translate(Vector3.forward * Time.deltaTime * vertSpeed);
        gameObject.transform.Translate(Vector3.right * Time.deltaTime * horzSpeed);
    }

    private void RotatePlayer()
    {
        gameObject.transform.Rotate(Vector3.up * rotSpeed * mouseX);
    }

    private void WalkMotion()
    {
        if(isHolding == false)
        {
            if (vert != 0 || horz != 0)
            {
                animator.SetBool("Walk", true);
            }
            else
            {
                animator.SetBool("Walk", false);
            }
        }
    }

    private void AimWalkMotion()
    {
        float progress = animator.GetFloat("AimWalk");
        animator.SetBool("Holding", isHolding);

        if (isHolding == true)
        {
            if (vert == 0 && progress > 0f)
            {
                animator.SetFloat("AimWalk", progress - Time.deltaTime * motionChangeSpeed);
            }
            if (vert != 0 && progress < 1f)
            {
                animator.SetFloat("AimWalk", progress + Time.deltaTime * motionChangeSpeed);
            }
        }
    }

    private void ThrowMotion()
    {
        if (Input.GetMouseButtonDown(1) && holdingObj != null)
        {
            animator.SetBool("Throw", true);
            
            if (holdingObj.TryGetComponent(out Gun gun) && !isActing) //총을 들고 있을 경우 실행
            {
                gun.ThrowMotion();
            }
        }
        else
        {
            animator.SetBool("Throw", false);
        }
    }

    private void LeftMouseBtnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!GrabGun())
            {
                HitMotion();
                ShotGunMotion();
            }
        }
        else
        {
            animator.SetBool("Hit1", false);
            animator.SetBool("Hit2", false);
        }
    }

    private bool GrabGun()
    {
        if (!isHolding)
        {
            holdingObj = mouseCtrl.ShotRay();

            if (holdingObj != null)
            {
                return true;
            }
        }
        return false;
    }

    private void HitMotion()
    {
        string hitType = Random.Range(0, 2) == 0 ? "Hit1" : "Hit2";

        if (!isHolding)
        {
            animator.SetBool(hitType, true);
        }
    }

    private void ShotGunMotion()
    {
        if (!isActing && holdingObj != null)
        {
            if (holdingObj.TryGetComponent(out Gun gun))
            {
                animator.SetBool("Shot", true);
                gun.ShotGun();
            }
        }
    }

    private void CheckActing()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Act"))
        {
            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1)
            {
                isActing = true;
            }
            else
            {
                isActing = false;
            }
        }
        else if(animator.GetCurrentAnimatorStateInfo(0).IsTag("Motion"))
        {
            isActing = false;
        }
    }
}