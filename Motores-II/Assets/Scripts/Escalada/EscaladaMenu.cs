using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscaladaMenu : MonoBehaviour
{
    //a

    private void Update()
    {
        if (EscaladaManager.Instance.paused && Input.GetKeyDown(KeyCode.Escape)) ClosePause(); 

    }

    public void RefresScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Escalada");
    }

    public void GoToMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Menu");
    }


    public void ClosePause()
    {
        Time.timeScale = 1.0f;
        EscaladaManager.Instance.paused = false;
        EscaladaManager.Instance.pauseMenu.gameObject.SetActive(false);
    }

    public void OpenOptions()
    {
        //Time.timeScale = 1.0f;
        EscaladaManager.Instance.pauseMenu.gameObject.SetActive(false);

    }
}
