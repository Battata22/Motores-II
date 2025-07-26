using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScreen : MonoBehaviour
{
    public bool chargeDone = false;
    public int sceneElegida;

    void Start()
    {
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
                    //ChargeDone();

                    //Chequear si ya cargaron los valores cloud

                    yield return null;
                }
            }
            yield return null;
        }
    }

    public void ChargeDone() => chargeDone = true;

}
