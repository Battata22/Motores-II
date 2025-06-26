using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveWithJson : MonoBehaviour
{
    [SerializeField] protected SaveData _saveData = new SaveData();
    protected string path;

    protected virtual void Awake()
    {
        //string gameName = Application.productName;
        //string companyName = Application.companyName;
        //string version = Application.version;

        //sirve para guardar el path de nuestro save en la ruta de la aplicacion pero no sirve para mobile
#if UNITY_EDITOR   
        path = Application.dataPath + "/Pepito.Hola"; //path = Application.dataPath + "/SaveData.json";
#endif
        //sirve para guardar el path de nuestro save en la ruta appdata y sirve para mobile
#if !UNITY_EDITOR
        path = Application.persistentDataPath + "/Pepito.Hola";
#endif

        Debug.Log(path);

        LoadGame();
    }

    protected void SaveGame()
    {
        string json = JsonUtility.ToJson( _saveData, true);
        File.WriteAllText(path, json);

        //Si se quisiera PlayerPrefs en vez de un archivo...
        // PlayerPrefs.SetString("MySaveData", json);

        Debug.Log(json);
        Debug.Log("Saving Game");
    }

    protected void LoadGame()
    {
        if (Directory.Exists(path))
        {
            string json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, _saveData);
            Debug.Log("Loading Game");
        }

        //Si se quisiera PlayerPrefs en vez de un archivo...
       // if (PlayerPrefs.HasKey("MySaveData"))
       // {
       //     string json = PlayerPrefs.GetString("MySaveData");
       //     JsonUtility.FromJsonOverwrite(json, _saveData);
       //     Debug.Log("Loading Game");
       // }
    }

    public void DeleteGame()
    {
        File.Delete(path);

        //Si se quisiera PlayerPrefs en vez de un archivo...
        // PlayerPrefs.DeleteKey("MySaveData");
        // PlayerPrefs.DeleteAll();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    protected void OnApplicationPause(bool pause)
    {
        if (pause) SaveGame();
    }

    /* protected void OnApplicationFocus(bool focus)
     {
         if (!focus) SaveGame();
     }*/

    protected void OnApplicationQuit()
    {
        SaveGame();
    }
}
