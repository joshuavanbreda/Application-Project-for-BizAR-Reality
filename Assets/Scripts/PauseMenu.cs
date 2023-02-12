using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private NewMovement newMovement;
    public AudioController audioController;
    public GameObject pauseCanvas;
    private bool playFunkySounds;

    private void Start()
    {
        newMovement = GameObject.Find("Player").GetComponent<NewMovement>();
        playFunkySounds = false;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        newMovement.isInitialized = true;
        pauseCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.Equals(0, 0);
        Cursor.visible = false;
    }

    public void SoundToggle()
    {
        playFunkySounds = !playFunkySounds;

        if (playFunkySounds)
        {
            audioController.ChangeToFunky();
        }
        else
        {
            audioController.ChangeToNormal();
        }
    }
}
