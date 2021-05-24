using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraKeyboard : MonoBehaviour
{
    [SerializeField] private float m_speed;
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * m_speed;
        float deltaY = Input.GetAxis("Vertical") * m_speed;

        transform.position += Vector3.up * deltaY;
        transform.position += Vector3.right * deltaX;
    }
}
