using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{

    [SerializeField] Rigidbody rb;
    [SerializeField] float fuerzaDisparo, fuerzaDisparoVert, ayuda;
    [SerializeField] float _speedTiny;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        ShootArrow(InputManager.lastCarga);
    }

    private void Update()
    {
        BecomeTiny();
    }

    public void ShootArrow(float fuerza)
    {
        rb.AddForce(-transform.forward * fuerza * ayuda, ForceMode.Impulse);
    }

    void BecomeTiny()
    {
        transform.localScale -= transform.localScale * Time.deltaTime * _speedTiny;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }

}
