using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UnlockedItems : MonoBehaviour
{
    public Dictionary<Minijuego, int> unlokeds;
    [SerializeField] bool isLocked = false;
    [SerializeField] ItemButtonShop itemScript;

    void Start()
    {
        itemScript = GetComponent<ItemButtonShop>();
        LoadInventory();
        if (unlokeds.TryGetValue(itemScript.minijuegoType, out int id))
        {
            if (id == itemScript.iD)
            {
                print("este esta en la lista " +  id);
            }
        }
    }

    
    void Update()
    {
        
    }

    public void UnlockItem(ItemButtonShop item)
    {
        //if (item.)
    }

    public void RestartInventory()
    {

    }

    public void SaveInventory(Minijuego minijuego, int ItemID)
    {
        Inventory data = new();
        data.unlokeds.Add(minijuego, ItemID);

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.dataPath + "/Menu/Shop/InventoryInfo.json", json);
    }

    public void LoadInventory()
    {
        if (File.Exists(Application.dataPath + "/Menu/Shop/InventoryInfo.json") == true)
        {
            string json = File.ReadAllText(Application.dataPath + "/Menu/Shop/InventoryInfo.json");
            Inventory data = JsonUtility.FromJson<Inventory>(json);

            unlokeds = data.unlokeds;
        }
        else
        {
            unlokeds = new();
        }
    }
}

[System.Serializable]
public class Inventory
{
    public Dictionary<Minijuego, int> unlokeds;
}

public enum Minijuego
{
    Arquero, Saltarin, Escalada, Carlos
}
