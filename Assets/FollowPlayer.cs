using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Vector3 _cameraOffset;

    [Range(0.0f,1.0f)]
    public float smoothFactor = 0.5f;
    public Transform playerTransform;
    public bool lookAtPlayer = true;
    public bool rotateAroundPlayer = true;
    public float rotationSpeed = 5.0f;
    public bool autoRotate = false;
    public float speed = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        _cameraOffset = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotateAroundPlayer)
        {
            Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);

            if (autoRotate)
            {
                camTurnAngle = Quaternion.AngleAxis(speed * rotationSpeed, Vector3.up);
            }

            _cameraOffset = camTurnAngle * _cameraOffset;
        }

        Vector3 newPos = playerTransform.position + _cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);

        if (lookAtPlayer || rotateAroundPlayer)
        {
            transform.LookAt(playerTransform);
        }


    }
}
