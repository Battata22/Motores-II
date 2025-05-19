using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonVolver : MonoBehaviour
{

    void Start()
    {

    }


    void Update()
    {

    }

    public void VolverMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
