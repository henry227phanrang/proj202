using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;

public class GameManager : MonoBehaviour
{
    private int currentEnergy;
    [SerializeField] private int energyThreshold = 3;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject enemySpaner;
    private bool bossCalled = false;
    [SerializeField] private Image energyBar;
    [SerializeField] GameObject gameUi;

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject winMenu;

    [SerializeField] private GameObject red;

    [SerializeField] private AudioManager audioManager;
    [SerializeField] private CinemachineCamera cam;


    private void SetOrthographicSize(float size)
    {
        Camera cameraComponent = cam.GetComponent<Camera>();
        if (cameraComponent != null && cameraComponent.orthographic)
        {
            cameraComponent.orthographicSize = size;
        }
    }
    void Start()
    {
        currentEnergy = 0;
        UpdateEnergyBar();
        boss.SetActive(false);
        red.SetActive(false);
        MainMenu();
        audioManager.StopAudioGame();
        audioManager.PlayMenuAudio();
        SetOrthographicSize(5f);


    }

    public void AddEnergy()
    {
        if (bossCalled)
        {
            return;
        }

        currentEnergy += 1;
        UpdateEnergyBar();
        if (currentEnergy == energyThreshold)
        {
            CallBoss();
        }
    }

    private void CallBoss()
    {
        bossCalled = true;
        boss.SetActive(true);
        enemySpaner.SetActive(false);
        gameUi.SetActive(false);
        audioManager.PlayBossAudio();
        SetOrthographicSize(10f);
        red.SetActive(true);

    }

    private void UpdateEnergyBar()
    {
        if (energyBar != null)
        {
            float fillAmount = Mathf.Clamp01((float)currentEnergy / (float)energyThreshold);
            energyBar.fillAmount = fillAmount;
        }
    }

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    public void GameOverMenu()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(true);
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        Time.timeScale = 0f;
        audioManager.PlayGameOverAudio();
    }

    public void PauseGameMenu()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(true);
        winMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        Debug.Log("YES, StartGame is working!");
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        Time.timeScale = 1f;
        audioManager.PlayDefaultAudio();
    }

    public void ResumeGame()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void WinGame()
    {
        winMenu.SetActive(true);
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        Time.timeScale = 0f;
        audioManager.PlayWinAudio();

    }
    public void PlayerDie()
    {
        audioManager.PlayDieAudio();
        StartCoroutine(DelayGameOver());
    }

private IEnumerator DelayGameOver()
{
    yield return new WaitForSeconds(audioManager.GetDieClipLength());
    GameOverMenu();
}


}
