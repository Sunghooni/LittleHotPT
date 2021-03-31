using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject player;
    public GameObject playerHand;

    public bool isHolded = false;
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
        player.GetComponent<PlayerMove>().isHolding = true;

        yield return new WaitForSeconds(0.3f);

        while (timer <= 1f)
        {
            Vector3 toPos = playerHand.transform.position;
            timer += Time.deltaTime * 2;
            gameObject.transform.position = Vector3.Lerp(originPos, toPos, timer);
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
}
