using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Objects/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public string itemPrice;
    public Sprite itemImage;
    public Sprite Locked;
    public Sprite check;
    public Minijuego minijuegoType;
    public int iD;

    public ItemSO(string itemName, string itemPrice, Sprite itemImage, Sprite locked, Minijuego minijuegoType, int iD, Sprite check)
    {
        this.itemName = itemName;
        this.itemPrice = itemPrice;
        this.itemImage = itemImage;
        this.Locked = locked;
        this.minijuegoType = minijuegoType;
        this.iD = iD;
        this.check = check;
    }

    public void SetValues(string itemName, string itemPrice, Sprite itemImage, Sprite locked, Minijuego minijuegoType, int iD, Sprite check)
    {
        this.itemName = itemName;
        this.itemPrice = itemPrice;
        this.itemImage = itemImage;
        this.Locked = locked;
        this.minijuegoType = minijuegoType;
        this.iD = iD;
        this.check = check;
    }
}
