using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI objectiveText;
    public GameObject winText;
    public GameObject loseText;
    public GameObject enemy;

    public bool codeFound = false;
    public bool gameEnded = false;

    void Start()
    {
        objectiveText.text = "OBJECTIVE:\n\nFind the code";

        messageText.text = "Find the code.";

        if (winText != null)
        {
            winText.SetActive(false);
        }

        if (loseText != null)
        {
            loseText.SetActive(false);
        }

        if (enemy != null)
        {
            enemy.SetActive(false);
        }
    }

    public void ShowCode()
    {
        if (gameEnded) return;

        codeFound = true;

        objectiveText.text = "OBJECTIVE:\n\nReach the exit";

        messageText.text =
            "You found a note!\n\nCode: 413\n\nThe enemy is active!";

        if (enemy != null)
        {
            enemy.SetActive(true);
        }
    }

    public void Escape()
    {
        if (gameEnded) return;

        gameEnded = true;
        messageText.gameObject.SetActive(false);
        objectiveText.gameObject.SetActive(false);

        if (winText != null)
        {
            winText.SetActive(true);
        }
    }

    public void DoorLocked()
    {
        if (gameEnded) return;

        messageText.text = "Door locked!\nFind the code first.";
    }

    public void GameOver()
    {
        if (gameEnded) return;

        gameEnded = true;
        messageText.gameObject.SetActive(false);
        objectiveText.gameObject.SetActive(false);

        if (winText != null)
        {
            winText.SetActive(false);
        }

        if (loseText != null)
        {
            loseText.SetActive(true);
        }
    }
}