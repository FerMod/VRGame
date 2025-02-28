using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class NpcSpawner : MonoBehaviour
{
    public GameObject[] npcPrefabs;
    public SpawnPoint[] spawnPoints;

    [Header("Debug")]
    public bool showGizmos = false;
    public float spawnInterval = 5f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void Spawn(bool destroyOnReach = true, Vector3? targetPosition = null)
    {
        if (npcPrefabs.Length == 0) return;
        if (spawnPoints.Length == 0) return;

        var npcPrefab = npcPrefabs[Random.Range(0, npcPrefabs.Length)];
        var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        var gameObject = Instantiate(npcPrefab, spawnPoint.RandomPosition(), Quaternion.identity);
        if (gameObject.TryGetComponent(out NpcBase npc))
        {
            var destinationPoint = targetPosition ?? RandomDestinationPoint(spawnPoint).RandomPosition();
            npc.MoveTo(destinationPoint);
            if (destroyOnReach)
            {
                npc.OnDestinationReached += () => Destroy(gameObject);
            }
        }
    }

    private SpawnPoint RandomDestinationPoint(SpawnPoint excludePoint)
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


