using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Movimiento : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _speed;

    Gyroscope _gyro;
    [SerializeField] float _gyroSpeed;
    [SerializeField] float _jumpForce;
    bool tengoGyro = false;
    bool _onFloor = false;

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

    }

    private void Update()
    {
        //Jump();
        JumpAcelerometro();
    }

    private void FixedUpdate()
    {
        Foward();
        GyroMovement();
    }



    void Foward()
    {
        transform.position += transform.forward * _speed * Time.fixedDeltaTime;
    }

    void GyroMovement()
    {
        if (tengoGyro)
        {
            transform.position += new Vector3(_gyro.gravity.x, 0, 0) * _gyroSpeed * Time.fixedDeltaTime;
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
        if(Input.acceleration.y <= -1.2)
        {
            _rb.AddForce(transform.up * _jumpForce * Time.deltaTime, ForceMode.Impulse);
            _onFloor = false;
        }
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
