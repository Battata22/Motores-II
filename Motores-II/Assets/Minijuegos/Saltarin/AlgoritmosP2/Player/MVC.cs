using UnityEngine;

public class MVC : MonoBehaviour
{

}

public class Model_Player
{
    //ParticleSystem _partSalto;

    public float _speed;
    public float _jumpForce;
    public Rigidbody _rb;
    public Renderer _renderer;
    public AudioSource _audioSource;
    public AudioClip _jumpClip;


    public Gyroscope _gyro;
    public float _gyroSpeed;
    public bool _tengoGyro = false;
    public bool _onFloor = false;
    public Animator _animator;

    public float _lastY, _actualY;

    public Model_Player(float Speed, float JumpForce, Rigidbody RB, Renderer Renderer, AudioSource AudioSource,
        AudioClip JumpClip, Gyroscope Gyro, float GyroSpeed, bool TengoGyro, bool OnFloor, Animator animator)
    {
        _speed = Speed;
        _jumpForce = JumpForce;
        _rb = RB;
        _renderer = Renderer;
        _audioSource = AudioSource;
        _jumpClip = JumpClip;
        _gyro = Gyro;
        _gyroSpeed = GyroSpeed;
        _tengoGyro = TengoGyro;
        _onFloor = OnFloor;
        _animator = animator;
    }

    public void FakeStart()
    {
        PausaInGame.Instance.Paused += PausaRB;
        PausaInGame.Instance.Despaused += DespausaRB;

        #region Gyroscopio
        //Para que SOLO se ejecute en android y no en unity
#if UNITY_ANDROID && !UNITY_EDITOR
        if (SystemInfo.supportsGyroscope)
        {
            _tengoGyro = true;
            _gyro = Input.gyro;
            _gyro.enabled = true;
        }
        else
        {
            Debug.LogError("No hay giroscopio");
        }
#endif
        #endregion
    }

    public void FakeUpdate()
    {
        //---------------------------------------------Memento-----------------------------------------------
        if (MementoManager.instance.finishLoad)
        {
            DespausaRB();
        }
        else
        {
            PausaRB();
        }
    }

    public void FakeFixedUpdate()
    {

    }

    public void FakeOnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            _onFloor = true;
            PlayerBehaivour.Instance._onFloor = _onFloor;
        }

        if (collision.gameObject.GetComponent<PlataformaScript>() != null)
        {
            collision.gameObject.GetComponent<PlataformaScript>()._playerTouchThis = true;
            collision.gameObject.GetComponent<PlataformaScript>().LastStep();
            if (PlayerBehaivour.Instance.activatedMultiply)
            {
                PlayerBehaivour.Instance.Saltadas++;
                PlayerBehaivour.Instance.Saltadas++;
            }
            else
            {
                PlayerBehaivour.Instance.Saltadas++;
            }
            PlayerBehaivour.Instance.NotifyObserver();
        }

    }

    public void FakeOnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "TechoTrigger")
        {
            SaltarinManager.instance.TriggerStep += RunningAnimation;
            other.gameObject.SetActive(false);
        }
    }



    public void FakeOnDestroy()
    {
        PausaInGame.Instance.Paused -= PausaRB;
        PausaInGame.Instance.Despaused -= DespausaRB;
    }

    public void Foward()
    {
        PlayerBehaivour.Instance.transform.position += PlayerBehaivour.Instance.transform.forward * _speed * Time.fixedDeltaTime;
    }

    public void GyroMovement()
    {
        PlayerBehaivour.Instance.transform.position += new Vector3(_gyro.gravity.x, 0, 0) * _gyroSpeed * Time.deltaTime;
    }

    public void HackedGyroMovement()
    {
        PlayerBehaivour.Instance.transform.position += new Vector3(-_gyro.gravity.x, 0, 0) * _gyroSpeed * Time.deltaTime;
    }

    public void MovementPC()
    {
        var xAxis = Input.GetAxisRaw("Horizontal");

        PlayerBehaivour.Instance.transform.position += new Vector3(xAxis, 0, 0) * Time.deltaTime * 7;
    }

    public void HackedMovementPC()
    {
        var xAxis = Input.GetAxisRaw("Horizontal");

        PlayerBehaivour.Instance.transform.position += new Vector3(-xAxis, 0, 0) * Time.deltaTime * 7;
    }

    public void JumpAcelerometro()
    {
        _rb.AddForce(PlayerBehaivour.Instance.transform.up * _jumpForce * Time.deltaTime, ForceMode.Impulse);
        _onFloor = false;
        PlayerBehaivour.Instance._onFloor = _onFloor;
        _lastY = 1000;
    }

    public void JumpAcelerometroPC()
    {
        //Debug.Log("jump");
        _rb.AddForce(PlayerBehaivour.Instance.transform.up * _jumpForce * 1.1f * Time.deltaTime, ForceMode.Impulse);
        _onFloor = false;
        PlayerBehaivour.Instance._onFloor = _onFloor;
        _lastY = 1000;
    }

    public void JumpSound()
    {
        _audioSource.clip = _jumpClip;
        _audioSource.Play();
    }

    public void JumpAnimation()
    {
        SetFalseAnims();
        _animator.SetBool("Jumping", true);
    }

    public void FallingAnimation()
    {
        SetFalseAnims();
        _animator.SetBool("Falling", true);
    }

    public void SetFalseFallingAnimation()
    {
        _animator.SetBool("Falling", false);
    }

    public void RunningAnimation()
    {
        SetFalseAnims();
        _animator.SetBool("Running", true);
    }

    public void SetFalseAnims()
    {
        _animator.SetBool("Jumping", false);
        _animator.SetBool("Falling", false);
        _animator.SetBool("Running", false);
    }

    public void ChangeColor(Color color)
    {
        //_renderer.material.color = color;
    }

    //--------------------Rigidbody---------------------

    public Vector3 savedVelocity;
    public Vector3 savedAngularVelocity;

    public void PausaRB()
    {
        savedVelocity = _rb.velocity;
        savedAngularVelocity = _rb.angularVelocity;
        _rb.isKinematic = true;
    }
    public void DespausaRB()
    {
        _rb.isKinematic = false;
        _rb.AddForce(savedVelocity, ForceMode.VelocityChange);
        _rb.AddTorque(savedAngularVelocity, ForceMode.VelocityChange);
    }

}
public class Controller_Player
{
    //----------------------MVC--------------------
    Model_Player _model;

    public Controller_Player(Model_Player model)
    {
        _model = model;
    }

    public void FakeUpdate()
    {
        CheckJumpAce();

        CheckJumpAcePC();

        //CheckFalling();
    }
    public void FakeFixedUpdate()
    {
        //_model.Foward();

        if (!PlayerBehaivour.Instance.Hacked)
        {
            CheckGyro();

            CheckMovementPC();
        }
        else
        {
            CheckHackedGyro();

            CheckHackedMovementPC();
        }
    }

    void CheckFalling()
    {
        _model._actualY = PlayerBehaivour.Instance.transform.position.y;

        if (_model._actualY < _model._lastY && _model._animator.GetBool("Falling") == false)
        {
            _model._lastY = PlayerBehaivour.Instance.transform.position.y;
            _model.FallingAnimation();
        }
        //else
        //{
        //    _model.SetFalseFallingAnimation();
        //}
    }

    void CheckMovementPC()
    {
        var xAxis = Input.GetAxisRaw("Horizontal");

        if (xAxis != 0)
        {
            _model.MovementPC();
        }
    }

    void CheckHackedMovementPC()
    {
        var xAxis = Input.GetAxisRaw("Horizontal");

        if (xAxis != 0)
        {
            _model.HackedMovementPC();
        }
    }

    void CheckJumpAce()
    {
        if (Input.acceleration.y <= -1.2 && _model._onFloor == true)
        {
            _model.JumpAcelerometro();
        }
    }

    void CheckJumpAcePC()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _model._onFloor == true)
        {
            _model.JumpAcelerometroPC();
        }
    }

    void CheckGyro()
    {
        if (_model._tengoGyro)
        {
            _model.GyroMovement();
        }
    }

    void CheckHackedGyro()
    {
        if (_model._tengoGyro)
        {
            _model.HackedGyroMovement();
        }
    }
}

public class View_Player
{
    //----------------------MVC--------------------
    Model_Player _model;

    public View_Player(Model_Player model)
    {
        _model = model;
    }

    public void FakeUpdate()
    {
        CheckFloor();

        CheckJump();

        CheckJumpPC();
    }

    public void Move()
    {
        //Debug.Log("Me muevo");
    }

    void CheckFloor()
    {
        if (_model._onFloor == true)
        {
            _model.ChangeColor(Color.blue);
        }
        else
        {
            _model.ChangeColor(Color.red);
        }
    }

    void CheckJump()
    {
        if (Input.acceleration.y <= -1.2 && _model._onFloor == true)
        {
            _model.JumpSound();
            _model.JumpAnimation();
        }
    }

    void CheckJumpPC()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _model._onFloor == true)
        {
            _model.JumpSound();
            _model.JumpAnimation();
        }
    }

}
