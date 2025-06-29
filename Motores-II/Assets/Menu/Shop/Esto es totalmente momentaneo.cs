using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Estoestotalmentemomentaneo : MonoBehaviour
{

    public List<int> idsComprados;

    #region Singelton
    public static Estoestotalmentemomentaneo instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion


    void Start()
    {
        LoadMomentaneo();
    }


    void Update()
    {

    }

    public void SaveMomentaneo(int ID)
    {
        InventoryMomentaneo data = GetComponent<InventoryMomentaneo>();
        data.idsComprados.Add(ID);

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.dataPath + "/Menu/Shop/InventoryInfoMomentaneo.json", json);
    }

    public void LoadMomentaneo()
    {
        if (File.Exists(Application.dataPath + "/Menu/Shop/InventoryInfoMomentaneo.json") == true)
        {
            string json = File.ReadAllText(Application.dataPath + "/Menu/Shop/InventoryInfoMomentaneo.json");
            InventoryMomentaneo data = JsonUtility.FromJson<InventoryMomentaneo>(json);

            idsComprados = data.idsComprados;
        }
        else
        {
            idsComprados = new();
        }
    }
}

[System.Serializable]
public class InventoryMomentaneo
{
    public List<int> idsComprados;
}

