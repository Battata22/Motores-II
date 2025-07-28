using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonVolver : MonoBehaviour
{
    [SerializeField] Canvas _confirmCanvas;

    public void VolverMenu()
    {
        //SceneManager.LoadScene("Menu");
        _confirmCanvas.enabled = true;
        //SceneLoaderManager.instance.SceneToLoad = 2;
    }
}
