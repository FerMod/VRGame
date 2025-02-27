using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class QueueManager : MonoBehaviour
{
    public Transform[] queuePositions;
    private readonly Queue<NPCBase> npcQueue = new();

    /// <summary>
    /// Event handler for when an npc is added to the queue.
    /// </summary>
    /// <param name="npc">The npc that was added to the queue.</param>
    public delegate void QueueChangedHandler(NPCBase npc);

    public event QueueChangedHandler OnQueue;
    public event QueueChangedHandler OnDequeue;

    public void Enqueue(NPCBase npc)
    {
        if (npcQueue.Contains(npc)) return;

        npcQueue.Enqueue(npc);
        UpdateQueuePositions();

        OnQueue?.Invoke(npc);
    }

    public void Dequeue()
    {
        if (npcQueue.Count <= 0) return;

        var agent = npcQueue.Dequeue();
        UpdateQueuePositions();

        OnDequeue?.Invoke(agent);
    }

    private void UpdateQueuePositions()
    {
        int index = 0;
        foreach (var npc in npcQueue)
        {
            if (index < queuePositions.Length)
            {
                npc.MoveTo(queuePositions[index].position);
                index++;
            }
        }
    }
}
