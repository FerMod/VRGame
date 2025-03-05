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

#if UNITY_EDITOR
    [Header("Debug")]
    public bool showPath = false;
    public Color pathColor = Color.yellow;

    [Space]
    public bool showDestination = false;
    public Color destinationColor = Color.red;
    public float destinationSize = 1f;
#endif

    void Start()
    {
    }

    public virtual bool MoveTo(Vector3 position, bool destroyOnReach = false)
    {
        HasReachedDestination = false;
        if (destroyOnReach)
        {
            OnDestinationReached = () => Destroy(gameObject);
        }
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
        if (Agent == null) return;
        if (Agent.path == null) return;

        if (showPath)
        {
            DrawPath(Agent.path);
        }

        if (showDestination)
        {
            DrawTargetPoint(Agent.path);
        }
    }

    private void DrawPath(NavMeshPath path)
    {
        Gizmos.color = pathColor;

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

        float size = destinationSize * 0.5f;
        Gizmos.color = destinationColor;
                                     
        var targetPosition = path.corners[^1];
        Gizmos.DrawLine(targetPosition +  size * Vector3.left, targetPosition +  size * Vector3.right);
        Gizmos.DrawLine(targetPosition +  size * Vector3.up, targetPosition +  size * Vector3.down);
        Gizmos.DrawLine(targetPosition +  size * Vector3.forward, targetPosition +  size * Vector3.back);
    }
#endif
}
