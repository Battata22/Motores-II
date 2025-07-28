using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonesVictoriaRunner : MonoBehaviour
{

    [SerializeField] GameObject _player;
    [SerializeField] SpawnerPlataformas _spawnerScript;
    [SerializeField] GameObject _victoria;
    [SerializeField] GameObject boton1, boton2, boton3;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _errorClip;

    public void GoMenu()
    {
        //SceneManager.LoadScene("Menu");
        SceneLoaderManager.instance.SceneToLoad = 2;
    }

    public void Retry()
    {
        if (StaminaSystem.Instance.CurrentStamina >= StaminaSystem.Instance.gameStaminaCost)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            //que suene el error
            _audioSource.clip = _errorClip;
            _audioSource.Play();
        }
    }

    public void Continuar()
    {
        _player.SetActive(true);
        _spawnerScript.enabled = true;
        _victoria.SetActive(false);
        boton1.SetActive(false);
        boton2.SetActive(false);
        boton3.SetActive(false);
    }

}
