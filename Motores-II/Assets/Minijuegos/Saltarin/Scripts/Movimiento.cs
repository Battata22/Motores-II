using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
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
    }

    private void Update()
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

    private void FixedUpdate()
    {
        Foward();
        GyroMovement();
        MovementPC();
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
        if(Input.acceleration.y <= -1.2 && _onFloor == true)
        {
            _rb.AddForce(transform.up * _jumpForce * Time.deltaTime, ForceMode.Impulse);
            _onFloor = false;
        }
    }

    void JumpAcelerometroPC()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _onFloor == true)
        {
            _rb.AddForce(transform.up * _jumpForce * Time.deltaTime, ForceMode.Impulse);
            _onFloor = false;
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
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null)
        {
            _onFloor = true;
        }
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
