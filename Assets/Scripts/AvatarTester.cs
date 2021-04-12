using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarTester : MonoBehaviour
{
    private Animator animator;
    private float timer = 0;
    private float progress;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void OnAnimatorIK(int layerIndex)
    {
        Debug.Log(timer);
        
        timer += Input.GetAxisRaw("Mouse Y");
        timer = timer < 3 ? timer : 3;
        timer = timer > -8 ? timer : -8;

        progress = timer * 0.02f;
        Vector3 toPos = animator.GetIKPosition(AvatarIKGoal.RightHand);
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        animator.SetIKPosition(AvatarIKGoal.RightHand, toPos + Vector3.up * progress);
    }
}
