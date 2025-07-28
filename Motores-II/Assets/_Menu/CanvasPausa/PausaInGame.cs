using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausaInGame : MonoBehaviour
{
    public bool isPaused = false;
    public event Action Paused;
    public event Action Despaused;


    #region Singleton
    public static PausaInGame Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            //DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        Paused += TestEventPaused;
        Despaused += TestEventDespaused;
    } 
    #endregion

    public void Pausar()
    {
        Paused();
        isPaused = true;
    }

    public void Despausar()
    {
        Despaused();
        isPaused = false;
    }

    public void VolverAlMenu()
    {
        SceneLoaderManager.instance.SceneToLoad = 2;
    }
    void TestEventPaused()
    {
        //print("Paused");
    }
    void TestEventDespaused()
    {
        //print("Despaused");
    }
}
