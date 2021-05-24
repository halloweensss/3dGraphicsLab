using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScale : MonoBehaviour
{
    [SerializeField] private float m_speed;
    
    void Update()
    {
        float value = Input.mouseScrollDelta.y * m_speed * Time.deltaTime;
        Vector3 scale = transform.localScale + Vector3.one * value;
        
        if (scale.x <= 0.01f ||
            scale.y <= 0.01f ||
            scale.z <= 0.01f)
        {
            return;
        }
        
        transform.localScale += Vector3.one * value;
    }
}
