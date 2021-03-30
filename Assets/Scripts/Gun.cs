using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject player;
    public GameObject playerHand;

    private float fixedRotY = -90;

    private void Start()
    {
        HoldedToHand();
    }

    private void FixedUpdate()
    {
        HoldingToHand();
    }

    private void HoldingToHand()
    {
        var handAngle = playerHand.transform.TransformDirection(playerHand.transform.eulerAngles);
        var rotY = fixedRotY + player.transform.eulerAngles.y;

        gameObject.transform.position = playerHand.transform.position;
        gameObject.transform.eulerAngles = handAngle;
        //gameObject.transform.eulerAngles = new Vector3(handAngle.x, rotY, handAngle.z);
    }

    public void HoldedToHand()
    {
        StartCoroutine(MoveToPos());
    }

    IEnumerator MoveToPos()
    {
        float timer = 0;
        Vector3 originPos = gameObject.transform.position;
        Vector3 toPos = playerHand.transform.position;

        while(timer <= 1f)
        {
            toPos = playerHand.transform.position;
            timer += Time.deltaTime;
            gameObject.transform.position = Vector3.Slerp(originPos, toPos, timer);
            yield return new WaitForFixedUpdate();
        }
    }
}
