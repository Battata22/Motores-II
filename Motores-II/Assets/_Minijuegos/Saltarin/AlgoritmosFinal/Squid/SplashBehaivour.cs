using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashBehaivour : MonoBehaviour
{
    [SerializeField] Image _splash;
    [SerializeField, Range(0,1)] float _speed;
    bool on = false;
    void Start()
    {
        _splash = GetComponent<Image>();
    }

    
    void Update()
    {
        if (on)
        {
            _splash.color -= new Color(0, 0, 0, _speed * Time.deltaTime);

            if (_splash.color.a <= 0)
            {
                TurnOff();
            }
        }
    }

    public void TurnOn()
    {
        _splash.color = new Color(0, 0, 0, 1);
        on = true;
    }

    public void TurnOff()
    {
        _splash.color = new Color(0, 0, 0, 0);
        on = false;
    }
}
