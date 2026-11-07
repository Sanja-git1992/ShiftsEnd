
using UnityEngine;
using TMPro;

public class DocumentReader : MonoBehaviour
{
    public GameObject documentPanel;
    public PlayerController playerController;

    public TMP_Text documentTitle;
    public TMP_Text documentBody;
    public TMP_Text documentHint;

    private bool documentOpen = false;
    public bool IsDocumentOpen => documentOpen;

    private float previousTimeScale = 1f;

    private void Start()
    {
        if (documentPanel != null)
            documentPanel.SetActive(false);
    }

    private void Update()
    {
        if (documentOpen && Input.GetKeyDown(KeyCode.Return))
        {
            CloseDocument();
        }
    }

    // Zadrzava podrsku za postojeci prvi dokument.
    public void OpenDocument()
    {
        ShowDocument();
    }

    // Novi dokumenti mogu prikazivati vlastiti tekst.
    public void OpenDocument(
        string title,
        string body,
        string hint)
    {
        if (documentOpen || documentPanel == null)
            return;

        if (documentTitle != null)
            documentTitle.text = title;

        if (documentBody != null)
            documentBody.text = body;

        if (documentHint != null)
            documentHint.text = hint;

        ShowDocument();
    }

    private void ShowDocument()
    {
        if (documentPanel == null || documentOpen)
            return;

        previousTimeScale = Time.timeScale;
        documentOpen = true;

        documentPanel.SetActive(true);
        Time.timeScale = 0f;

        if (playerController != null)
            playerController.controlsEnabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseDocument()
    {
        if (!documentOpen)
            return;

        documentOpen = false;
        documentPanel.SetActive(false);

        Time.timeScale = previousTimeScale;

        if (playerController != null)
            playerController.controlsEnabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        if (documentOpen)
            CloseDocument();
    }
}

