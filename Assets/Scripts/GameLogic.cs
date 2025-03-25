using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [Header("NPC")]
    public NpcSpawner npcSpawner;
    public QueueManager queueManager;

    [Space]
    public Transform waitPosition;
    public Transform acceptPosition;

    private NpcBase npcWaiting;

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

    private void HandleOnQueue(NpcBase npc)
    {
        // NO-OP
    }

    private void HandleOnDequeue(NpcBase npc)
    {
        npc.MoveTo(waitPosition.position);
        npcWaiting = npc;
    }

    public void NextCustomer()
    {
        queueManager.Dequeue();
    }

    public void AcceptCustomer()
    {
        ServeWaitingCustomer(acceptPosition.position);
    }

    public void DismissCustomer()
    {
        ServeWaitingCustomer(npcSpawner.RandomDestinationPoint().RandomPosition());
    }

    public void ServeWaitingCustomer(Vector3 position)
    {
        if (npcWaiting == null) return;
        npcWaiting.MoveTo(position, destroyOnReach: true);
        npcWaiting = null;
    }

    public void SpawnPasserbyNpc()
    {
        npcSpawner.SpawnPasserbyNpc();
    }

    public void SpawnQueueNpc()
    {
        if (queueManager.IsQueueFull) return;
        var npc = npcSpawner.SpawnQueueNpc();
        queueManager.Enqueue(npc);
    }
}
