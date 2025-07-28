using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasConfirmacion : MonoBehaviour
{
    [SerializeField] Canvas _myCanvas;
    [SerializeField] CompraItem compraItemScript;

    void Start()
    {
        _myCanvas = GetComponent<Canvas>();
    }

    public void SiBoton()
    {
        SceneLoaderManager.instance.SceneToLoad = 2;
    }

    public void NoBoton()
    {
        _myCanvas.enabled = false;
    }

    public void CerrarApp()
    {
        Application.Quit();
    }

    public void SetCompraItemScript(CompraItem compraItemScript1)
    {
        compraItemScript = compraItemScript1;
    }

    public void TouchItem()
    {
        if (compraItemScript.bloqueado && PointsManager.Instance._points >= int.Parse(compraItemScript.itemScript.itemPrice.text))
        {
            compraItemScript.bloqueado = false;
            PointsManager.Instance.SubstractPoints(int.Parse(compraItemScript.itemScript.itemPrice.text));
            compraItemScript.itemScript.locked.enabled = false;
            PlayerPrefs.SetInt(compraItemScript.itemScript.iD.ToString(), 1);
            PlayerPrefs.Save();
            compraItemScript.AudioSource.clip = compraItemScript.purchase;
            compraItemScript.AudioSource.Play();
        }
        else if (compraItemScript.bloqueado && PointsManager.Instance._points < int.Parse(compraItemScript.itemScript.itemPrice.text))
        {
            print("no tenes guita");
            compraItemScript.AudioSource.clip = compraItemScript.error;
            compraItemScript.AudioSource.Play();
        }

        if (!compraItemScript.bloqueado)
        {
            if (compraItemScript.itemScript.iD == 1)
            {
                PlayerPrefs.SetInt("SelectedFondo", 1);
            }
            else if (compraItemScript.itemScript.iD == 2)
            {
                PlayerPrefs.SetInt("SelectedFondo", 2);
            }
            else if (compraItemScript.itemScript.iD == 3)
            {
                PlayerPrefs.SetInt("SelectedBall", 3);
            }
            else if (compraItemScript.itemScript.iD == 4)
            {
                PlayerPrefs.SetInt("SelectedBall", 4);
            }
            else if (compraItemScript.itemScript.iD == 5)
            {
                PlayerPrefs.SetInt("SelectedDiana", 5);
            }
            else if (compraItemScript.itemScript.iD == 6)
            {
                PlayerPrefs.SetInt("SelectedDiana", 6);
            }
            else if (compraItemScript.itemScript.iD == 7)
            {
                PlayerPrefs.SetInt("SelectedGuantes", 7);
            }
            else if (compraItemScript.itemScript.iD == 8)
            {
                PlayerPrefs.SetInt("SelectedGuantes", 8);
            }
            compraItemScript.AudioSource.clip = compraItemScript.selected;
            compraItemScript.AudioSource.Play();
        }
    }
}
