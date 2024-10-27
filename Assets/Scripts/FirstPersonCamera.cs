using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private float turnSpeed;
    [SerializeField] private GameObject pov;
    public Vector3 heigth;
    private float minAngle = -90.0f;
    private float maxAngle = 90f;
    private float rotX;

    private void Start()
    {
        heigth = new Vector3(0, pov.transform.position.y - 0.5f, 0);
    }
    void Update()
    {
        float y = Input.GetAxis("Mouse X") * turnSpeed;
        rotX += Input.GetAxis("Mouse Y") * turnSpeed;

        rotX = Mathf.Clamp(rotX, minAngle, maxAngle);

        transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + y, 0);
        transform.position = pov.transform.position + (heigth/2); 

    }
}
