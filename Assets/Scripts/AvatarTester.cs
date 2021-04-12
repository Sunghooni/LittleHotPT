using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarTester : MonoBehaviour
{
    public GameObject Camera;
    public Transform handPos;
    private Animator animator;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        animator = gameObject.GetComponent<Animator>();
    }

    void OnAnimatorIK(int layerIndex)
    {
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);

        animator.SetIKPosition(AvatarIKGoal.RightHand, handPos.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, handPos.rotation);
    }
}