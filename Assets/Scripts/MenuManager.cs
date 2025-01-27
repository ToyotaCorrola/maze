using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject musicManager; // Reference to the BackgroundMusic GameObject

    public void PlayGame()
    {
        // Start the background music
        if (musicManager != null)
        {
            MusicManager manager = musicManager.GetComponent<MusicManager>();
            if (manager != null)
            {
                manager.PlayMusic();
            }
        }

        // Load the game scene
        SceneManager.LoadScene("Game"); // Replace with your game scene name
    }

    public void OpenRules()
    {
        SceneManager.LoadScene("Rules"); // Replace with your rules scene name
    }

}
