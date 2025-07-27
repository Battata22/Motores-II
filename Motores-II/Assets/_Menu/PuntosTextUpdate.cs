using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PuntosTextUpdate : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textPuntos;
    void Start()
    {
        _textPuntos = GetComponent<TextMeshProUGUI>();
        //por ahora lo pongo aca pero cuando se puedan gastar abria que actualizarlo en ese momento tambien
        _textPuntos.text = ("Puntos: " + PointsManager.Instance._points);
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            PointsManager.Instance.AddPoints(11);
            _textPuntos.text = ("Puntos: " + PointsManager.Instance._points);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            PointsManager.Instance.SubstractPoints(1000000000);
            _textPuntos.text = ("Puntos: " + PointsManager.Instance._points);
        }
    }
}
