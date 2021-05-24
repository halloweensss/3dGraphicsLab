using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotation : MonoBehaviour
{
    [SerializeField] private float m_speed;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float rotX = Input.GetAxis("Mouse X") * m_speed * Time.deltaTime;
            float rotY = Input.GetAxis("Mouse Y") * m_speed * Time.deltaTime;

            transform.RotateAround(Vector3.up, -rotX);
            transform.RotateAround(Vector3.right, rotY);
        }
    }
}
