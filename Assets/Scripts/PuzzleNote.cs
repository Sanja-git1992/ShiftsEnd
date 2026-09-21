using UnityEngine;

public class PuzzleNote : MonoBehaviour
{
    public GameManager gameManager;

    // Tekst koji se razlikuje za svaki papir:
    // CLUE 1 - First digit: 7
    // CLUE 2 - Second digit: 2
    // CLUE 3 - Third digit: 9
    public string clueText = "CLUE";

    private bool playerNear = false;

    // Pamti je li ovaj konkretni trag vec pronaden
    private bool clueFound = false;

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (gameManager == null)
            {
                return;
            }

            // Prvo se mora pronaci pocetni kod 413
            if (!gameManager.codeFound)
            {
                gameManager.messageText.text =
                    "Find the first code before searching for clues.";

                return;
            }

            // Prikazujemo tekst traga
            gameManager.messageText.text = clueText;

            // Brojimo ovaj papir samo prvi put
            if (!clueFound)
            {
                clueFound = true;
                gameManager.ClueFound();
            }
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