using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonShop : MonoBehaviour
{
    public TextMeshProUGUI itemName, itemPrice;
    [SerializeField] Image itemImage, locked;
    public Minijuego minijuegoType;
    public int iD;

    private void Start()
    {
        if (Estoestotalmentemomentaneo.instance.idsComprados.Contains(iD))
        {
            locked.enabled = false;
        }
        else
        {
            locked.enabled = true;
        }
    }

    public void SetStats(ItemSO item)
    {
        itemName.text = item.itemName;
        itemPrice.text = item.itemPrice;
        itemImage.sprite = item.itemImage;
        locked.sprite = item.Locked;
        minijuegoType = item.minijuegoType;
        iD = item.iD;
    }

    public void Comprado()
    {
        locked.enabled = false;
    }
}