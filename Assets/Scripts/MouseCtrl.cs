using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCtrl : MonoBehaviour
{
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.transform.tag.Equals("gun") && !hit.transform.GetComponent<Gun>().isHolded)
                {
                    hit.transform.GetComponent<Gun>().HoldedToHand();
                }
            }
        }
    }
}