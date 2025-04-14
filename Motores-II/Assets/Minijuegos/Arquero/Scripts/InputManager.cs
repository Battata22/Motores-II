using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField] GameObject imagen;
    [SerializeField] float carga, speedCarga, offset;
    [SerializeField] Slider Slider;
    [SerializeField] GameObject arrow;

    public static float lastCarga;


    void Start()
    {
        
    }

    
    void Update()
    {
        //if (Input.touchCount < 1) return;
        //else
        //{
        //    var touch = Input.GetTouch(0);
        //}

        if (Input.GetMouseButton(0))
        {
            if (imagen.active == false) imagen.SetActive(true);

            var touch = Input.GetTouch(0);

            imagen.transform.position = new Vector3(touch.position.x - offset, touch.position.y);

            Cargador();

            lastCarga = carga;
        }

        if (Input.GetMouseButtonUp(0))
        {
            var touch = Input.GetTouch(0);

            imagen.SetActive(false);

            ResetCargador();

            ShootArrow(Camera.main.ScreenToWorldPoint(touch.position));

        }



    }

    bool subiendo = true;
    void Cargador()
    {
        if (carga < Slider.maxValue && subiendo == true)
        {
            carga += Time.deltaTime * speedCarga;
        }
        else if (carga >= Slider.maxValue)
        {
            subiendo = false;
        }

        if (carga > Slider.minValue && subiendo == false)
        {
            carga -= Time.deltaTime * speedCarga;
        }
        else if (carga <= Slider.minValue)
        {
            subiendo = true;
        }

        Slider.value = carga;
    }

    void ResetCargador()
    {
        carga = 0;
        subiendo = true;
    }


    void ShootArrow(Vector3 shootPos)
    {
        Instantiate(arrow, shootPos, Quaternion.identity);
    }
}
