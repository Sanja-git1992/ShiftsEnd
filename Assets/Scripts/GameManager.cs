using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // =====================================================
    // GLAVNI UI
    // =====================================================

    public TextMeshProUGUI messageText;
    public TextMeshProUGUI objectiveText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI winText;

    public TextMeshProUGUI livesText;
    public GameObject loseText;

    // Stari početni meni - ostavljamo ga zbog postojećeg projekta
    public GameObject menuPanel;

    // Novi dizajnirani uvodni ekran
    public GameObject introPanel;

    // =====================================================
    // NOVI STORY / OBJECTIVE PANEL
    // =====================================================

    public GameObject storyPanel;

    public TextMeshProUGUI storyTitle;
    public TextMeshProUGUI storyObjective;
    public TextMeshProUGUI storyMessage;

    // =====================================================
    // GAMEPLAY OBJEKTI
    // =====================================================

    public GameObject enemy;

    public Transform playerStartPosition;
    public GameObject player;

    public GameObject key;
    public GameObject exitBlocker;

    // =====================================================
    // STANJE IGRE
    // =====================================================

    public int lives = 3;

    public bool codeFound = false;
    public bool gameEnded = false;
    public bool gameStarted = false;

    public int cluesFound = 0;

    public float timer = 0f;


    // =====================================================
    // START
    // =====================================================

    void Start()
    {
        // Igra je na početku pauzirana dok je IntroPanel otvoren
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gameStarted = false;
        gameEnded = false;

        codeFound = false;
        cluesFound = 0;

        timer = 0f;
        lives = 3;

        // Resetiramo puzzle varijable
        PuzzleKeypad.puzzleSolved = false;
        KeycardInteraction.hasKeycard = false;
        TerminalUse.terminalActivated = false;


        // -------------------------
        // GLAVNI UI
        // -------------------------

        if (messageText != null)
        {
            messageText.text = "";
            messageText.gameObject.SetActive(false);
        }

        if (objectiveText != null)
        {
            objectiveText.text = "";
            objectiveText.gameObject.SetActive(false);
        }

        if (timerText != null)
        {
            timerText.text = "TIME: 0.0";
            timerText.gameObject.SetActive(false);
        }

        if (livesText != null)
        {
            livesText.text = "LIVES: " + lives;
            livesText.gameObject.SetActive(false);
        }


        // -------------------------
        // WIN / LOSE
        // -------------------------

        if (winText != null)
        {
            winText.gameObject.SetActive(false);
        }

        if (loseText != null)
        {
            loseText.SetActive(false);
        }


        // -------------------------
        // ENEMY
        // -------------------------

        if (enemy != null)
        {
            enemy.SetActive(false);
        }


        // -------------------------
        // STARI MENU
        // -------------------------

        if (menuPanel != null)
        {
            menuPanel.SetActive(false);
        }


        // -------------------------
        // NOVI INTRO
        // -------------------------

        if (introPanel != null)
        {
            introPanel.SetActive(true);
        }


        // -------------------------
        // STORY PANEL
        // Na početku mora biti skriven.
        // Pojavit će se tek nakon ENTER-a.
        // -------------------------

        if (storyPanel != null)
        {
            storyPanel.SetActive(false);
        }


        // -------------------------
        // KEY
        // -------------------------

        if (key != null)
        {
            key.SetActive(true);
        }


        // -------------------------
        // EXIT BLOCKER
        // -------------------------

        if (exitBlocker != null)
        {
            exitBlocker.SetActive(true);
        }
    }


    // =====================================================
    // UPDATE
    // =====================================================

    void Update()
    {
        // Dok igra još nije počela
        if (!gameStarted)
        {
            // ENTER pokreće igru
            if (Input.GetKeyDown(KeyCode.Return))
            {
                StartGame();
            }

            // ESC izlazi iz igre
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                QuitGame();
            }

            return;
        }


        // Timer radi samo dok igra traje
        if (!gameEnded)
        {
            timer += Time.deltaTime;

            if (timerText != null)
            {
                timerText.text =
                    "TIME: " + timer.ToString("F1");
            }
        }


        // Restart nakon pobjede ili Game Overa
        if (gameEnded && Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;

            SceneManager.LoadScene(
                SceneManager.GetActiveScene().name
            );
        }
    }


    // =====================================================
    // START GAME
    // =====================================================

    public void StartGame()
    {
        gameStarted = true;

        Time.timeScale = 1f;


        // Sakrij uvodni ekran
        if (introPanel != null)
        {
            introPanel.SetActive(false);
        }


        // Sakrij stari meni
        if (menuPanel != null)
        {
            menuPanel.SetActive(false);
        }


        // Uključi gameplay UI
        if (timerText != null)
        {
            timerText.gameObject.SetActive(true);
        }

        if (livesText != null)
        {
            livesText.gameObject.SetActive(true);
        }


        // Zaključaj miš za FPS kontrolu
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        // Prva poruka priče
        ShowStoryMessage(
            "SECURITY SYSTEM OFFLINE",
            "OBJECTIVE 01",
            "Emergency access requires the security code.\nSearch the office for the security code.",
            5f
        );
    }


    // =====================================================
    // STORY PANEL
    // =====================================================

    public void ShowStoryMessage(
    string title,
    string objective,
    string message,
    float duration = 5f)
{
    if (storyPanel == null)
    {
        return;
    }

    if (storyTitle != null)
    {
        storyTitle.text = title;
    }

    if (storyObjective != null)
    {
        storyObjective.text = objective;
    }

    if (storyMessage != null)
    {
        storyMessage.text = message;
    }

    storyPanel.SetActive(true);

    StartCoroutine(HideStoryPanelAfterTime(duration));
}

private IEnumerator HideStoryPanelAfterTime(float duration)
{
    yield return new WaitForSeconds(duration);

    storyPanel.SetActive(false);
}


    // =====================================================
    // PRONALAZAK PRVOG KODA 413
    // =====================================================

    public void ShowCode()
    {
        if (gameEnded || !gameStarted)
        {
            return;
        }


        codeFound = true;


        // Stari MessageText više nam ne treba za ovu poruku
        if (messageText != null)
        {
            messageText.text =
                "You found a note!\n\nCode: 413\n\nThe enemy has appeared!";
        }


        // Aktiviraj neprijatelja
        if (enemy != null)
        {
            enemy.SetActive(true);
        }


        // Novi StoryPanel
        ShowStoryMessage(
            "SECURITY BREACH",
            "OBJECTIVE 02",
            "Code 413 accepted.\nSomething is moving in the building.\nFind the puzzle clues.",
            5f
        );
    }
    // =====================================================
    // PRONALAZAK PUZZLE TRAGOVA
    // =====================================================

public void ClueFound()
{
    if (gameEnded || !gameStarted)
    {
        return;
    }

    cluesFound++;

    // Ne dopuštamo da broj prijeđe 3
    if (cluesFound > 3)
    {
        cluesFound = 3;
    }

    // Kada igrač pronađe sva 3 traga
    if (cluesFound >= 3)
    {
        ShowStoryMessage(
            "CLUES COMPLETE",
            "OBJECTIVE 03",
            "You found all three clues.\nUse them to discover the 3-digit code.",
            5f
        );
    }
}


    // =====================================================
    // IGRAČ JE DOŠAO DO ZAKLJUČANIH VRATA
    // =====================================================

    public void DoorLocked()
    {
        if (gameEnded || !gameStarted)
        {
            return;
        }


        ShowStoryMessage(
            "ACCESS DENIED",
            "DOOR LOCKED",
            "The exit is still locked.\nComplete the security procedure first.",
            4f
        );
    }


    // =====================================================
    // IGRAČ GUBI ŽIVOT
    // =====================================================

    public void LoseLife()
    {
        if (gameEnded || !gameStarted)
        {
            return;
        }


        lives--;


        if (livesText != null)
        {
            livesText.text =
                "LIVES: " + lives;
        }


        // Ako nema više života -> Game Over
        if (lives <= 0)
        {
            GameOver();
            return;
        }


        // Vrati igrača na PlayerStart
        if (player != null &&
            playerStartPosition != null)
        {
            player.transform.position =
                playerStartPosition.position;
        }


        // Reset puzzlea 729
        PuzzleKeypad.puzzleSolved = false;


        // Reset ključa
        KeycardInteraction.hasKeycard = false;

        if (key != null)
        {
            key.SetActive(true);
        }


        // Reset terminala
        TerminalUse.terminalActivated = false;


        // Ponovno zaključaj izlaz
        if (exitBlocker != null)
        {
            exitBlocker.SetActive(true);
        }


        // Obavijest igraču
        ShowStoryMessage(
            "YOU WERE CAUGHT",
            "SECURITY RESET",
            "Lives left: " + lives +
            "\nPuzzle progress has been reset.",
            4f
        );
    }


    // =====================================================
    // TERMINAL JE AKTIVIRAN
    // =====================================================

    public void TerminalActivated()
    {
        if (gameEnded || !gameStarted)
        {
            return;
        }


        ShowStoryMessage(
            "ACCESS GRANTED",
            "FINAL OBJECTIVE",
            "The emergency exit is unlocked.\nReach the exit.",
            5f
        );
    }


    // =====================================================
    // POBJEDA
    // =====================================================

    public void Escape()
    {
        if (gameEnded || !gameStarted)
        {
            return;
        }


        gameEnded = true;

        Time.timeScale = 0f;


        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


        // Sakrij StoryPanel
        if (storyPanel != null)
        {
            storyPanel.SetActive(false);
        }


        if (messageText != null)
        {
            messageText.gameObject.SetActive(false);
        }

        if (objectiveText != null)
        {
            objectiveText.gameObject.SetActive(false);
        }


        // Prikaži pobjedu
        if (winText != null)
        {
            winText.text =
                "YOU ESCAPED!\n\n" +
                "Time: " +
                timer.ToString("F1") +
                " s\n" +
                "Lives left: " +
                lives +
                "\n\nPress R to restart";

            winText.gameObject.SetActive(true);
        }
    }


    // =====================================================
    // GAME OVER
    // =====================================================

    public void GameOver()
    {
        if (gameEnded || !gameStarted)
        {
            return;
        }


        gameEnded = true;

        Time.timeScale = 0f;


        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


        // Sakrij StoryPanel
        if (storyPanel != null)
        {
            storyPanel.SetActive(false);
        }


        if (messageText != null)
        {
            messageText.gameObject.SetActive(false);
        }

        if (objectiveText != null)
        {
            objectiveText.gameObject.SetActive(false);
        }


        if (winText != null)
        {
            winText.gameObject.SetActive(false);
        }


        if (loseText != null)
        {
            loseText.SetActive(true);
        }
    }


    // =====================================================
    // QUIT
    // =====================================================

    public void QuitGame()
    {
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;

#else

        Application.Quit();

#endif
    }
}