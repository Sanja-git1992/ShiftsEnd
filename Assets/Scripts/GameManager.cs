using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI messageText;
    public TextMeshProUGUI objectiveText;
    public TextMeshProUGUI timerText;

    public GameObject winText;
    public GameObject loseText;
    public GameObject enemy;
    public GameObject menuPanel;

    public bool codeFound = false;
    public bool gameEnded = false;
    public bool gameStarted = false;

    public float timer = 0f;

    void Start()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gameStarted = false;
        gameEnded = false;
        codeFound = false;
        timer = 0f;

        messageText.text = "";
        objectiveText.text = "OBJECTIVE:\nFind the code";
        timerText.text = "TIME: 0.0";

        winText.SetActive(false);
        loseText.SetActive(false);
        enemy.SetActive(false);
        menuPanel.SetActive(true);
    }

    void Update()
{
    if (!gameStarted)
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }

        return;
    }

    if (!gameEnded)
    {
        timer += Time.deltaTime;
        timerText.text = "TIME: " + timer.ToString("F1");
    }

    if (gameEnded && Input.GetKeyDown(KeyCode.R))
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

    public void StartGame()
    {
        gameStarted = true;
        Time.timeScale = 1f;

        menuPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ShowCode()
    {
        if (gameEnded || !gameStarted) return;

        codeFound = true;

        objectiveText.text = "OBJECTIVE:\nReach the exit";

        messageText.text =
            "You found a note!\n\nCode: 413\n\nThe enemy has appeared!";

        enemy.SetActive(true);
    }

    public void Escape()
    {
        if (gameEnded || !gameStarted) return;

        gameEnded = true;

        messageText.gameObject.SetActive(false);
        objectiveText.gameObject.SetActive(false);

        winText.SetActive(true);
    }

    public void DoorLocked()
    {
        if (gameEnded || !gameStarted) return;

        messageText.text = "Door locked!\nFind the code first.";
    }

    public void GameOver()
    {
        if (gameEnded || !gameStarted) return;

        gameEnded = true;

        messageText.gameObject.SetActive(false);
        objectiveText.gameObject.SetActive(false);

        winText.SetActive(false);
        loseText.SetActive(true);
    }

    public void QuitGame()
    {

    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}