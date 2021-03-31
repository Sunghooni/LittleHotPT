using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCtrl : MonoBehaviour
{
    public PlayerMove playerMove;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public GameObject ShotRay()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (hit.transform.tag.Equals("gun") && !hit.transform.GetComponent<Gun>().isHolded)
            {
                hit.transform.GetComponent<Gun>().HoldedToHand();
                return hit.transform.gameObject;
            }
        }

        return null;
    }
}