using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    private float mouseY;
    private float rotSpeed = 2;
    private float cameraX = 0;

    public float CameraX
    {
        get
        {
            cameraX = cameraX <= 0 ? cameraX + 360 : cameraX;
            cameraX = cameraX > 360 ? cameraX - 360 : cameraX;

            if (cameraX > 30 && cameraX < 180)
            {
                cameraX = 30;
            }
            else if(cameraX < 330 && cameraX > 180)
            {
                cameraX = 330;
            }
            return cameraX;
        }
        set
        {
            cameraX = value;
        }
    }

    private void Update()
    {
        mouseY = Input.GetAxisRaw("Mouse Y");
    }

    private void FixedUpdate()
    {
        var cameraRot = gameObject.transform.eulerAngles;

        CameraX += -mouseY * rotSpeed;
        gameObject.transform.eulerAngles = new Vector3(CameraX, cameraRot.y, cameraRot.z);
    }
}
