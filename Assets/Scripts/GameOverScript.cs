using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    //we can modify this later to display the number of correct questions
    public void Setup()
    {
        gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void NavigateToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
