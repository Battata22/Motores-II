using System.Collections;
using System.Collections.Generic;
using Unity.Services.RemoteConfig;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{

    [SerializeField] Rigidbody rb;
    [SerializeField] float fuerzaDisparo, fuerzaDisparoVert, ayuda;
    [SerializeField] float _speedTiny;
    [SerializeField] float _puntMulti;

    //private void Awake()
    //{
    //    RemoteConfigManager.Instance.OnConfigFetched += SetData;
    //}

    //void SetData()
    //{
    //    _puntMulti = RemoteConfigService.Instance.appConfig.GetFloat("Archer_PuntMultiply");
    //}

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        _puntMulti = RemoteConfigManager.Instance._puntMulti;

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

    float CalcularPuntos(float z)
    {
        float res;

        res = z * _puntMulti;

        if (res < 0)
        {
            res = res * -1;
        }

        return res;
    }

    private void OnCollisionEnter(Collision collision)
    {
        PuntajeManager.instance.AddPoints(CalcularPuntos(transform.position.z));

        Destroy(collision.gameObject);
        Destroy(gameObject);
    }

}
