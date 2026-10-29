
using UnityEngine;

public class PuzzleNote : MonoBehaviour
{
    public GameManager gameManager;
    public DocumentReader documentReader;

    public GameObject interactionText;

    public string clueText = "CLUE";

    private bool playerNear = false;
    private bool clueFound = false;

    void Start()
    {
        if (interactionText != null)
            interactionText.SetActive(false);
    }

    void Update()
    {
        if (gameManager == null)
            return;

        bool canInteract =
            playerNear &&
            gameManager.gameStarted &&
            !gameManager.gameEnded;

        bool documentIsOpen =
            documentReader != null &&
            documentReader.IsDocumentOpen;

        if (interactionText != null)
        {
            interactionText.SetActive(
                canInteract && !documentIsOpen
            );
        }

        if (!canInteract || documentIsOpen)
            return;

        if (!Input.GetKeyDown(KeyCode.E))
            return;

        if (!gameManager.codeFound)
        {
            if (gameManager.messageText != null)
            {
                gameManager.messageText.text =
                    "Find the first code before searching for clues.";

                gameManager.messageText.gameObject.SetActive(true);
            }

            return;
        }

        if (documentReader != null)
        {
            if (gameManager.messageText != null)
                gameManager.messageText.gameObject.SetActive(false);

            documentReader.OpenDocument();
        }
        else if (gameManager.messageText != null)
        {
            gameManager.messageText.text = clueText;
            gameManager.messageText.gameObject.SetActive(true);
        }

        if (!clueFound)
        {
            clueFound = true;
            gameManager.ClueFound();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNear = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;

            if (interactionText != null)
                interactionText.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (interactionText != null)
            interactionText.SetActive(false);
    }
}
