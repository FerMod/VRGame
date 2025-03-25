using UnityEngine;
public class DebugDrawCenter : MonoBehaviour
{
#if UNITY_EDITOR
    public bool enable = true;
    public float size = 0.25f;
    public Color color = Color.green;

    private void OnDrawGizmos()
    {
        DrawPosition(size, color);
    }

    private void DrawPosition(float size, Color color)
    {
        if (!enable) return;

        var lineSize = size * 0.5f;
        Gizmos.color = color;

        var linePosition = transform.position;
        Gizmos.DrawLine(linePosition + lineSize * Vector3.left, linePosition + lineSize * Vector3.right);
        Gizmos.DrawLine(linePosition + lineSize * Vector3.up, linePosition + lineSize * Vector3.down);
        Gizmos.DrawLine(linePosition + lineSize * Vector3.forward, linePosition + lineSize * Vector3.back);
    }
#endif
}
