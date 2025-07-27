using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CompraItem : MonoBehaviour
{
    [SerializeField] ItemButtonShop itemScript;
    [SerializeField] bool bloqueado = true;
    //[SerializeField] bool _1, _2, _3, _4, _5, _6, _7, _8;
    [SerializeField] AudioSource AudioSource;
    [SerializeField] AudioClip purchase, selected, error;
    [SerializeField] Image test;

    void Start()
    {
        itemScript = GetComponent<ItemButtonShop>();

        AudioSource = GetComponentInChildren<AudioSource>();

        //LoadBool();

        CargarDatos();

        Shop.ResetValues += ResetMe;

        #region aa
        //if (itemScript.iD == 1)
        //{
        //    _1 = true;
        //}
        //else if (itemScript.iD == 2)
        //{
        //    _2 = true;
        //}
        //else if (itemScript.iD == 3)
        //{
        //    _3 = true;
        //}
        //else if (itemScript.iD == 4)
        //{
        //    _4 = true;
        //}
        //else if (itemScript.iD == 5)
        //{
        //    _5 = true;
        //}
        //else if (itemScript.iD == 6)
        //{
        //    _6 = true;
        //}
        //else if (itemScript.iD == 7)
        //{
        //    _7 = true;
        //}
        //else if (itemScript.iD == 8)
        //{
        //    _8 = true;
        //}

        //if (_1)
        //{
        //    if (Estoestotalmentemomentaneo.instance.skinArcBall1)
        //    {
        //        itemScript.locked.enabled = false;
        //    }
        //    else
        //    {
        //        itemScript.locked.enabled = true;
        //    }
        //}
        //else if (_2)
        //{
        //    if (Estoestotalmentemomentaneo.instance.skinArcBall2)
        //    {
        //        itemScript.locked.enabled = false;
        //    }
        //    else
        //    {
        //        itemScript.locked.enabled = true;
        //    }
        //}
        //else if (_3)
        //{
        //    if (Estoestotalmentemomentaneo.instance.skinArcDiana1)
        //    {
        //        itemScript.locked.enabled = false;
        //    }
        //    else
        //    {
        //        itemScript.locked.enabled = true;
        //    }
        //}
        //else if (_4)
        //{
        //    if (Estoestotalmentemomentaneo.instance.skinArcDiana2)
        //    {
        //        itemScript.locked.enabled = false;
        //    }
        //    else
        //    {
        //        itemScript.locked.enabled = true;
        //    }
        //}
        //else if (_5)
        //{
        //    if (Estoestotalmentemomentaneo.instance.skinArcFondo1)
        //    {
        //        itemScript.locked.enabled = false;
        //    }
        //    else
        //    {
        //        itemScript.locked.enabled = true;
        //    }
        //}
        //else if (_6)
        //{
        //    if (Estoestotalmentemomentaneo.instance.skinArcFondo2)
        //    {
        //        itemScript.locked.enabled = false;
        //    }
        //    else
        //    {
        //        itemScript.locked.enabled = true;
        //    }
        //}
        //else if (_7)
        //{
        //    if (Estoestotalmentemomentaneo.instance.skinEscGuantes1)
        //    {
        //        itemScript.locked.enabled = false;
        //    }
        //    else
        //    {
        //        itemScript.locked.enabled = true;
        //    }
        //}
        //else if (_8)
        //{
        //    if (Estoestotalmentemomentaneo.instance.skinEscGuantes2)
        //    {
        //        itemScript.locked.enabled = false;
        //    }
        //    else
        //    {
        //        itemScript.locked.enabled = true;
        //    }
        //} 
        #endregion
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt(itemScript.iD.ToString()) == 0)
        {
            bloqueado = true;
            itemScript.locked.enabled = true;
        }
        else
        {
            bloqueado = false;
            itemScript.locked.enabled = false;
        }

        if(itemScript.iD == 1 || itemScript.iD == 2)
        {
            if (PlayerPrefs.GetInt("SelectedFondo") == itemScript.iD)
            {
                itemScript.check.enabled = true;
            }
            else
            {
                itemScript.check.enabled = false;
            }
        }
        if (itemScript.iD == 3 || itemScript.iD == 4)
        {
            if (PlayerPrefs.GetInt("SelectedBall") == itemScript.iD)
            {
                itemScript.check.enabled = true;
            }
            else
            {
                itemScript.check.enabled = false;
            }
        }
        if (itemScript.iD == 5 || itemScript.iD == 6)
        {
            if (PlayerPrefs.GetInt("SelectedDiana") == itemScript.iD)
            {
                itemScript.check.enabled = true;
            }
            else
            {
                itemScript.check.enabled = false;
            }
        }
        if (itemScript.iD == 7 || itemScript.iD == 8)
        {
            if (PlayerPrefs.GetInt("SelectedGuantes") == itemScript.iD)
            {
                itemScript.check.enabled = true;
            }
            else
            {
                itemScript.check.enabled = false;
            }
        }
    }

    void CargarDatos()
    {

        if (PlayerPrefs.GetInt(itemScript.iD.ToString()) == 0)
        {
            bloqueado = true;
            itemScript.locked.enabled = true;
        }
        else
        {
            bloqueado = false;
            itemScript.locked.enabled = false;
        }
    }

    public void TouchItem()
    {
        if (bloqueado && PointsManager.Instance._points >= int.Parse(itemScript.itemPrice.text))
        {
            bloqueado = false;
            //PointsManager.Instance.SubstractPoints(int.Parse(itemScript.itemPrice.text));
            PointsManager.Instance.SubstractPoints(int.Parse(itemScript.itemPrice.text));
            //print(itemScript.iD);
            //Estoestotalmentemomentaneo.instance.SaveMomentaneo(itemScript.iD);
            //itemScript.Comprado();
            itemScript.locked.enabled = false;
            PlayerPrefs.SetInt(itemScript.iD.ToString(), 1);
            PlayerPrefs.Save();
            AudioSource.clip = purchase;
            AudioSource.Play();
            //print("comprado");
        }
        else if (bloqueado && PointsManager.Instance._points < int.Parse(itemScript.itemPrice.text))
        {
            print("no tenes guita");
            AudioSource.clip = error;
            AudioSource.Play();
        }

        if (!bloqueado)
        {
            if(itemScript.iD == 1)
            {
                PlayerPrefs.SetInt("SelectedFondo", 1);
            }
            else if (itemScript.iD == 2)
            {
                PlayerPrefs.SetInt("SelectedFondo", 2);
            }
            else if (itemScript.iD == 3)
            {
                PlayerPrefs.SetInt("SelectedBall", 3);
            }
            else if (itemScript.iD == 4)
            {
                PlayerPrefs.SetInt("SelectedBall", 4);
            }
            else if (itemScript.iD == 5)
            {
                PlayerPrefs.SetInt("SelectedDiana", 5);
            }
            else if (itemScript.iD == 6)
            {
                PlayerPrefs.SetInt("SelectedDiana", 6);
            }
            else if (itemScript.iD == 7)
            {
                PlayerPrefs.SetInt("SelectedGuantes", 7);
            }
            else if (itemScript.iD == 8)
            {
                PlayerPrefs.SetInt("SelectedGuantes", 8);
            }
            AudioSource.clip = selected;
            AudioSource.Play();
        }
    }

    void ResetMe()
    {
        PlayerPrefs.SetInt(itemScript.iD.ToString(), 0);
        StaminaSystem.Instance.ResetStamina();
    }

    #region CuidadoConLosOjos
    //public void SaveBool()
    //{
    //    InventoryBools data = new();
    //    data.skinArcFondo1 = _1;
    //    data.skinArcFondo2 = _2;
    //    data.skinArcBall1 = _3;
    //    data.skinArcBall2 = _4;
    //    data.skinArcDiana1 = _5;
    //    data.skinArcDiana2 = _6;
    //    data.skinEscGuantes1 = _7;
    //    data.skinEscGuantes2 = _8;


    //    string json = JsonUtility.ToJson(data, true);
    //    File.WriteAllText(Application.dataPath + "/Menu/Shop/InventoryBools.json", json);
    //}

    //public void LoadBool()
    //{
    //    if (File.Exists(Application.dataPath + "/Menu/Shop/InventoryBools.json") == true)
    //    {
    //        string json = File.ReadAllText(Application.dataPath + "/Menu/Shop/InventoryBools.json");
    //        InventoryBools data = JsonUtility.FromJson<InventoryBools>(json);

    //        _1 = data.skinArcFondo1;
    //        _2 = data.skinArcFondo2;
    //        _3 = data.skinArcBall1;
    //        _4 = data.skinArcBall2;
    //        _5 = data.skinArcDiana1;
    //        _6 = data.skinArcDiana2;
    //        _7 = data.skinEscGuantes1;
    //        _8 = data.skinEscGuantes2;
    //    }
    //    else
    //    {
    //        _1 = false;
    //        _2 = false;
    //        _3 = false;
    //        _4 = false;
    //        _5 = false;
    //        _6 = false;
    //        _7 = false;
    //        _8 = false;
    //    }
    //}

    //public bool CheckForID(int iD)
    //{
    //    if (iD == 1)
    //    {
    //        return _1;
    //    }
    //    else if (iD == 2)
    //    {
    //        return _2;
    //    }
    //    else if (iD == 3)
    //    {
    //        return _3;
    //    }
    //    else if (iD == 4)
    //    {
    //        return _4;
    //    }
    //    else if (iD == 5)
    //    {
    //        return _5;

    //    }
    //    else if (iD == 6)
    //    {
    //        return _6;

    //    }
    //    else if (iD == 7)
    //    {
    //        return _7;

    //    }
    //    else if (iD == 8)
    //    {
    //        return _8;
    //    }
    //    else return false;
    //} 
    #endregion


}
