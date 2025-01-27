using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Speed of player movement
    [SerializeField] private float rotationSpeed = 10f; // Speed of smooth rotation

    void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        // Handle movement input
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveDirection += Vector3.forward; // Move forward
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection += Vector3.right; // Move right
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveDirection += Vector3.back; // Move backward
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection += Vector3.left; // Move left
        }

        // Normalize moveDirection to ensure consistent speed diagonally
        if (moveDirection != Vector3.zero)
        {
            moveDirection.Normalize();

            // Move the player
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

            // Calculate target rotation based on movement direction
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            // Smoothly rotate towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}

