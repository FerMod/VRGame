using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class NPCBase : MonoBehaviour
{
    protected NavMeshAgent agent;
    private bool hasReachedDestination;

    public event Action OnDestinationReached;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public virtual void MoveTo(Vector3 position)
    {
        agent.SetDestination(position);
        hasReachedDestination = false;
    }

    protected void Update()
    {
        HandleDestinationReached();
    }

    private void HandleDestinationReached()
    {
        if (hasReachedDestination) return;
        if (agent.remainingDistance > agent.stoppingDistance) return;
        if (agent.pathPending) return;

        hasReachedDestination = true;
        OnDestinationReached?.Invoke();
    }
}

public class NPC : NPCBase
{
    /*
     public QueueManager queueManager;
     public GameLogic gameLogic;

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

     private void HandleOnQueue(NPCBase npc)
     {
         if (this == npc) return;
     }

     private void HandleOnDequeue(NPCBase npc)
     {
         if (this == npc) return;
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
    */

}

enum QueueState
{
    Queued,
    Waiting,
    Dismissed,
    Accepted
}
