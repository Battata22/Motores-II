using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscaladaMenu : MonoBehaviour
{
    //a
    [SerializeField] Canvas _pauseCanvas;
    [SerializeField] Canvas _optionCanvas;

    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _errorSound;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(_pauseCanvas.enabled)
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

    public void RefresScene()
    {
        if (!StaminaSystem.Instance.HasEnoughStamina(StaminaSystem.Instance.gameStaminaCost))
        {
            _audioSource.PlayOneShot(_errorSound);

            Debug.Log($"ESTAMINA INSUFICIENTE {StaminaSystem.Instance.CurrentStamina} \n" +
                $"Estamina Necesaria {StaminaSystem.Instance.gameStaminaCost}");
            return;
        }

        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Escalada");
    }

    public void GoToMenu()
    {
        Time.timeScale = 1.0f;
        //SceneManager.LoadScene("Menu");
        SceneLoaderManager.instance.SceneToLoad = 2;
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
