using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectionSwitcher : MonoBehaviour
{
    [SerializeField] private Camera m_camera;

    public void ChangeValue(int value)
    {
        ChangeType((ProjectionType)value);
    }
    public void ChangeType(ProjectionType type)
    {
        switch (type)
        {
            case ProjectionType.Orthographic:
                m_camera.orthographic = true;
                break;
            case ProjectionType.Perspective:
                m_camera.orthographic = false;
                break;
        }
    }

    public enum ProjectionType
    {
        Orthographic,
        Perspective
    }
}
