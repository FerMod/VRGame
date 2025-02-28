using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [Header("NPC Queue")]
    public QueueManager queueManager;

    [Space]
    public Transform waitPosition;
    public Transform acceptPosition;
    public Transform[] dismissPositions;

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
        ServeWaitingCustomer(acceptPosition);
    }

    public void DismissCustomer()
    {
        ServeWaitingCustomer(dismissPositions[Random.Range(0, dismissPositions.Length)]);
    }

    public void ServeWaitingCustomer(Transform transform, float time = 0f)
    {
        if (npcWaiting == null) return;
        MoveAndDestroy(npcWaiting, transform.position);
        npcWaiting = null;
    }

    private void MoveAndDestroy(NpcBase npc, Vector3 position, float time = 0f)
    {
        npc.OnDestinationReached += () =>
        {
            Destroy(npc.gameObject, time);
        };
        npc.MoveTo(position);
    }
}
