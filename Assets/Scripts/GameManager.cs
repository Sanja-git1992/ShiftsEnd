using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public GameObject winText;

    public bool codeFound = false;

    public void ShowCode()
    {
        codeFound = true;
        messageText.text = "Code found: 413";
    }

    
    public void Escape()
    {
    messageText.gameObject.SetActive(false);
    winText.SetActive(true);
    }
    

    public void DoorLocked()
    {
        messageText.text = "Door locked!";
    }
}