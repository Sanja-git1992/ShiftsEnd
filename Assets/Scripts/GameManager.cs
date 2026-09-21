using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI objectiveText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI winText;

    public GameObject loseText;
    public GameObject enemy;
    public GameObject menuPanel;

    public TextMeshProUGUI livesText;
    public int lives = 3;

    public Transform playerStartPosition;
    public GameObject player;

    // Objekti potrebni za reset nakon gubitka zivota
    public GameObject key;
    public GameObject exitBlocker;

    public bool codeFound = false;
    public bool gameEnded = false;
    public bool gameStarted = false;

    // Broj pronadenih puzzle tragova
    public int cluesFound = 0;

    public float timer = 0f;

    void Start()
    {
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gameStarted = false;
        gameEnded = false;
        codeFound = false;
        cluesFound = 0;
        timer = 0f;

        // Reset statickih vrijednosti
        PuzzleKeypad.puzzleSolved = false;
        KeycardInteraction.hasKeycard = false;
        TerminalUse.terminalActivated = false;

        messageText.text = "";

        objectiveText.text =
            "OBJECTIVE:\nFind code 413";

        timerText.text = "TIME: 0.0";

        if (winText != null)
        {
            winText.gameObject.SetActive(false);
        }

        if (loseText != null)
        {
            loseText.SetActive(false);
        }

        // Neprijatelj je ugasen dok se ne pronade 413
        if (enemy != null)
        {
            enemy.SetActive(false);
        }

        if (menuPanel != null)
        {
            menuPanel.SetActive(true);
        }

        lives = 3;

        if (livesText != null)
        {
            livesText.text = "LIVES: " + lives;
        }

        // Kljuc je vidljiv, ali ga nije moguce uzeti
        // dok se ne rijesi kod 729
        if (key != null)
        {
            key.SetActive(true);
        }

        // Izlaz je zakljucan
        if (exitBlocker != null)
        {
            exitBlocker.SetActive(true);
        }
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

            timerText.text =
                "TIME: " + timer.ToString("F1");
        }

        if (gameEnded && Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;

            SceneManager.LoadScene(
                SceneManager.GetActiveScene().name
            );
        }
    }

    public void StartGame()
    {
        gameStarted = true;

        Time.timeScale = 1f;

        if (menuPanel != null)
        {
            menuPanel.SetActive(false);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // --------------------------------------------------
    // POCETNI KOD 413
    // --------------------------------------------------

    public void ShowCode()
    {
        if (gameEnded || !gameStarted)
        {
            return;
        }

        codeFound = true;

        // Nakon 413 igrac treba traziti tri puzzle traga
        objectiveText.text =
            "OBJECTIVE:\nFind the puzzle clues\nCLUES FOUND: 0/3";

        messageText.text =
            "You found the first code!\n\n" +
            "Code: 413\n\n" +
            "The enemy has appeared!";

        // Pronalaženje 413 aktivira neprijatelja
        if (enemy != null)
        {
            enemy.SetActive(true);
        }
    }

    // --------------------------------------------------
    // PUZZLE TRAGOVI 7 - 2 - 9
    // --------------------------------------------------

    public void ClueFound()
    {
        if (gameEnded || !gameStarted)
        {
            return;
        }

        // Tragovi se broje tek nakon pronalaska 413
        if (!codeFound)
        {
            return;
        }

        // Maksimalno tri traga
        if (cluesFound < 3)
        {
            cluesFound++;
        }

        // Ako jos nisu pronadena sva tri
        if (cluesFound < 3)
        {
            objectiveText.text =
                "OBJECTIVE:\nFind the puzzle clues\n" +
                "CLUES FOUND: " + cluesFound + "/3";
        }
        else
        {
            // Sva tri traga su pronadena
            objectiveText.text =
                "OBJECTIVE:\nEnter the 3-digit code at the keypad\n" +
                "CLUES FOUND: 3/3";

            messageText.text +=
                "\n\nAll clues found!";
        }
    }

    // --------------------------------------------------
    // POBJEDA
    // --------------------------------------------------

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

        messageText.gameObject.SetActive(false);
        objectiveText.gameObject.SetActive(false);

        if (winText != null)
        {
            winText.text =
                "YOU ESCAPED!\n\n" +
                "Time: " + timer.ToString("F1") + " s\n" +
                "Lives left: " + lives + "\n\n" +
                "Press R to restart";

            winText.gameObject.SetActive(true);
        }
    }

    // --------------------------------------------------
    // ZAKLJUCANA VRATA
    // --------------------------------------------------

    public void DoorLocked()
    {
        if (gameEnded || !gameStarted)
        {
            return;
        }

        messageText.text =
            "Door locked!\nComplete the objectives first.";
    }

    // --------------------------------------------------
    // GUBITAK ZIVOTA
    // --------------------------------------------------

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

        if (lives <= 0)
        {
            GameOver();
            return;
        }

        messageText.text =
            "You were caught!\n" +
            "Lives left: " + lives +
            "\nPuzzle progress reset!";

        // Vracamo igraca u Room1
        if (player != null &&
            playerStartPosition != null)
        {
            player.transform.position =
                playerStartPosition.position;
        }

        // --------------------------------------------------
        // RESET NAKON GUBITKA ZIVOTA
        // --------------------------------------------------

        // 413 se NE resetira.
        // Neprijatelj zato ostaje aktivan.

        // Kod 729 ponovno nije rijesen
        PuzzleKeypad.puzzleSolved = false;

        // Igrac ponovno nema kljuc
        KeycardInteraction.hasKeycard = false;

        // Kljuc se ponovno pojavljuje
        if (key != null)
        {
            key.SetActive(true);
        }

        // Terminal se resetira
        TerminalUse.terminalActivated = false;

        // Izlaz se ponovno zakljucava
        if (exitBlocker != null)
        {
            exitBlocker.SetActive(true);
        }

        /*
         * Tragove namjerno NE resetiramo.
         *
         * Igrac je vec otkrio 7, 2 i 9.
         * Nakon smrti mora ponovno unijeti 729,
         * uzeti kljuc i koristiti terminal.
         */
        if (cluesFound >= 3)
        {
            objectiveText.text =
                "OBJECTIVE:\nEnter the 3-digit code at the keypad\n" +
                "CLUES FOUND: 3/3";
        }
        else
        {
            objectiveText.text =
                "OBJECTIVE:\nFind the puzzle clues\n" +
                "CLUES FOUND: " + cluesFound + "/3";
        }
    }

    // --------------------------------------------------
    // GAME OVER
    // --------------------------------------------------

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

        messageText.gameObject.SetActive(false);
        objectiveText.gameObject.SetActive(false);

        if (winText != null)
        {
            winText.gameObject.SetActive(false);
        }

        if (loseText != null)
        {
            loseText.SetActive(true);
        }
    }

    // --------------------------------------------------
    // ZAVRSNI TERMINAL
    // --------------------------------------------------

    public void TerminalActivated()
    {
        if (gameEnded || !gameStarted)
        {
            return;
        }

        messageText.text =
            "ACCESS GRANTED!\nExit unlocked.";

        objectiveText.text =
            "OBJECTIVE:\nReach the EXIT";
    }

    // --------------------------------------------------
    // IZLAZ IZ IGRE
    // --------------------------------------------------

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}