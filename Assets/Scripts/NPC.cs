using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class NPCBase : MonoBehaviour
{
    protected NavMeshAgent agent;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public abstract void MoveTo(Vector3 position);
}

public class NPC : NPCBase
{
    public QueueManager queueManager;

    private void OnEnable()
    {
        queueManager.OnQueue += HandleOnQueue;
        queueManager.OnDequeue += HandleOnDequeue;
    }

    private void OnDisable()
    {
        queueManager.OnQueue -= HandleOnQueue;
        queueManager.OnDequeue -= HandleOnDequeue;
    }

    public override void MoveTo(Vector3 position)
    {
        agent.SetDestination(position);
    }

    private void HandleOnQueue(NavMeshAgent agent)
    {
        if (this.agent != agent) return;
    }

    private void HandleOnDequeue(NavMeshAgent agent)
    {
        if (this.agent != agent) return;
        LeaveQueue();
    }

    //TODO: Define different exit behaviors
    public void LeaveQueue()
    {
        // Example: Move NPC away after serving
        Vector3 exitPoint = transform.position + new Vector3(5, 0, 0); // Move right
        MoveTo(exitPoint);
        Destroy(gameObject, 3f); // Optional: Remove NPC after exiting
    }
}
