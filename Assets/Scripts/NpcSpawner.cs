using System.Collections;
using System.ComponentModel;
using UnityEngine;
using Random = UnityEngine.Random;

public class NpcSpawner : MonoBehaviour
{
    public float spawnInterval = 5f;

    [Description("Passerby NPCs that will spawn and walk from one point to another.")]
    public GameObject[] passerbyNpcPrefabs;

    [Description("Queue NPCs that will go to the waiting line to be served.")]
    public GameObject[] queueNpcPrefabs;

    public SpawnPoint[] spawnPoints;

    private struct NpcSpawnData
    {
        public NpcBase npc;
        public SpawnPoint spawnPoint;
    }

#if UNITY_EDITOR
    [Header("Debug")]
    public bool showGizmos = false;
#endif

    void Start()
    {
        if (spawnInterval != 0)
        {
            StartCoroutine(SpawnRoutine());
        }
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnPasserbyNpc();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public NpcBase SpawnPasserbyNpc()
    {
        var spawnData = SpawnNpc(passerbyNpcPrefabs);

        if (spawnData.npc != null)
        {
            var destinationPoint = RandomDestinationPoint(spawnData.spawnPoint).RandomPosition();
            spawnData.npc.MoveTo(destinationPoint, destroyOnReach: true);
        }

        return spawnData.npc;

    }

    public NpcBase SpawnQueueNpc()
    {
        var spawnData = SpawnNpc(queueNpcPrefabs, false);
        return spawnData.npc;
    }

    private NpcSpawnData SpawnNpc(GameObject[] npcPrefabs, bool destroyOnReach = true)
    {
        if (npcPrefabs.Length == 0) return default;
        if (spawnPoints.Length == 0) return default;

        var npcPrefab = npcPrefabs[Random.Range(0, npcPrefabs.Length)];
        var spawnPoint = RandomDestinationPoint();

        var gameObject = Instantiate(npcPrefab, spawnPoint.RandomPosition(), Quaternion.identity);
        var npc = gameObject.GetComponent<NpcBase>();

        return new NpcSpawnData
        {
            npc = npc,
            spawnPoint = spawnPoint,
        };
    }

    public SpawnPoint RandomDestinationPoint(SpawnPoint excludePoint = null)
    {
        SpawnPoint destinationPoint;
        do
        {
            destinationPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        } while (destinationPoint == excludePoint);

        return destinationPoint;
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (!showGizmos) return;
        DrawSpawnPointsGizmos();
    }

    private void DrawSpawnPointsGizmos()
    {
        if (spawnPoints == null) return;

        foreach (var spawn in spawnPoints)
        {
            spawn.DrawGizmos(Color.green);
        }
    }
#endif
}


