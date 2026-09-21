using UnityEngine;

public class KeycardInteraction : MonoBehaviour
{
    // Iako se varijabla još zove hasKeycard,
    // sada zapravo predstavlja ima li igrač ključ.
    public static bool hasKeycard = false;

    public GameManager gameManager;

    private bool playerNear = false;

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            // Ključ se ne može uzeti prije rješavanja koda 729
            if (!PuzzleKeypad.puzzleSolved)
            {
                if (gameManager != null)
                {
                    gameManager.messageText.text =
                        "KEY LOCKED!\nSolve the keypad code first.";
                }

                Debug.Log("Key locked - solve code 729 first!");
                return;
            }

            // Kod 729 je riješen -> igrač može uzeti ključ
            hasKeycard = true;

            if (gameManager != null)
            {
                gameManager.messageText.text =
                    "KEY COLLECTED!";

                gameManager.objectiveText.text =
                    "OBJECTIVE:\nUse the terminal";
            }

            Debug.Log("Key collected!");

            // Ključ nestaje iz scene jer ga je igrač pokupio
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
        }
    }
}