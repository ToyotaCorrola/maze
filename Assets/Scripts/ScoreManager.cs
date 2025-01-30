using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton instance for global access

    public TextMeshProUGUI playerScoreText; // Player score UI text
    public TextMeshProUGUI npcScoreText; // NPC score UI text
    public TextMeshProUGUI countdownMessage;
    public GameObject winScreenCanvas; // Reference to Win Screen UI
    public GameObject loseScreenCanvas; // Reference to Lose Screen UI

    private int playerScore = 0; // Player's score
    private int npcScore = 0; // NPC's score
    private int winScore = 7; // Score required to win
    private int loseScore = 7; // Score required to lose

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Add points to the player and check for win condition
    public void AddPlayerScore(int amount)
    {
        playerScore += amount;
        UpdatePlayerScoreUI();

        if (playerScore >= winScore)
        {
            ShowWinScreen();
        }
    }

    // Add points to the NPC and check for lose condition
    public void AddNPCScore(int amount)
    {
        npcScore += amount;
        UpdateNPCScoreUI();

        if (npcScore >= loseScore)
        {
            ShowLoseScreen();
        }
    }

    // Update player score UI
    private void UpdatePlayerScoreUI()
    {
        if (playerScoreText != null)
        {
            playerScoreText.text = "Your Score: " + playerScore;
        }
    }

    // Update NPC score UI
    private void UpdateNPCScoreUI()
    {
        if (npcScoreText != null)
        {
            npcScoreText.text = "NPC Score: " + npcScore;
        }
    }

    // Show the Win Screen when the player reaches 7 points
    private void ShowWinScreen()
    {
        if (winScreenCanvas != null)
        {
            winScreenCanvas.SetActive(true); // Display Win Screen UI
        }
        Time.timeScale = 0f; // Pause the game
    }

    // Show the Lose Screen when the NPC reaches 7 points
    private void ShowLoseScreen()
    {
        if (loseScreenCanvas != null)
        {
            loseScreenCanvas.SetActive(true); // Display Lose Screen UI
        }
        Time.timeScale = 0f; // Pause the game
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
            countdownMessage.gameObject.SetActive(false); // Hide message after countdown
        }
        UpdatePlayerScoreUI(); // Restore score display
    }
}



