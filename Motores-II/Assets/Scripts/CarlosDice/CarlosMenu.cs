using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarlosMenu : MonoBehaviour
{
    [SerializeField] Canvas _pauseCanvas;
    [SerializeField] Canvas _optionCanvas;

    //a
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_pauseCanvas.enabled)
                ClosePause();
            else
                OpenPause();
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
            OpenPause();
    }


    public void GoToMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Menu");
    }

    public void ClosePause()
    {
        Time.timeScale = 1.0f;
        //EscaladaManager.Instance.paused = false;
        _pauseCanvas.enabled = false;
    }

    public void OpenPause()
    {
        ///if (paused) return;

        _pauseCanvas.enabled = true;
        Time.timeScale = 0;
    }

    public void OpenOptions()
    {
        //Time.timeScale = 1.0f;
        _pauseCanvas.enabled = false;
        _optionCanvas.enabled = true;
        //EscaladaManager.Instance.pauseMenu.gameObject.SetActive(false);

    }
}
