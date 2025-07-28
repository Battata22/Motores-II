using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{

    [SerializeField] Rigidbody rb;
    [SerializeField] float fuerzaDisparo, fuerzaDisparoVert, ayuda;
    [SerializeField] float _speedTiny;
    [SerializeField] float _puntMulti;
    [SerializeField] float _velGiro;
    float wait;

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

        //Destroy(gameObject, 3);

        PausaInGame.Instance.Paused += PauseRB;
        PausaInGame.Instance.Despaused += DespauseRB;
    }

    void DestroyMe()
    {
        wait += Time.deltaTime;

        if (wait >= 3)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (!PausaInGame.Instance.isPaused)
        {
            DestroyMe();
            BecomeTiny();
            Girar();
        }
        //else if (PausaInGame.Instance.isPaused)
        //{
            
        //}
    }

    Vector3 savedVelocity;
    Vector3 savedAngularVelocity;
    void PauseRB()
    {
        savedVelocity = rb.velocity;
        savedAngularVelocity = rb.angularVelocity;
        rb.isKinematic = true;
    }
    void DespauseRB()
    {
        rb.isKinematic = false;
        rb.AddForce(savedVelocity, ForceMode.VelocityChange);
        rb.AddTorque(savedAngularVelocity, ForceMode.VelocityChange);
    }

    void Girar()
    {
        transform.Rotate(new Vector3(0, 0, _velGiro));
    }

    public void ShootArrow(float fuerza)
    {
        rb.AddForce(-transform.forward * fuerza * ayuda, ForceMode.Impulse);
    }

    void BecomeTiny()
    {
        transform.localScale -= transform.localScale * Time.deltaTime * -transform.position.z * 0.1f /*_speedTiny*/;
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

        ArqueroManager.instance.Hit();
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        PausaInGame.Instance.Paused -= PauseRB;
        PausaInGame.Instance.Despaused -= DespauseRB;
    }

}
