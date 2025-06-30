using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonShop : MonoBehaviour
{
    public TextMeshProUGUI itemName, itemPrice;
    public Image itemImage, locked, check;
    public Minijuego minijuegoType;
    public int iD;



    private void Start()
    {
        //if (Estoestotalmentemomentaneo.instance.idsComprados.Contains(iD))
        //{
        //    locked.enabled = false;
        //}
        //else
        //{
        //    locked.enabled = true;
        //}
        
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

        //#region IFs
        //if (iD == 1)
        //{
        //    InventoryBools.skinArcFondo1 = true;
        //}
        //else if (iD == 2)
        //{
        //    InventoryBools.skinArcFondo2 = true;
        //}
        //else if (iD == 3)
        //{
        //    InventoryBools.skinArcBall1 = true;
        //}
        //else if (iD == 4)
        //{
        //    InventoryBools.skinArcBall2 = true;
        //}
        //else if (iD == 5)
        //{
        //    InventoryBools.skinArcDiana1 = true;
        //}
        //else if (iD == 6)
        //{
        //    InventoryBools.skinArcDiana2 = true;
        //}
        //else if (iD == 7)
        //{
        //    InventoryBools.skinEscGuantes1 = true;
        //}
        //else if (iD == 8)
        //{
        //    InventoryBools.skinEscGuantes2 = true;
        //} 
        //#endregion

    }

   
}