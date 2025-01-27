using UnityEngine;

public class NPCAI : MonoBehaviour
{
    public float moveSpeed = 3f; // NPC movement speed
    public float detectionRange = 10f; // Line-of-sight range to detect the player
    public LayerMask obstacleMask; // Layer mask for maze walls
    public LayerMask playerMask; // Layer mask for detecting the player
    private Vector3 randomTarget; // Random movement target
    private Transform playerTransform; // Reference to the player's transform
    private bool isChasingPlayer = false; // Whether the NPC is chasing the player

    void Start()
    {
        // Find the player in the scene
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Choose a random initial position
        SetRandomPosition();
    }

    void Update()
    {
        if (isChasingPlayer)
        {
            ChasePlayer();
        }
        else
        {
            RandomMovement();
            CheckForPlayer();
        }
    }

    void SetRandomPosition()
    {
        Vector3 boundsMin = new Vector3(1, 0.5f, 1); // Replace with actual maze bounds
        Vector3 boundsMax = new Vector3(19, 0.5f, 19); // Replace with actual maze bounds

        Vector3 randomPos = new Vector3(
            Random.Range(boundsMin.x, boundsMax.x),
            0.5f,
            Random.Range(boundsMin.z, boundsMax.z)
        );

        transform.position = randomPos;
    }

    void RandomMovement()
    {
        if (Vector3.Distance(transform.position, randomTarget) < 0.5f)
        {
            randomTarget = new Vector3(
                Random.Range(-1f, 1f),
                0f,
                Random.Range(-1f, 1f)
            ).normalized * 3f + transform.position; // Short random steps
        }

        MoveTowards(randomTarget);
    }

    void MoveTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    void CheckForPlayer()
    {
        Vector3 directionToPlayer = playerTransform.position - transform.position;

        if (directionToPlayer.magnitude < detectionRange)
        {
            Ray ray = new Ray(transform.position, directionToPlayer);
            if (!Physics.Raycast(ray, directionToPlayer.magnitude, obstacleMask))
            {
                isChasingPlayer = true;
            }
        }
    }

    void ChasePlayer()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) > detectionRange)
        {
            isChasingPlayer = false;
            return;
        }

        MoveTowards(playerTransform.position);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision with Player detected!");
            StartCoroutine(StunPlayer());
        }
    }


    System.Collections.IEnumerator StunPlayer()
    {
        Debug.Log("Player stunned!");
        PlayerMovement playerMovement = playerTransform.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            Debug.Log("Disabling PlayerMovement...");
            playerMovement.enabled = false;

            // Display countdown timer
            ScoreManager.Instance.StartCountdown(5);

            yield return new WaitForSeconds(5f);

            Debug.Log("Re-enabling PlayerMovement...");
            playerMovement.enabled = true;
        }
    }
}
