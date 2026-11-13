
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string targetScene = "NightShift_Final";
    public KeyCode interactionKey = KeyCode.E;

    [Header("Access Requirements")]
    public bool requireReceptionDocument = true;

    [Header("References")]
    public ShiftRegisterInteraction receptionDocument;

    private bool playerNear = false;

    void Update()
    {
        if (!playerNear || !Input.GetKeyDown(interactionKey))
            return;

        if (requireReceptionDocument)
        {
            if (receptionDocument == null ||
                !receptionDocument.HasBeenRead)
            {
                Debug.Log(
                    "ACCESS DENIED: Read the reception document first."
                );
                return;
            }
        }

        Debug.Log("Access granted. Entering restricted area.");
        SceneManager.LoadScene(targetScene);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            Debug.Log("Press E to enter the restricted area.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNear = false;
    }
}
