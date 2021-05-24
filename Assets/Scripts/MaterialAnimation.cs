using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialAnimation : MonoBehaviour
{
    [SerializeField] private Color m_fromColor;
    [SerializeField] private Color m_toColor;

    [SerializeField] private float m_speed;
    
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
        m_fromColor = m_material.GetColor(ColorStart);
        m_toColor = m_material.GetColor(ColorEnd);
    }

    private IEnumerator AnimationColor()
    {
        Color fromColorTmp = m_fromColor;
        Color toColorTmp = m_toColor;
        
        var wait = new WaitForFixedUpdate();
        while (true)
        {
            float value = Time.deltaTime * m_speed;
            fromColorTmp = AddColorPoint(fromColorTmp, value);
            toColorTmp = AddColorPoint(toColorTmp, value);
            
            m_material.SetColor(ColorStart, fromColorTmp);
            m_material.SetColor(ColorEnd, toColorTmp);
            yield return wait;
            
        }
    }

    private Color AddColorPoint(Color color, float add)
    {
        Color colorTmp = color;

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
