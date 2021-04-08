using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarTester : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void OnAnimatorIK(int layerIndex)
    {
        //Hand, Foot : AvatarIKGoal.Part
        //Elbow, Etc : AvatarIKHint.Part
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);

        Vector3 handPosition = new Vector3(0, 0, 1);
        animator.SetIKPosition(AvatarIKGoal.RightHand, handPosition);
    }
}
