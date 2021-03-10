using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    private Camera theCam;
    private float old_rotation = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        old_rotation = transform.eulerAngles.z;
        transform.LookAt(theCam.transform);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, old_rotation);
    }
}
