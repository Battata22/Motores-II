using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonVolver : MonoBehaviour
{
    public void VolverMenu()
    {
        //SceneManager.LoadScene("Menu");
        SceneLoaderManager.instance.SceneToLoad = 2;
    }
}
