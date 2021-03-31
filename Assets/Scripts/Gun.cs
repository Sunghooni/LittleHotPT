using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject player;
    public GameObject playerHand;
    public bool isHolded = false;

    private float fixedRotY = -90;
    private float upRotX = 0;
    private float downRotX = 0;

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
        //Unity Animation 만들어서 실행
    }
}