using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class animTextoCargando : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textTMP;
    [SerializeField] float timer;
    float wait;
    int puntos = 0;
    
    void Start()
    {
        _textTMP = GetComponent<TextMeshProUGUI>();
    }

    
    void Update()
    {
        wait += Time.deltaTime;

        if (wait >= timer)
        {
            if (puntos == 0)
            {
                _textTMP.text = ("Cargando" + ".");
                puntos++;
            }
            else if (puntos == 1)
            {
                _textTMP.text = ("Cargando" + "..");
                puntos++;
            }
            else if (puntos == 2)
            {
                _textTMP.text = ("Cargando" + "...");
                puntos++;
            }
            else if (puntos == 3)
            {
                _textTMP.text = ("Cargando");
                puntos = 0;
            }
            else
            {
                puntos = 0;
            }

            wait = 0;
        }

    }
}
