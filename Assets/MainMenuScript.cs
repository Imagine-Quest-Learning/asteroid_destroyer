using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject howToScreen;
    public void QuitGame() 
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OpenHowTo()
    {
        howToScreen.SetActive(true);
    }

    public void CloseHowTo()
    {
        howToScreen.SetActive(false);
    }
}
