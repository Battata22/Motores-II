using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Botones : MonoBehaviour
{
    [SerializeField] GameObject canvasMenu;
    [SerializeField] GameObject canvasMinijuegos;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void Minijuegos()
    {
        canvasMenu.SetActive(false);
        canvasMinijuegos.SetActive(true);
    }

    public void VolverMenu()
    {
        canvasMenu.SetActive(true);
        canvasMinijuegos.SetActive(false);
    }

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
