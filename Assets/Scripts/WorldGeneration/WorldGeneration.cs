using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    // Gameplay
    private float chunkSpawnZ;
    private Queue<Chunk> activeChunks = new Queue<Chunk>();
    private List<Chunk> chunkPool = new List<Chunk>();

    // Configurable fields
    [SerializeField] private int firstChunkPosition = 5;
    [SerializeField] private int chunkOnScreen = 10;
    [SerializeField] private float despawnDistance = 5;

    // Prefabs
    [SerializeField] private List<GameObject> chunkPrefab;
    [SerializeField] private Transform cameraTransform;

    private void Awake()
    {
        ResetWorld();
    }

    private void Start()
    {
        if (chunkPrefab.Count.Equals(0))
        {
            Debug.LogError("~ Missing chunk prefabs in world generator.");
            return;
        }

        if (!cameraTransform)
        {
            Debug.LogError("~ No camera reference, trying to reach main camera.");
            cameraTransform = Camera.main.transform;
        }
    }

    private void Update()
    {
        ScanPosition();
    }

    private void ScanPosition()
    {
        float cameraTransformZ = cameraTransform.position.z;
        Chunk lastChunk = activeChunks.Peek();

        if (cameraTransformZ >= lastChunk.transform.position.z + lastChunk.length + despawnDistance)
        {
            SpawnNewChunk();
            DeleteLastChunk();
        }
    }

    private void SpawnNewChunk()
    {
        int randomIndex = Random.Range(0, chunkPrefab.Count);

        Chunk chunk = chunkPool.Find(chunk => !chunk.gameObject.activeSelf && chunk.name.StartsWith(chunkPrefab[randomIndex].name));

        if (!chunk)
        {
            GameObject go = Instantiate(chunkPrefab[randomIndex], transform);
            chunk = go.GetComponent<Chunk>();
        }

        chunk.transform.position = new Vector3(0, 0, chunkSpawnZ);
        chunkSpawnZ += chunk.length;

        activeChunks.Enqueue(chunk);
        chunk.Show();
    }

    private void DeleteLastChunk()
    {
        Chunk chunk = activeChunks.Dequeue();
        chunk.Hide();
        chunkPool.Add(chunk);
    }

    public void ResetWorld()
    {
        chunkSpawnZ = firstChunkPosition;

        for (int i = activeChunks.Count; i >= 1; i--)
        {
            DeleteLastChunk();
        }

        for (int i = 0; i < chunkOnScreen; i++)
        {
            SpawnNewChunk();
        }
    }

}
