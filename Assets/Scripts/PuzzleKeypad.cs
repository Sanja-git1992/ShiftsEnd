using UnityEngine;

public class PuzzleKeypad : MonoBehaviour
{
    public GameManager gameManager;

    public static bool puzzleSolved = false;

    private bool playerNear = false;
    private string enteredCode = "";

    void Update()
    {
        // Ako igrac nije blizu keypada, nista se ne dogada
        if (!playerNear)
        {
            return;
        }

        // Ako je puzzle vec rijesen, nema ponovnog unosa
        if (puzzleSolved)
        {
            return;
        }

        // PRVI UVJET:
        // igrac prvo mora pronaci pocetni kod 413
        if (!gameManager.codeFound)
        {
            return;
        }

        // DRUGI UVJET:
        // igrac mora pronaci sva 3 puzzle traga
        if (gameManager.cluesFound < 3)
        {
            return;
        }

        // --------------------------------------------------
        // UNOS BROJEVA
        // --------------------------------------------------

        if (Input.GetKeyDown(KeyCode.Alpha0) ||
            Input.GetKeyDown(KeyCode.Keypad0))
        {
            AddDigit("0");
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) ||
            Input.GetKeyDown(KeyCode.Keypad1))
        {
            AddDigit("1");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) ||
            Input.GetKeyDown(KeyCode.Keypad2))
        {
            AddDigit("2");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) ||
            Input.GetKeyDown(KeyCode.Keypad3))
        {
            AddDigit("3");
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) ||
            Input.GetKeyDown(KeyCode.Keypad4))
        {
            AddDigit("4");
        }

        if (Input.GetKeyDown(KeyCode.Alpha5) ||
            Input.GetKeyDown(KeyCode.Keypad5))
        {
            AddDigit("5");
        }

        if (Input.GetKeyDown(KeyCode.Alpha6) ||
            Input.GetKeyDown(KeyCode.Keypad6))
        {
            AddDigit("6");
        }

        if (Input.GetKeyDown(KeyCode.Alpha7) ||
            Input.GetKeyDown(KeyCode.Keypad7))
        {
            AddDigit("7");
        }

        if (Input.GetKeyDown(KeyCode.Alpha8) ||
            Input.GetKeyDown(KeyCode.Keypad8))
        {
            AddDigit("8");
        }

        if (Input.GetKeyDown(KeyCode.Alpha9) ||
            Input.GetKeyDown(KeyCode.Keypad9))
        {
            AddDigit("9");
        }

        // Backspace brise trenutni unos
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            enteredCode = "";

            gameManager.messageText.text =
                "KEYPAD\nCode cleared.";
        }

        // Enter potvrduje uneseni kod
        if (Input.GetKeyDown(KeyCode.Return) ||
            Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            CheckCode();
        }
    }

    void AddDigit(string digit)
    {
        // Kod ima maksimalno 3 znamenke
        if (enteredCode.Length < 3)
        {
            enteredCode += digit;

            gameManager.messageText.text =
                "KEYPAD\nEntered: " + enteredCode +
                "\nPress ENTER to confirm.";
        }
    }

    void CheckCode()
    {
        // Tocan puzzle kod
        if (enteredCode == "729")
        {
            puzzleSolved = true;

            gameManager.messageText.text =
                "CODE CORRECT!\n729 accepted.";

            gameManager.objectiveText.text =
                "OBJECTIVE:\nFind the key";

            Debug.Log("Puzzle solved - code 729 correct!");
        }
        else
        {
            // Pogresan kod -> brisemo unos
            enteredCode = "";

            gameManager.messageText.text =
                "WRONG CODE!\nTry again.";

            Debug.Log("Wrong puzzle code!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;

            // Nije pronaden pocetni kod 413
            if (!gameManager.codeFound)
            {
                gameManager.messageText.text =
                    "KEYPAD LOCKED!\nFind code 413 first.";

                return;
            }

            // Nisu pronadena sva tri traga
            if (gameManager.cluesFound < 3)
            {
                gameManager.messageText.text =
                    "KEYPAD LOCKED!\nFind all 3 clues first.\n" +
                    "Clues found: " +
                    gameManager.cluesFound + "/3";

                return;
            }

            // Sve je spremno za unos 729
            if (!puzzleSolved)
            {
                gameManager.messageText.text =
                    "KEYPAD READY\nEnter the 3-digit code.";
            }
            else
            {
                gameManager.messageText.text =
                    "KEYPAD UNLOCKED\nCode already accepted.";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;

            // Ako igrac ode od keypada,
            // nedovrseni unos se brise
            enteredCode = "";
        }
    }
}