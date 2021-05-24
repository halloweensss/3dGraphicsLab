using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshGenerator : MonoBehaviour
{
    [SerializeField] private List<Vector3> m_points;

    [SerializeField] private float m_x;
    [SerializeField] private float m_y;

    [SerializeField] private int m_dotsCount;

    [SerializeField] private float m_a;
    [SerializeField] private float m_b;
    [SerializeField] private float m_c;

    [SerializeField] private int num;

    [SerializeField] private Mesh m_mesh;
    
    private void Start()
    {
        
    }

    [ContextMenu("Create Mesh")]
    private void Create()
    {
        m_points = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        float stepX = m_x * 2 / m_dotsCount;
        float stepY = m_y * 2 / m_dotsCount;
        for (float y = -m_y; y <= m_y; y+=stepY)
        {
            for (float x = -m_x; x <= m_x; x+=stepX)
            {
                m_points.Add(new Vector3(x, y, F(x, y)));
                uvs.Add(new Vector2((x + m_x) / (m_x * 2), (y + m_y) / (m_y * 2)));
            }
        }

        int count = m_points.Count;
        
        for(int i = count - 1; i >= 0; i--)
        {
            var point = m_points[i];
            m_points.Add(new Vector3(point.x, point.y, -point.z));
            uvs.Add(new Vector2((point.x + m_x) / (m_x * 2), (point.y + m_y) / (m_y * 2)));
        }

        int[] triangles = new int[m_dotsCount * m_dotsCount * 2 * 2 * 6];

        int ti = 0;
        int vi = 0;
        
        for (int y = 0; y < m_dotsCount; y++, vi++)
        {
            for (int x = 0; x < m_dotsCount; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 1] = triangles[ti + 4] = vi + m_dotsCount + 1;
                triangles[ti + 2] = triangles[ti + 3] = vi + 1;
                triangles[ti + 5] = vi + m_dotsCount + 2;
                
                /*ti += 6;
                
                triangles[ti] = vi;
                triangles[ti + 1] = triangles[ti + 4] = vi + 1;
                triangles[ti + 2] = triangles[ti + 3] = vi + m_dotsCount + 1;
                triangles[ti + 5] = vi + m_dotsCount + 2;*/
            }
        }

        vi = m_points.Count / 2;
        
        for (int y = 0; y < m_dotsCount; y++, vi++)
        {
            for (int x = 0; x < m_dotsCount; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 1] = triangles[ti + 4] = vi + 1;
                triangles[ti + 2] = triangles[ti + 3] = vi + m_dotsCount + 1;
                triangles[ti + 5] = vi + m_dotsCount + 2;
            }
        }
        
        m_mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = m_mesh;
        m_mesh.name = "Grid";

        m_mesh.vertices = m_points.ToArray();
        m_mesh.uv = uvs.ToArray();
        m_mesh.triangles = triangles;
        m_mesh.RecalculateBounds();
        m_mesh.RecalculateNormals();
        m_mesh.RecalculateTangents();
    }

    private float F(float x, float y)
    {
        float x2 = x * x;
        float y2 = y * y;
        float a2 = m_a * m_a;
        float b2 = m_b * m_b;
        float c2 = m_c * m_c;
        return Mathf.Sqrt(c2 * (x2 / a2 + y2 / b2 + 1));
    }

    private void OnDrawGizmos()
    {
        /*if (m_points == null)
        {
            return;
        }

        int c = 0;
        foreach (var point in m_points)
        {
            Gizmos.color = Color.Lerp(Color.yellow, Color.cyan, c * 1.0f / m_points.Count);
            Gizmos.DrawSphere(point, 0.2f);
            c++;
        }

        Gizmos.color = Color.blue;

        var dot = m_points[num];
        Gizmos.DrawSphere(dot, 0.2f);*/
    }
}
