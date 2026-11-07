
using UnityEngine;
using TMPro;

public class BatteryPickup : MonoBehaviour
{
    public GameManager gameManager;
    public DocumentReader documentReader;
    public GameObject interactionText;
    public TMP_Text messageText;

    public static bool HasBattery { get; private set; }

    private bool playerNear;

    private void Start()
    {
        HasBattery = false;

        if (interactionText != null)
            interactionText.SetActive(false);
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
            !HasBattery;

        if (interactionText != null)
            interactionText.SetActive(canInteract);

        if (!canInteract || !Input.GetKeyDown(KeyCode.E))
            return;

        HasBattery = true;

        if (messageText != null)
        {
            messageText.text =
                "BATTERY COLLECTED. Find the electrical panel.";

            messageText.gameObject.SetActive(true);
        }

        if (interactionText != null)
            interactionText.SetActive(false);

        gameObject.SetActive(false);
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

    private void OnDisable()
    {
        if (interactionText != null)
            interactionText.SetActive(false);
    }
}
