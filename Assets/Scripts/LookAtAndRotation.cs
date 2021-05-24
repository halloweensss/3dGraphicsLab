using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class LookAtAndRotation : MonoBehaviour
{
    [SerializeField] private Transform m_target;

    [SerializeField] private float m_speed;

    private void Start()
    {
        StartCoroutine(AnimationColor());
    }

    private IEnumerator AnimationColor()
    {
        var wait = new WaitForFixedUpdate();
        while (true)
        {
            transform.LookAt(m_target);
            transform.RotateAround(m_target.position, Vector3.up, Time.deltaTime * m_speed);
            yield return wait;
        }
    }
}
