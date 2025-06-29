using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CompraItem : MonoBehaviour
{
    [SerializeField] ItemButtonShop itemScript;
    [SerializeField] bool bloqueado = true;
    
    void Start()
    {
        itemScript = GetComponent<ItemButtonShop>();
    }

    
    void Update()
    {
        
    }

    public void ComprarItem()
    {
        if (bloqueado && PointsManager.Instance._points >= int.Parse(itemScript.itemPrice.text))
        {
            bloqueado = false;
            PointsManager.Instance.SubstractPoints(int.Parse(itemScript.itemPrice.text));
            print(itemScript.iD);
            Estoestotalmentemomentaneo.instance.SaveMomentaneo(itemScript.iD);
            itemScript.Comprado();
            print("comprado");
        }
        else if (PointsManager.Instance._points < int.Parse(itemScript.itemPrice.text))
        {
            print("no tenes guita");
        }
    }

    
}
