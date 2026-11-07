
using UnityEngine;
using TMPro;

public class PowerRestoration : MonoBehaviour
{
    public GameManager gameManager;
    public DocumentReader documentReader;
    public GameObject interactionText;
    public TMP_Text messageText;

    public GameObject installedBattery;

    public bool powerRestored = false;

    private bool playerNear = false;

    private void Start()
    {
        powerRestored = false;

        if (installedBattery != null)
            installedBattery.SetActive(false);
    }

    private void Update()
    {
        if (gameManager == null)
            return;

        bool documentOpen =
            documentReader != null &&
            documentReader.IsDocumentOpen;

        bool canInteract =
            playerNear &&
            gameManager.gameStarted &&
            !gameManager.gameEnded &&
            !documentOpen &&
            !powerRestored;

        if (interactionText != null)
            interactionText.SetActive(canInteract);

        if (!canInteract || !Input.GetKeyDown(KeyCode.E))
            return;

        // Prvo je potrebno pronaci pocetni kod 413.
        if (!gameManager.codeFound)
        {
            ShowMessage(
                "Find the initial security code first."
            );
            return;
        }

        // Prvi trag mora biti pronaden u Archive Roomu.
        if (!PuzzleNote.FirstClueFound)
        {
            ShowMessage(
                "Search the archive before restoring power."
            );
            return;
        }

        // Igrac mora imati akumulator.
        if (!BatteryPickup.HasBattery)
        {
            ShowMessage(
                "POWER OFFLINE. A battery is missing."
            );
            return;
        }

        powerRestored = true;

        if (installedBattery != null)
            installedBattery.SetActive(true);

        ShowMessage(
            "POWER RESTORED. Search for the maintenance report."
        );

        if (interactionText != null)
            interactionText.SetActive(false);
    }

    private void ShowMessage(string message)
    {
        if (messageText == null)
            return;

        messageText.text = message;
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

