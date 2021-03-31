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
        Vector3 playerRot = player.transform.eulerAngles;
        Vector3 gunRot = gameObject.transform.eulerAngles;

        gameObject.transform.position = playerHand.transform.position;
        gameObject.transform.eulerAngles = new Vector3(gunRot.x, playerRot.y + fixedRotY, gunRot.z);
    }

    public void HoldedToHand()
    {
        StartCoroutine(MoveToPos());
    }

    IEnumerator MoveToPos()
    {
        float timer = 0;
        Vector3 originPos = gameObject.transform.position;
        
        while (timer <= 1f)
        {
            Vector3 toPos = playerHand.transform.position;
            timer += Time.deltaTime;
            gameObject.transform.position = Vector3.Slerp(originPos, toPos, timer);
            yield return new WaitForFixedUpdate();
        }
    }
}