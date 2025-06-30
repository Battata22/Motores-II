using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlataformaScript : MonoBehaviour
{
    [SerializeField] Camera cam;
    public Image _avisoPrefab, aviso;
    public Canvas _canvas;
    public bool _playerTouchThis = false;
    void Start()
    {
        cam = Camera.main;
        aviso = Instantiate(_avisoPrefab, _canvas.transform);
        aviso.gameObject.GetComponent<AvisoScript>()._plat = gameObject;
        aviso.enabled = false;
    }


    void Update()
    {
      //  if (cam.WorldToViewportPoint(transform.position).x > 0 && cam.WorldToViewportPoint(transform.position).x < 1 &&
      //cam.WorldToViewportPoint(transform.position).y > 0 && cam.WorldToViewportPoint(transform.position).y < 1 &&
      //cam.WorldToViewportPoint(transform.position).z > 0)
      //  {
      //      //print("estoy en camara");
      //      if (aviso.enabled == true)
      //      {
      //          aviso.enabled = false;
      //      }
      //  }
      //  else
      //  {
      //      //print("no estoy en camara :(");
      //      if (aviso.enabled == false)
      //      {
      //          aviso.enabled = true;
      //      }
      //      aviso.rectTransform.position = cam.WorldToViewportPoint(transform.position);
      //  }

        if (_playerTouchThis == true)
        {
            //Destroy(aviso.gameObject);
            Destroy(gameObject, 3);
        }
    }
}
