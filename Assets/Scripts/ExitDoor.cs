using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public GameManager gameManager;

    private bool playerNearby = false;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (gameManager.codeFound)
            {
                gameManager.Escape();
            }
            else
            {
                gameManager.DoorLocked();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}