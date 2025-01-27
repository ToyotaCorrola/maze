using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    private Item nearbyItem; // The item the player is near
    public TextMeshProUGUI interactionMessage; // UI text for displaying messages
    public GameObject itemPrefab; // Reference to the item prefab
    public Vector3 mazeBoundsMin; // Minimum bounds for random item spawning
    public Vector3 mazeBoundsMax; // Maximum bounds for random item spawning

    void Update()
    {
        // Inspect the item when pressing 'I'
        if (Input.GetKeyDown(KeyCode.I) && nearbyItem != null)
        {
            string result = nearbyItem.Inspect();
            DisplayMessage($"Inspection: {result}");
        }

        // Collect the item when pressing 'C'
        if (Input.GetKeyDown(KeyCode.C) && nearbyItem != null)
        {
            string result = nearbyItem.Collect();

            // Update scores based on the item's smell
            if (result == "Good")
            {
                ScoreManager.Instance.AddPlayerScore(1); // Increase player score
                DisplayMessage("You collected a good item! Player score +1.");
            }
            else if (result == "Rotten")
            {
                ScoreManager.Instance.AddNPCScore(1); // Increase NPC score
                DisplayMessage("You collected a rotten item! NPC score +1.");
            }
            else if (result == "Questionable")
            {
                string actualState = nearbyItem.ResolveQuestionable();
                if (actualState == "Good")
                {
                    ScoreManager.Instance.AddPlayerScore(1); // Increase player score
                    DisplayMessage("Questionable item turned out to be good! Player score +1.");
                }
                else
                {
                    ScoreManager.Instance.AddNPCScore(1); // Increase NPC score
                    DisplayMessage("Questionable item turned out to be rotten! NPC score +1.");
                }
            }

            // Destroy the collected item and spawn a new one
            Destroy(nearbyItem.gameObject);
            SpawnRandomItem();

            // Clear the nearby item reference
            nearbyItem = null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            nearbyItem = other.GetComponent<Item>();
            if (nearbyItem != null)
            {
                DisplayMessage("You are near an item. Press 'I' to inspect, 'C' to collect.");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item") && other.GetComponent<Item>() == nearbyItem)
        {
            nearbyItem = null;
            DisplayMessage(""); // Clear the message
        }
    }

    void DisplayMessage(string message)
    {
        if (interactionMessage != null)
        {
            interactionMessage.text = message;
            interactionMessage.gameObject.SetActive(true);

            CancelInvoke("HideMessage");
            Invoke("HideMessage", 3f);
        }
    }

    void HideMessage()
    {
        if (interactionMessage != null)
        {
            interactionMessage.gameObject.SetActive(false);
        }
    }

    void SpawnRandomItem()
    {
        Debug.Log("Spawning a new item...");

        // Generate a random position within the maze bounds
        float x = Random.Range(mazeBoundsMin.x, mazeBoundsMax.x);
        float z = Random.Range(mazeBoundsMin.z, mazeBoundsMax.z);
        Vector3 randomPosition = new Vector3(x, 0.5f, z);

        // Instantiate the item prefab at the random position
        GameObject newItem = Instantiate(itemPrefab, randomPosition, Quaternion.identity);

        // Assign a random smell state
        Item itemComponent = newItem.GetComponent<Item>();
        if (itemComponent != null)
        {
            itemComponent.AssignRandomSmell();
            Debug.Log($"New item spawned at {randomPosition} with smell: {itemComponent.smellState}");
        }
        else
        {
            Debug.LogError($"Spawned item is missing the Item script! Prefab used: {itemPrefab.name}");
        }
    }
}
