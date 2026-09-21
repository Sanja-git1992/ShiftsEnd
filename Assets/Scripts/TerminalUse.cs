using UnityEngine;

public class TerminalUse : MonoBehaviour
{
    private bool playerNear = false;

    public GameObject exitBlocker;
    public GameManager gameManager;
    public AudioSource successSound;

    public static bool terminalActivated = false;

    void Update()
    {
        // Provjerava je li igrač blizu terminala i je li pritisnuo E
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            // PRVO provjeravamo je li pronađen početni kod 413
            if (!gameManager.codeFound)
            {
                Debug.Log("ACCESS DENIED - Find code 413 first!");

                gameManager.messageText.text =
                    "ACCESS DENIED!\nFind the first code.";

                return;
            }

            // ZATIM provjeravamo ima li igrač Keycard
            if (!KeycardInteraction.hasKeycard)
            {
                Debug.Log("ACCESS DENIED - Keycard required!");

                gameManager.messageText.text =
                    "ACCESS DENIED!\nKeycard required.";

                return;
            }

            // Ako imamo i kod 413 i Keycard,
            // terminal se uspješno aktivira
            terminalActivated = true;

            Debug.Log("ACCESS GRANTED - Final exit unlocked!");

            // Uklanjamo fizičku prepreku ispred izlaza
            if (exitBlocker != null)
            {
                exitBlocker.SetActive(false);
            }

            // Obavještavamo GameManager da je terminal aktiviran
            if (gameManager != null)
            {
                gameManager.TerminalActivated();
            }

            // Reproduciramo zvuk uspješnog pristupa
            if (successSound != null)
            {
                successSound.Play();
            }
        }
    }

    // Kada Player uđe u područje terminala
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
        }
    }

    // Kada Player izađe iz područja terminala
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
        }
    }
}