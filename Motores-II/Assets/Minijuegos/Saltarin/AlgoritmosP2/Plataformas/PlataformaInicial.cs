using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaInicial : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerBehaivour>() != null)
        {
            SaltarinManager.instance.ActivarTrigger();
            Destroy(gameObject);
        }
    }
}
