using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameLogic : MonoBehaviour
{
    [Header("NPC")]
    public NpcSpawner npcSpawner;
    public QueueManager queueManager;

    [Space]
    public Transform waitPosition;
    public Transform acceptPosition;

    private NpcBase npcWaiting;

    public UnityEvent<NpcBase> OnNpcWaiting;
    public UnityEvent<NpcBase> OnServeNpc;

    [Header("Spawn Settings")]
    public float spawnInterval = 30f; // Interval in minutes
    public float spawnChance = 0.2f; // Chance to spawn

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

    void Start()
    {
        StartCoroutine(QueueNpcSpawnRoutine());
    }

    private IEnumerator QueueNpcSpawnRoutine()
    {
        while (true)
        {
            var forceSpawn = queueManager.IsQueueEmpty && npcWaiting == null;
            if (forceSpawn || Random.value < spawnChance)
            {
                SpawnQueueNpc();
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void HandleOnQueue(NpcBase npc)
    {
        NextCustomer();
    }

    private void HandleOnDequeue(NpcBase npc)
    {
        npc.MoveTo(waitPosition.position);
        npcWaiting = npc;
        npcWaiting.OnDestinationReached += () => OnNpcWaiting?.Invoke(npcWaiting);
    }

    public void NextCustomer()
    {
        if (npcWaiting != null) return;
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

        OnServeNpc?.Invoke(npcWaiting);
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
