using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CircleArea : MonoBehaviour
{
    [SerializeField] private float m_Radius;
    public float Radius => m_Radius;

    public Vector2 GetRandomInsideZone()
    {
        return (Vector2)transform.position + (Vector2)UnityEngine.Random.insideUnitSphere * m_Radius;
    }

    #if UNITY_EDITOR
    private static Color GizmoColor = new Color(0f, 1f, 0f, 0.3f);

    private void OnDrawGizmosSelected()
    {
        Handles.color = GizmoColor;
        Handles.DrawSolidDisc(transform.position, transform.forward, m_Radius);
    }
    #endif
}
