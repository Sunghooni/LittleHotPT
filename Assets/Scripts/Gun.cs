using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject player;
    public GameObject playerHand;
    public bool isHolded = false;
    public bool isThrowing = false;

    private Transform tr;
    private Rigidbody rb;
    private Transform playerTr;
    private PlayerMove _playerMove;
    private const float fixedRotY = -90;

    private void Awake()
    {
        tr = gameObject.GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody>();
        playerTr = player.GetComponent<Transform>();
        _playerMove = player.GetComponent<PlayerMove>();
    }

    private void FixedUpdate()
    {
        if(isHolded)
        {
            HoldingToHand();
        }
    }

    public void HoldedToHand()
    {
        StartCoroutine(MoveToPos());
    }

    IEnumerator MoveToPos()
    {
        float timer = 0;
        Vector3 originPos = tr.position;
        Vector3 originRot = tr.eulerAngles;

        rb.useGravity = false;
        _playerMove.isHolding = true;
        yield return new WaitForSeconds(0.3f);

        while (timer <= 1f)
        {
            timer += Time.deltaTime * 2;

            Vector3 toPos = playerHand.transform.position;
            Vector3 toRot = new Vector3(0, playerTr.eulerAngles.y + fixedRotY, 0);

            tr.position = Vector3.Lerp(originPos, toPos, timer);
            tr.eulerAngles = Vector3.Lerp(originRot, toRot, timer);

            yield return new WaitForFixedUpdate();
        }

        isHolded = true;
    }

    private void HoldingToHand()
    {
        Vector3 playerRot = playerTr.eulerAngles;

        tr.position = playerHand.transform.position;
        tr.eulerAngles = new Vector3(0, playerRot.y + fixedRotY, 0);
    }

    public void ShotGun()
    {
        StartCoroutine(ShotMotion());
    }

    IEnumerator ShotMotion()
    {
        float motionSpeed = 3;
        float timer = 0;
        bool isUp = true;
        Vector3 downAngle;
        Vector3 upAngle;

        while (timer >= 0)
        {
            if (timer > 1)
            {
                isUp = false;
            }

            downAngle = new Vector3(0, playerTr.eulerAngles.y + fixedRotY, 0);
            upAngle = downAngle + Vector3.forward * 10;

            timer += (isUp ? Time.deltaTime : -Time.deltaTime) * motionSpeed;
            tr.eulerAngles = Vector3.Lerp(downAngle, upAngle, timer);

            yield return new WaitForFixedUpdate();
        }

        _playerMove.animator.SetBool("Shot", false);
    }

    public void ThrowMotion()
    {
        Invoke(nameof(Throwing), 0.5f);
    }

    private void Throwing()
    {
        isHolded = false;
        isThrowing = true;
        _playerMove.isHolding = false;
        _playerMove.holdingObj = null;

        rb.useGravity = true;
        rb.AddForce(transform.right * 10, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isThrowing)
        {
            isThrowing = false;
        }
    }
}