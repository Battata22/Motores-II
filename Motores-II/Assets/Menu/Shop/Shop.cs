using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Shop : MonoBehaviour
{

    //[SerializeField] ItemSO[] items;

    [SerializeField] ItemButtonShop buttonShopPrefab;
    [SerializeField] Transform parent;

    void Start()
    {
        var items = Resources.LoadAll<ItemSO>("ScriptableObjects");
        for (int i = 0; i < items.Length; i++)
        {
            var newItem = Instantiate(buttonShopPrefab, parent);
            newItem.SetStats(items[i]);
        }
    }


}