using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class QueueManager : MonoBehaviour
{
    public Transform[] queuePositions;
    private readonly Queue<NavMeshAgent> agentQueue = new();

    /// <summary>
    /// Event handler for when an item is added to the queue.
    /// </summary>
    /// <param name="agent">The agent that was added to the queue.</param>
    public delegate void QueueChangedHandler(NavMeshAgent agent);

    public event QueueChangedHandler OnQueue;
    public event QueueChangedHandler OnDequeue;

    public void Enqueue(NavMeshAgent agent)
    {
        if (agentQueue.Contains(agent)) return;

        agentQueue.Enqueue(agent);
        UpdateQueuePositions();

        OnQueue?.Invoke(agent);
    }

    public void Dequeue()
    {
        if (agentQueue.Count <= 0) return;

        var agent = agentQueue.Dequeue();
        UpdateQueuePositions();

        OnDequeue?.Invoke(agent);
    }

    private void UpdateQueuePositions()
    {
        int index = 0;
        foreach (var agent in agentQueue)
        {
            if (index < queuePositions.Length)
            {
                agent.SetDestination(queuePositions[index].position);
                index++;
            }
        }
    }
}
