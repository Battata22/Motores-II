using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Momentaneo2Elretorno : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI estrellas;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        estrellas.text = (PointsManager.Instance._points.ToString());
    }
}
