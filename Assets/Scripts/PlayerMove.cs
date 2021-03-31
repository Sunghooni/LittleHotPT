using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Animator animator;

    private int vert;
    private int horz;
    private float mouseX;

    private int moveSpeed = 2;
    private int rotSpeed = 2;
    private int motionChangeSpeed = 2;

    [SerializeField] private bool isActing = false;
    [SerializeField] private bool isHolding = false;

    public void Update()
    {
        GetInput();
        WalkMotion();
        AimWalkMotion();
        ThrowMotion();
        HitMotion();
        CheckActing();
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
        mouseX = Input.GetAxis("Mouse X");
    }

    private void MovePlayer()
    {
        int vertSpeed = vert * moveSpeed;
        int horzSpeed = horz * moveSpeed;

        if (!isActing)
        {
            gameObject.transform.Translate(Vector3.forward * Time.deltaTime * vertSpeed);
            gameObject.transform.Translate(Vector3.right * Time.deltaTime * horzSpeed);
        }
    }

    private void RotatePlayer()
    {
        if (!isActing)
        {
            gameObject.transform.Rotate(Vector3.up * rotSpeed * mouseX);
        }
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
        if (Input.GetKeyDown(KeyCode.Space) && !isActing)
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
        string hitType = Random.Range(0, 2) == 0 ? "Hit1" : "Hit2";

        if (Input.GetMouseButtonDown(0) && !isActing)
        {
            animator.SetBool(hitType, true);
        }
        else
        {
            animator.SetBool(hitType, false);
        }
    }

    private void CheckActing()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Act"))
        {
            isActing = true;
        }
        else if(animator.GetCurrentAnimatorStateInfo(0).IsTag("Motion"))
        {
            isActing = false;
        }
    }
}