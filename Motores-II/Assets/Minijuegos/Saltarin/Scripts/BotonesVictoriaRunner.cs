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

    public void GoMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
