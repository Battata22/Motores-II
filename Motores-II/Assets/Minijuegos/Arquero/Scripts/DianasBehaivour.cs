using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DianasBehaivour : MonoBehaviour
{
    float wait;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (!PausaInGame.Instance.isPaused)
        {
            DestroyMe();
        }
    }

    void DestroyMe()
    {
        wait += Time.deltaTime;

        if (wait >= 3)
        {
            Destroy(gameObject);
        }
    }
}
