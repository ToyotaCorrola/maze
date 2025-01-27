using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void RestartGame()
    {
        Time.timeScale = 1f; // Reset time scale
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current scene
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Reset time scale
        SceneManager.LoadScene("Menu"); // Replace with your menu scene name
    }
}
