using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAAAAAAAAA : MonoBehaviour
{

    public bool EasyMode;

    public static AAAAAAAAAA instance;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
}
