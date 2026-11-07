
using UnityEngine;
using TMPro;

public class MaintenanceReport : MonoBehaviour
{
    public GameManager gameManager;
    public DocumentReader documentReader;
    public PowerRestoration powerRestoration;

    public TMP_Text messageText;

    [TextArea(2, 5)]
    public string reportTitle = "MAINTENANCE REPORT";

    [TextArea(4, 10)]
    public string reportBody =
        "Emergency power system restored.\n\n" +
        "The second security digit is: 2\n\n" +
        "One final digit remains.";

    public string reportHint = "PRESS ENTER TO CLOSE";

    private bool playerNear = false;
    private bool clueRegistered = false;

    private void Update()
    {
        if (gameManager == null ||
            documentReader == null ||
            powerRestoration == null)
            return;

        if (!playerNear ||
            !gameManager.gameStarted ||
            gameManager.gameEnded ||
            documentReader.IsDocumentOpen)
            return;

        if (!Input.GetKeyDown(KeyCode.E))
            return;

        if (!powerRestoration.powerRestored)
        {
            ShowMessage(
                "RESTORE POWER BEFORE READING THIS REPORT."
            );
            return;
        }

        if (messageText != null)
            messageText.gameObject.SetActive(false);

        documentReader.OpenDocument(
            reportTitle,
            reportBody,
            reportHint
        );

        if (!clueRegistered)
        {
            clueRegistered = true;
            gameManager.ClueFound();
        }
    }

    private void ShowMessage(string text)
    {
        if (messageText == null)
            return;

        messageText.text = text;
        messageText.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNear = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNear = false;
    }
}
