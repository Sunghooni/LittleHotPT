using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject player;
    public GameObject playerHand;
    public bool isHolded = false;
    public bool isThrowing = false;

    private float fixedRotY = -90;

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
        Vector3 originPos = gameObject.transform.position;
        Vector3 originRot = gameObject.transform.eulerAngles;

        gameObject.GetComponent<Rigidbody>().useGravity = false;
        player.GetComponent<PlayerMove>().isHolding = true;
        yield return new WaitForSeconds(0.3f);

        while (timer <= 1f)
        {
            timer += Time.deltaTime * 2;

            Vector3 toPos = playerHand.transform.position;
            Vector3 toRot = new Vector3(0, player.transform.eulerAngles.y + fixedRotY, 0);

            gameObject.transform.position = Vector3.Lerp(originPos, toPos, timer);
            gameObject.transform.eulerAngles = Vector3.Lerp(originRot, toRot, timer);

            yield return new WaitForFixedUpdate();
        }

        isHolded = true;
    }

    private void HoldingToHand()
    {
        Vector3 playerRot = player.transform.eulerAngles;

        gameObject.transform.position = playerHand.transform.position;
        gameObject.transform.eulerAngles = new Vector3(0, playerRot.y + fixedRotY, 0);
    }

    public void ShotGun()
    {
        StartCoroutine(ShotMotion());
    }

    IEnumerator ShotMotion()
    {
        float timer = 0;
        bool isUp = true;

        while (timer >= 0)
        {
            if (timer > 1)
            {
                isUp = false;
            }

            Vector3 downAngle = new Vector3(0, player.transform.eulerAngles.y + fixedRotY, 0);
            Vector3 upAngle = downAngle + Vector3.forward * 10;
            
            timer += isUp ? Time.deltaTime * 3 : -Time.deltaTime * 3;
            gameObject.transform.eulerAngles = Vector3.Lerp(downAngle, upAngle, timer);

            yield return new WaitForFixedUpdate();
        }

        player.GetComponent<PlayerMove>().animator.SetBool("Shot", false);
    }

    public void ThrowMotion()
    {
        Invoke("Throwing", 0.5f);
    }

    private void Throwing()
    {
        isHolded = false;
        isThrowing = true;
        player.GetComponent<PlayerMove>().isHolding = false;
        player.GetComponent<PlayerMove>().holdingObj = null;

        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<Rigidbody>().AddForce(transform.right * 10, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isThrowing)
        {
            isThrowing = false;
        }
    }
}