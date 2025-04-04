using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class SpawnPoint
{
    public Transform transform;
    public Vector3 area = new(1, 0, 1);

    public Vector3 RandomPosition()
    {
        return transform.position + new Vector3(
            Random.Range(-area.x * 0.5f, area.x * 0.5f),
            Random.Range(-area.y * 0.5f, area.y * 0.5f),
            Random.Range(-area.z * 0.5f, area.z * 0.5f)
        );
    }

#if UNITY_EDITOR
    public void DrawGizmos(Color? color)
    {
        if (transform == null) return;

        if (color != null)
        {
            Gizmos.color = (Color)color;
        }

        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);

        Gizmos.DrawWireCube(Vector3.zero, area);
        Gizmos.DrawCube(Vector3.zero, Vector3.one * 0.08f); // Draw center point
    }
#endif
}
