using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaivour : Rewind, IObservable_
{
    public static PlayerBehaivour Instance;

    #region Variables
    //----------------------MVC---------------------
    Model_Player _model;
    View_Player _view;
    Controller_Player _controller;

    //----------------------Movimiento---------------------
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _speed = 10;
    [SerializeField] bool Troncado = false;

    //----------------------Sonidos---------------------
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _jumpClip;

    //----------------------Animaciones---------------------
    [SerializeField] Animator _animController;
    [SerializeField] Renderer _render;
    [SerializeField] Animator _animator;

    //----------------------Particulas---------------------
    [SerializeField] ParticleSystem _particleSystem;

    //----------------------Gyroscopio---------------------
    Gyroscope _gyro;
    [SerializeField] float _gyroSpeed;
    [SerializeField] float _jumpForce;
    bool _tengoGyro = false;
    public bool _onFloor = false;

    //----------------------Obstaculos-------------------------
    [SerializeField] float troncoTime;
    public bool Hacked;
    [SerializeField] float hackedTime;
    float waitHacked;

    //----------------Hacer con observer-------------
    //[SerializeField] TextMeshProUGUI _textDistancia;
    //---------------modificar para que sea tiempo y no metros

    public int Saltadas;
    List<IObserver_> _observers = new();
    public ScreenTest screenPaused;
    public bool activatedMultiply;

    public bool EasyMode;
    public Toggle toggleEasy;


    #endregion

    protected override void Awake()
    {
        base.Awake();

        Instance = this;

        //Primero consigo mis cosas antes de pasarlas
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _animator = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody>();
        _render = GetComponent<Renderer>();
        _speed = RemoteConfigManager.Instance.runner_speed;

        _model = new(_speed, _jumpForce, _rb, _render, _audioSource, _jumpClip, _gyro, _gyroSpeed, _tengoGyro, _onFloor, _animator);
        _controller = new(_model);
        _view = new(_model);
    }

    protected override void Start()
    {
        base.Start();

        _model.FakeStart();

        foreach (var observer in _observers)
        {
            observer.Notify(Saltadas);
        }

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
        EasyMode = toggleEasy.isOn;

        waitHacked += Time.deltaTime;
        if (waitHacked > hackedTime && Hacked)
        {
            Hacked = false;
        }

        //---------------------OBSERVER-------------------------------
        //_textDistancia.text = ((int)transform.position.z).ToString() + " M";

        if (!PausaInGame.Instance.isPaused && !Troncado)
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

        //---------------------------------------------------Screen Manager-----------------------------------------
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ScreenManager.instance.IsPausedActive)
                ScreenManager.instance.DesactiveScreen();
            else
                ScreenManager.instance.ActiveScreen(screenPaused);
        }
    }

    private void FixedUpdate()
    {
        if (!PausaInGame.Instance.isPaused && !Troncado)
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

    public void Hack()
    {
        waitHacked = 0;
        Hacked = true;
    }

    #region Memento
    public override void Save()
    {
        //parametros para pasar, pos, onfloor?
        mementoState.Rec(transform.position, _onFloor, Saltadas);
    }

    public override void Load()
    {
        if (!mementoState.IsRemember()) return;

        var remember = mementoState.Remember();

        transform.position = (Vector3)remember.parameters[0];
        _onFloor = (bool)remember.parameters[1];
        Saltadas = (int)remember.parameters[2];
    }

    public override void RemoveMe()
    {
        //MementoManager.instance.QuitMeRewind(this);
    }

    #endregion

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

    public IEnumerator Tronquear()
    {
        Troncado = true;
        yield return new WaitForSeconds(troncoTime);
        Troncado = false;
    }

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

    private void OnTriggerEnter(Collider other)
    {
        _model.FakeOnTriggerEnter(other);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _model.FakeOnDestroy();

        #region Old
        //PausaInGame.Instance.Paused -= PausaRB;
        //PausaInGame.Instance.Despaused -= DespausaRB; 
        #endregion
    }

    public void NotifyObserver()
    {
        foreach (var observer in _observers)
        {
            observer.Notify(Saltadas);
        }
    }

    public void Suscribe(IObserver_ observer)
    {
        if (_observers.Contains(observer)) return;

        _observers.Add(observer);
    }

    public void Unsuscribe(IObserver_ observer)
    {
        if (_observers.Contains(observer))
        {
            _observers.Remove(observer);
        }
    }
}

