using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaivour : MonoBehaviour
{
    public static PlayerBehaivour Instance;

    //----------------------MVC---------------------
    Model_Player _model;
    View_Player _view;
    Controller_Player _controller;

    //----------------------Movimiento---------------------
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _speed = 10;

    //----------------------Sonidos---------------------
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _jumpClip;

    //----------------------Animaciones---------------------
    [SerializeField] Animator _animController;
    [SerializeField] Renderer _render;

    //----------------------Particulas---------------------
    [SerializeField] ParticleSystem _particleSystem;

    //----------------------Gyroscopio---------------------
    Gyroscope _gyro;
    [SerializeField] float _gyroSpeed;
    [SerializeField] float _jumpForce;
    bool _tengoGyro = false;
    public bool _onFloor = false;

    //----------------Hacer con observer-------------
    //[SerializeField] TextMeshProUGUI _textDistancia;
    //---------------modificar para que sea tiempo y no metros

    private void Awake()
    {
        Instance = this;

        //Primero consigo mis cosas antes de pasarlas
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _rb = GetComponent<Rigidbody>();
        _render = GetComponent<Renderer>();
        _speed = RemoteConfigManager.Instance.runner_speed;

        _model = new(_speed, _jumpForce, _rb, _render, _audioSource, _jumpClip, _gyro, _gyroSpeed, _tengoGyro, _onFloor);
        _controller = new(_model);
        _view = new(_model);
    }

    void Start()
    {
        _model.FakeStart();

        #region Old
        //_rb = GetComponent<Rigidbody>();

        #region Gyroscopio
        //if (SystemInfo.supportsGyroscope)
        //{
        //    tengoGyro = true;
        //    _gyro = Input.gyro;
        //    _gyro.enabled = true;
        //}
        //else
        //{
        //    Debug.LogError("No hay giroscopio");
        //}
        #endregion

        //_render = GetComponent<Renderer>();

        //PausaInGame.Instance.Paused += PausaRB;
        //PausaInGame.Instance.Despaused += DespausaRB; 
        #endregion
    }

    private void Update()
    {
        //---------------------OBSERVER-------------------------------
        //_textDistancia.text = ((int)transform.position.z).ToString() + " M";

        if (!PausaInGame.Instance.isPaused)
        {
            _model.FakeUpdate();
            _view.FakeUpdate();
            _controller.FakeUpdate();

            #region Old
            //Jump();
            //JumpAcelerometro();
            //JumpAcelerometroPC();
            //MovementPC();

            #endregion
        }
    }

    private void FixedUpdate()
    {
        if (!PausaInGame.Instance.isPaused)
        {
            _model.FakeFixedUpdate();
            _controller.FakeFixedUpdate();

            #region Old
            //Foward();
            //GyroMovement();
            //MovementPC(); 
            #endregion
        }
    }


    #region Old
    //void Foward()
    //{
    //    transform.position += transform.forward * _speed * Time.fixedDeltaTime;
    //}

    //void GyroMovement()
    //{
    //    if (tengoGyro)
    //    {
    //        transform.position += new Vector3(_gyro.gravity.x, 0, 0) * _gyroSpeed * Time.deltaTime;
    //    }
    //}

    //void MovementPC()
    //{
    //    var xAxis = Input.GetAxisRaw("Horizontal");

    //    if (xAxis != 0)
    //    {
    //        transform.position += new Vector3(xAxis, 0, 0) * Time.deltaTime * 7;
    //    }
    //}

    //void Jump()
    //{
    //    if (tengoGyro && _onFloor == true && _gyro.gravity.y >= 0.1f)
    //    {
    //        _rb.AddForce(transform.up * _jumpForce * Time.deltaTime, ForceMode.Impulse);
    //        _onFloor = false;
    //    }
    //}

    //void JumpAcelerometro()
    //{
    //    if (Input.acceleration.y <= -1.2 && _onFloor == true)
    //    {
    //        _rb.AddForce(transform.up * _jumpForce * Time.deltaTime, ForceMode.Impulse);
    //        _onFloor = false;
    //        _audioSource.clip = jumpClip;
    //        _audioSource.Play();
    //    }
    //}

    //void JumpAcelerometroPC()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space) && _onFloor == true)
    //    {
    //        _rb.AddForce(transform.up * _jumpForce * Time.deltaTime, ForceMode.Impulse);
    //        _onFloor = false;
    //        _audioSource.clip = jumpClip;
    //        _audioSource.Play();
    //    } 
    //}


    //Vector3 savedVelocity;
    //Vector3 savedAngularVelocity;
    //void PausaRB()
    //{
    //    savedVelocity = _rb.velocity;
    //    savedAngularVelocity = _rb.angularVelocity;
    //    _rb.isKinematic = true;
    //}
    //void DespausaRB()
    //{
    //    _rb.isKinematic = false;
    //    _rb.AddForce(savedVelocity, ForceMode.VelocityChange);
    //    _rb.AddTorque(savedAngularVelocity, ForceMode.VelocityChange);
    //} 
    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        _model.FakeOnCollisionEnter(collision);

        #region Old
        //if (collision != null)
        //{
        //    _onFloor = true;
        //}

        //if (collision.gameObject.GetComponent<PlataformaScript>() != null)
        //{
        //    collision.gameObject.GetComponent<PlataformaScript>()._playerTouchThis = true;
        //} 
        #endregion
    }

    private void OnDestroy()
    {
        _model.FakeOnDestroy();

        #region Old
        //PausaInGame.Instance.Paused -= PausaRB;
        //PausaInGame.Instance.Despaused -= DespausaRB; 
        #endregion
    }
}

