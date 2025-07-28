using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NieblaBehaivour : MonoBehaviour
{
    [SerializeField] float _duracion;
    [SerializeField, Range(0, 1)] float _speed;
    [SerializeField] Material _material;
    float wait;
    bool on = false;

    private void Start()
    {
        _material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (on)
        {
            wait += Time.deltaTime;

            if (wait >= _duracion)
            {
                _material.color -= new Color(0, 0, 0, _speed * Time.deltaTime);

                if (_material.color.a <= 0)
                {
                    TurnOff();
                }
            }
        }
    }

    public void TurnOn()
    {
        _material.color = new Color(0.6f, 0.6f, 0.6f, 0.93f);
        wait = 0;
        on = true;
    }

    public void TurnOff()
    {
        _material.color = new Color(0.6f, 0.6f, 0.6f, 0);
        on = false;
    }
}
