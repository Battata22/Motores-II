using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausaInGame : MonoBehaviour
{
    public bool isPaused = false;
    //[SerializeField] Canvas _canvasPausa;
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

    void Start()
    {
        //_canvasPausa = GetComponent<Canvas>();
    }


    void Update()
    {

    }

    public void Pausar()
    {
        //_canvasPausa.enabled = true;
        Paused();
        isPaused = true;
    }

    public void Despausar()
    {
        //_canvasPausa.enabled = false;
        Despaused();
        isPaused = false;
    }

    public void VolverAlMenu()
    {
        //SceneManager.LoadScene("Menu");
        SceneLoaderManager.instance.SceneToLoad = 2;
    }
    void TestEventPaused()
    {
        print("Paused");
    }
    void TestEventDespaused()
    {
        print("Despaused");
    }
}
