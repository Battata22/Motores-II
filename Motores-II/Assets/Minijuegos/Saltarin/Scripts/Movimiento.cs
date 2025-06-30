using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movimiento : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textDistancia;
    [SerializeField] float _metrosParaVictoria;

    [SerializeField] Rigidbody _rb;
    [SerializeField] float _speed;

    Gyroscope _gyro;
    [SerializeField] float _gyroSpeed;
    [SerializeField] float _jumpForce;
    bool tengoGyro = false;
    bool _onFloor = false;

    [SerializeField] Renderer _render;

    [SerializeField] GameObject _imagenDer1, _imagenDer2, _imagenDer3;
    [SerializeField] GameObject _botonreiniciar, _botonVolver;
    [SerializeField] SpawnerPlataformas _spawnScript;
    [SerializeField] bool yaSalioVictoria = false;
    [SerializeField] bool _yaSumePuntos;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _errorClip, jumpClip;

    private void Start()
    {
        _yaSumePuntos = false;

        _rb = GetComponent<Rigidbody>();

        #region Gyroscopio
        if (SystemInfo.supportsGyroscope)
        {
            tengoGyro = true;
            _gyro = Input.gyro;
            _gyro.enabled = true;
        }
        else
        {
            Debug.LogError("No hay giroscopio");
        }
        #endregion

        _render = GetComponent<Renderer>();

        _speed = RemoteConfigManager.Instance.runner_speed;

        yaSalioVictoria = false;

        PausaInGame.Instance.Paused += PausaRB;
        PausaInGame.Instance.Despaused += DespausaRB;
    }


    Vector3 savedVelocity;
    Vector3 savedAngularVelocity;
    void PausaRB()
    {
        savedVelocity = _rb.velocity;
        savedAngularVelocity = _rb.angularVelocity;
        _rb.isKinematic = true;
    }
    void DespausaRB()
    {
        _rb.isKinematic = false;
        _rb.AddForce(savedVelocity, ForceMode.VelocityChange);
        _rb.AddTorque(savedAngularVelocity, ForceMode.VelocityChange);
    }

    private void Update()
    {
        if (!PausaInGame.Instance.isPaused)
        {
            _textDistancia.text = ((int)transform.position.z).ToString() + " M";

            if (transform.position.z >= _metrosParaVictoria && yaSalioVictoria == false && _onFloor == true)
            {
                Victoria();
            }

            //Jump();
            JumpAcelerometro();
            JumpAcelerometroPC();

            //MovementPC();

            if (_onFloor == true)
            {
                _render.material.color = Color.blue;
            }
            else
            {
                _render.material.color = Color.red;
            }

            if (transform.position.y <= -0.5f)
            {
                Perdida();
            }
        }
        else
        {

        }       

    }

    private void FixedUpdate()
    {
        if (!PausaInGame.Instance.isPaused)
        {
            Foward();
            GyroMovement();
            MovementPC();
        }
        else
        {

        }
    }



    void Foward()
    {
        transform.position += transform.forward * _speed * Time.fixedDeltaTime;
    }

    void GyroMovement()
    {
        if (tengoGyro)
        {
            transform.position += new Vector3(_gyro.gravity.x, 0, 0) * _gyroSpeed * Time.deltaTime;
        }
    }

    void MovementPC()
    {
        var xAxis = Input.GetAxisRaw("Horizontal");

        if (xAxis != 0)
        {
            transform.position += new Vector3(xAxis, 0, 0) * Time.deltaTime * 7;
        }
    }

    void Jump()
    {
        if (tengoGyro && _onFloor == true && _gyro.gravity.y >= 0.1f)
        {
            _rb.AddForce(transform.up * _jumpForce * Time.deltaTime, ForceMode.Impulse);
            _onFloor = false;
        }
    }

    void JumpAcelerometro()
    {
        //Vector3 accelerometerFixed = Quaternion.Euler(90, 0, 0) * Input.acceleration;
        //rb.AddForce(accelerometerFixed * force);
        if (Input.acceleration.y <= -1.2 && _onFloor == true)
        {
            _rb.AddForce(transform.up * _jumpForce * Time.deltaTime, ForceMode.Impulse);
            _onFloor = false;
            _audioSource.clip = jumpClip;
            _audioSource.Play();
        }
    }

    void JumpAcelerometroPC()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _onFloor == true)
        {
            _rb.AddForce(transform.up * _jumpForce * Time.deltaTime, ForceMode.Impulse);
            _onFloor = false;
            _audioSource.clip = jumpClip;
            _audioSource.Play();
        }
    }

    void Perdida()
    {
        _imagenDer1.SetActive(true);
        _imagenDer2.SetActive(true);
        _imagenDer3.SetActive(true);
        _botonreiniciar.SetActive(true);
        _botonVolver.SetActive(true);

        gameObject.SetActive(false);
        _spawnScript.enabled = false;

        SumarPoints();

        StaminaSystem.Instance.UseStamina(StaminaSystem.Instance.gameStaminaCost);
    }

    void SumarPoints()
    {
        if (!_yaSumePuntos)
        {
            int puntos = (int)transform.position.z / 10;
            PointsManager.Instance.AddPoints(puntos);
            _yaSumePuntos = true;
        }
    }

    [SerializeField] GameObject victoria;
    [SerializeField] GameObject boton1, boton2, boton3;
    void Victoria()
    {
        victoria.SetActive(true);
        boton1.SetActive(true);
        boton2.SetActive(true);
        boton3.SetActive(true);
        yaSalioVictoria = true;

        gameObject.SetActive(false);
        _spawnScript.enabled = false;

        SumarPoints();

        StaminaSystem.Instance.UseStamina(StaminaSystem.Instance.gameStaminaCost);
    }

    public void ResetScene()
    {
        if (StaminaSystem.Instance.CurrentStamina >= StaminaSystem.Instance.gameStaminaCost)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            _audioSource.clip = _errorClip;
            _audioSource.Play();
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            _onFloor = true;
        }

        if (collision.gameObject.GetComponent<PlataformaScript>() != null)
        {
            collision.gameObject.GetComponent<PlataformaScript>()._playerTouchThis = true;
        }
    }

    private void OnDestroy()
    {
        PausaInGame.Instance.Paused -= PausaRB;
        PausaInGame.Instance.Despaused -= DespausaRB;
    }









    //private void Start()
    //{
    //    if (SystemInfo.supportsAccelerometer)
    //    {
    //        acelerometro = true;
    //    }
    //    else
    //    {
    //        Debug.LogError("No hay acelerometro");
    //    }
    //}

    //private void Update()
    //{
    //    if (acelerometro == true)
    //    {
    //        MovimientoConAcelerometro();
    //    }
    //}

    //public void MovimientoConAcelerometro()
    //{
    //    Vector3 accelerometerFixed = Quaternion.Euler(90, 0, 0) * Input.acceleration;
    //    Vector3 
    //    rb.AddForce(accelerometerFixed * _fuerza);
    //}
}
