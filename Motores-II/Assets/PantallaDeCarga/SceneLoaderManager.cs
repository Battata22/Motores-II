using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour
{
    public bool chargeDone = false;

    int _sceneToLoad;

    public int SceneToLoad
    {
        get 
        {  
            return _sceneToLoad; 
        }

        set 
        {
            _sceneToLoad = value; 
            //LoadScene(value);
            StartCoroutine(LoadScene(value));
            //print("Se acaba de setear la siguiente escena");
        }
    }

    #region Singleton
    public static SceneLoaderManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    } 
    #endregion

    //void Start()
    //{
    //    StartCoroutine(LoadAsyncSceneRoutine(sceneElegida));
    //}

    public IEnumerator LoadScene(int sceneElegida)
    {
        SceneManager.LoadScene(1);
        yield return new WaitForSeconds(1);
        StartCoroutine(LoadAsyncSceneRoutine(sceneElegida));
    }
    private IEnumerator LoadAsyncSceneRoutine(int index)
    {
        //yield return new WaitForSeconds(fakeWait);

        //Esto lo carga automaticamente

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);

        //Creo que lo de abajo es para activar la escena cuando vos quieras y si ya esta cargada

        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                if (chargeDone)
                    asyncLoad.allowSceneActivation = true;
                else
                {
                    //print("ya estamos cargados");
                    ChargeDone();
                }
            }
            yield return null;
        }
    }

    public void ChargeDone() => chargeDone = true;

}
