using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscaladaMenu : MonoBehaviour
{
    //a
    public void RefresScene()
    {
        SceneManager.LoadScene("Escalada");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
