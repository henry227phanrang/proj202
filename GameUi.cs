using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUi : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    // Hàm này gán cho Button Play
    public void StartGameBtn()
    {
        Debug.Log("StartGameBtn clicked!");
        StartGame();
    }

    public void StartGame()
    {
        gameManager.StartGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
        gameManager.ResumeGame();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
