using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoloParaEsto : MonoBehaviour
{
    [SerializeField] CanvasConfirmacion _canvas;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _canvas.GetComponent<Canvas>().enabled = true;
        }
    }
}
