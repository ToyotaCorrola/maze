using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject itemPrefab;    // Item prefab to spawn
    public int itemCount = 1;       // Number of items to spawn
    public Vector3 mazeBoundsMin;   // Minimum bounds of the maze
    public Vector3 mazeBoundsMax;   // Maximum bounds of the maze
    public float itemCheckRadius = 0.5f; // Radius to check for collisions when spawning

    void Start()
    {
        SpawnNPC();
    }

    void SpawnNPC()
    {
        int spawned = 0;

        while (spawned < itemCount)
        {
            // Generate a random position within the maze bounds
            float x = Random.Range(mazeBoundsMin.x, mazeBoundsMax.x);
            float z = Random.Range(mazeBoundsMin.z, mazeBoundsMax.z);
            Vector3 randomPosition = new Vector3(x, 0.5f, z);

            // Check if the position is valid (no collisions with walls)
            if (IsValidPosition(randomPosition))
            {
                Instantiate(itemPrefab, randomPosition, Quaternion.identity);
                spawned++;
            }
        }
    }

    bool IsValidPosition(Vector3 position)
    {
        // Check for collisions with walls using a Physics OverlapSphere
        Collider[] colliders = Physics.OverlapSphere(position, itemCheckRadius, LayerMask.GetMask("MazeWall"));

        // Return true if no collisions are detected
        return colliders.Length == 0;
    }
}

