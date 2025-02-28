using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class NpcBase : MonoBehaviour
{
    public bool HasReachedDestination { get; private set; } = true;

    public event Action OnDestinationReached;

    private NavMeshAgent _agent;
    protected NavMeshAgent Agent
    {
        get
        {
            if (_agent == null)
            {
                TryGetComponent(out _agent);
            }
            return _agent;
        }
    }

    [Header("Debug")]
    public bool showPath = true;

    void Start()
    {
    }

    public virtual bool MoveTo(Vector3 position)
    {
        HasReachedDestination = false;
        return Agent.SetDestination(position);
    }

    void Update()
    {
        HandleDestinationReached();
    }

    private void HandleDestinationReached()
    {
        if (HasReachedDestination) return;
        if (Agent.remainingDistance > Agent.stoppingDistance) return;
        if (Agent.pathPending) return;

        HasReachedDestination = true;
        OnDestinationReached?.Invoke();
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        DrawNavMeshPath();
    }

    private void DrawNavMeshPath()
    {
        if (HasReachedDestination) return;
        if (!showPath) return;
        if (Agent == null) return;
        if (Agent.path == null) return;

        DrawPath(Agent.path);
        DrawTargetPoint(Agent.path);
    }

    private void DrawPath(NavMeshPath path)
    {
        Gizmos.color = Color.yellow;

        var previousCorner = transform.position;
        foreach (var corner in path.corners)
        {
            Gizmos.DrawLine(previousCorner, corner);
            previousCorner = corner;
        }
    }

    private void DrawTargetPoint(NavMeshPath path)
    {
        if (path.corners.Length == 0) return;

        float size = 0.5f;
        Gizmos.color = Color.red;

        var targetPosition = path.corners[^1];
        Gizmos.DrawLine(targetPosition + Vector3.left * size, targetPosition + Vector3.right * size);
        Gizmos.DrawLine(targetPosition + Vector3.up * size, targetPosition + Vector3.down * size);
        Gizmos.DrawLine(targetPosition + Vector3.forward * size, targetPosition + Vector3.back * size);
    }
#endif
}
