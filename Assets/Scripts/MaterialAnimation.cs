using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialAnimation : MonoBehaviour
{
    [SerializeField] private float m_speed;

    [SerializeField] private Gradient m_gradientOne;
    [SerializeField] private Gradient m_gradientTwo;
    
    [SerializeField] private Material m_material;
    private static readonly int ColorEnd = Shader.PropertyToID("_ColorEnd");
    private static readonly int ColorStart = Shader.PropertyToID("_ColorStart");

    private void Start()
    {
        StartCoroutine(AnimationColor());
    }

    private void Reset()
    {
        m_material = GetComponent<MeshRenderer>().sharedMaterial;
    }

    private IEnumerator AnimationColor()
    {
        Color fromColorTmp = Color.black;
        Color toColorTmp = Color.black;
        
        var wait = new WaitForFixedUpdate();
        while (true)
        {
            fromColorTmp = m_gradientOne.Evaluate(Mathf.Repeat(Time.time * m_speed, 1f));
            toColorTmp = m_gradientTwo.Evaluate(Mathf.Repeat(Time.time * m_speed, 1f));

            m_material.SetColor(ColorStart, fromColorTmp);
            m_material.SetColor(ColorEnd, toColorTmp);
            yield return wait;
            
        }
    }

    private Color AddColorPoint(Color color, float add)
    {
        Color colorTmp = color;

        if (colorTmp.r > 0 && colorTmp.g > 0 && colorTmp.b > 0)
        {
            color.r += add;
            return color;
        }
        
        if (colorTmp.r < 1 && colorTmp.g == 0 && colorTmp.b == 0)
        {
            colorTmp.r += add;
            return colorTmp;
        }

        if (colorTmp.r >= 1 && colorTmp.b < 1 && colorTmp.g == 0)
        {
            colorTmp.b += add;
            return colorTmp;
        }
        
        if (colorTmp.r >= 1 && colorTmp.b >= 1 && colorTmp.g == 0)
        {
            colorTmp.r -= add;
            return colorTmp;
        }
        
        if (colorTmp.r >= 0 && colorTmp.b == 1 && colorTmp.g == 0)
        {
            colorTmp.r -= add;
            return colorTmp;
        }
        
        if (colorTmp.r == 0 && colorTmp.b == 1 && colorTmp.g >= 0)
        {
            colorTmp.g += add;
            return colorTmp;
        }
        
        if (colorTmp.r == 0 && colorTmp.b >= 0 && colorTmp.g == 1)
        {
            colorTmp.b -= add;
            return colorTmp;
        }
        
        if (colorTmp.r >= 0 && colorTmp.b == 0 && colorTmp.g == 1)
        {
            colorTmp.r += add;
            return colorTmp;
        }
        
        if (colorTmp.r == 1 && colorTmp.b == 0 && colorTmp.g >= 0)
        {
            colorTmp.g -= add;
            return colorTmp;
        }
        
        return colorTmp;
    }
}
