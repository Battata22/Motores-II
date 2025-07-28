using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoQuieroNiUnaPalabra : MonoBehaviour
{
    [SerializeField] Toggle toggle;

    private void Start()
    {
        toggle.isOn = AAAAAAAAAA.instance.EasyMode;
    }

    void Update()
    {
        AAAAAAAAAA.instance.EasyMode = toggle.isOn;
    }
}
