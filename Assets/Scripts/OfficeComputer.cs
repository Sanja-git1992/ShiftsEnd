using UnityEngine;

public class OfficeComputer : MonoBehaviour
{
    public GameManager gameManager;

    private bool playerNear = false;
    private bool computerChecked = false;

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E) && !computerChecked)
        {
            computerChecked = true;

            gameManager.messageText.text =
                "SECURITY SYSTEM OFFLINE!\n\n" +
                "Emergency access requires the security code.";

            gameManager.objectiveText.text =
                "OBJECTIVE:\nFind the security code";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;

            if (!computerChecked)
            {
                gameManager.messageText.text =
                    "Press E to check the computer";
            }
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
