using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [Range(0.01f, 1f)]
    public float mouseSensitivity = 0.7f;

    bool isLock;

    private void Start()
    {
        isLock = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //Cursor lock
        if(isLock && Input.GetKeyDown(KeyCode.Escape))
        {
            isLock = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if(!isLock && Input.GetKeyDown(KeyCode.L))
        {
            isLock = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    //mouse inputs
    public Vector2 mouseControls()
    {
        if (!isLock)
            return Vector3.zero;

        float x = Input.GetAxis("Mouse X") * mouseSensitivity * 20f;
        float y = Input.GetAxis("Mouse Y") * mouseSensitivity * 20f;

        return new Vector2(x, y);
    }
}
