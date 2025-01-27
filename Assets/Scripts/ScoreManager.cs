using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton instance for global access

    public TextMeshProUGUI playerScoreText; // Reference to the Player's score UI text
    public TextMeshProUGUI npcScoreText; // Reference to the NPC's score UI text
    public TextMeshProUGUI countdownMessage;

    private int playerScore = 0; // Player's score
    private int npcScore = 0; // NPC's score

    void Awake()
    {
        // Ensure there's only one instance of the ScoreManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to increase the player's score
    public void AddPlayerScore(int amount)
    {
        playerScore += amount;
        UpdatePlayerScoreUI();
    }

    // Method to increase the NPC's score
    public void AddNPCScore(int amount)
    {
        npcScore += amount;
        UpdateNPCScoreUI();
    }

    // Update the player's score UI
    private void UpdatePlayerScoreUI()
    {
        if (playerScoreText != null)
        {
            playerScoreText.text = "Your Score: " + playerScore;
        }
    }

    // Update the NPC's score UI
    private void UpdateNPCScoreUI()
    {
        if (npcScoreText != null)
        {
            npcScoreText.text = "NPC Score: " + npcScore;
        }
    }
    public void StartCountdown(int duration)
    {
        StartCoroutine(CountdownRoutine(duration));
    }

    private IEnumerator CountdownRoutine(int duration)
    {
        for (int i = duration; i > 0; i--)
        {
            if (countdownMessage != null)
            {
                countdownMessage.text = $"Stunned! Time left: {i}s";
                countdownMessage.gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(1f);
        }

        if (countdownMessage != null)
        {
            countdownMessage.gameObject.SetActive(false); // Hide the message after the countdown
        }
        UpdatePlayerScoreUI(); // Restore the original score text
    }


}


