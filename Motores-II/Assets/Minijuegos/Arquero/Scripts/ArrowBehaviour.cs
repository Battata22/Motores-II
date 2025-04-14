using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{

    [SerializeField] Rigidbody rb;
    [SerializeField] float fuerzaDisparo, fuerzaDisparoVert, ayuda;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //rb.AddForce(-transform.forward * fuerzaDisparo/* + transform.up * fuerzaDisparoVert*/, ForceMode.Impulse);

        ShootArrow(InputManager.lastCarga);
    }

    public void ShootArrow(float fuerza)
    {
        rb.AddForce(-transform.forward * fuerza * ayuda, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }

}
