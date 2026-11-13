
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string targetScene = "NightShift_Final";
    public KeyCode interactionKey = KeyCode.E;

    private bool playerNear = false;

    private void Update()
    {
        if (playerNear && Input.GetKeyDown(interactionKey))
        {
            SceneManager.LoadScene(targetScene);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            Debug.Log("Press E to enter the restricted area.");
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
