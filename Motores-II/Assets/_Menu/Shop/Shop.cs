using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop instance;

    //[SerializeField] ItemSO[] items;

    [SerializeField] ItemButtonShop buttonShopPrefab;
    [SerializeField] Transform parent;
    public static event Action ResetValues;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        var items = Resources.LoadAll<ItemSO>("ScriptableObjects");
        for (int i = 0; i < items.Length; i++)
        {
            var newItem = Instantiate(buttonShopPrefab, parent);
            newItem.SetStats(items[i]);
        }
    }

    private void Update()
    {
        
    }

    public void ResetData()
    {
        //resetear compras
        ResetValues();

        //resetear skin seleccionada a la basica
        PlayerPrefs.SetInt("SelectedFondo", 0);
        PlayerPrefs.SetInt("SelectedBall", 0);
        PlayerPrefs.SetInt("SelectedDiana", 0);
        PlayerPrefs.SetInt("SelectedGuantes", 0);
        PlayerPrefs.SetInt("Escalada_NeedTutorial", 1);
        PlayerPrefs.SetInt("Carlos_NeedTutorial", 1);
        PlayerPrefs.SetInt("BulletHell_NeedTutorial", 1);
        PlayerPrefs.SetInt("StickMode", 0);
        //resetear puntos
        PointsManager.Instance.SubstractPoints(PointsManager.Instance._points);



    }
}