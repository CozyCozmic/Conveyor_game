using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject instructionsPanel;


    void Start()
    {
        // Show the mouse while the menu is open
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Pause the game while the menu is showing
        Time.timeScale = 0f;
    }

    public void PlayGame()
    {
        // Hide the menu
        mainMenuPanel.SetActive(false);

        // Resume the game
        Time.timeScale = 1f;

        // Lock the mouse for the first-person controller
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log("Play button clicked!");
    }

    public void QuitGame()
    {
        Debug.Log("Quit button clicked!");
        Application.Quit();
    }
     public void OpenInstructions()
    {
        Debug.Log("Instructions button pressed!");
        mainMenuPanel.SetActive(false);
        instructionsPanel.SetActive(true);
    }

    public void CloseInstructions()
    {
        Debug.Log("Back button pressed!");
        instructionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
