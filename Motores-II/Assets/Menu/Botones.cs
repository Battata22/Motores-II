using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Botones : MonoBehaviour
{

    public void CargarArquero()
    {
        SceneManager.LoadScene("Arquero");
    }

    public void CargarCarlos()
    {
        SceneManager.LoadScene("CarlosDice");
    }

    public void CargarEscalar()
    {
        SceneManager.LoadScene("Escalada");
    }

    public void CargarSaltarin()
    {
        SceneManager.LoadScene("Saltarin");
    }
}
