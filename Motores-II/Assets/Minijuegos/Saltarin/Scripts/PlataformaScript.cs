using UnityEngine;

public class PlataformaScript : MonoBehaviour
{
    #region Intento de aviso
    //[SerializeField] Camera cam;
    //public Image _avisoPrefab, aviso;
    //public Canvas _canvas; 
    #endregion

    [SerializeField] float _speed;
    [SerializeField] float _returnTime;
    float waitReturn = 0;

    public bool _playerTouchThis = false;
    void Start()
    {
        #region Intento de aviso
        //cam = Camera.main;
        //aviso = Instantiate(_avisoPrefab, _canvas.transform);
        //aviso.gameObject.GetComponent<AvisoScript>()._plat = gameObject;
        //aviso.enabled = false; 
        #endregion
    }

    public bool este;
    float w;
    void Update()
    {
        #region Intento de aviso
        //  if (cam.WorldToViewportPoint(transform.position).x > 0 && cam.WorldToViewportPoint(transform.position).x < 1 &&
        //cam.WorldToViewportPoint(transform.position).y > 0 && cam.WorldToViewportPoint(transform.position).y < 1 &&
        //cam.WorldToViewportPoint(transform.position).z > 0)
        //  {
        //      //print("estoy en camara");
        //      if (aviso.enabled == true)
        //      {
        //          aviso.enabled = false;
        //      }
        //  }
        //  else
        //  {
        //      //print("no estoy en camara :(");
        //      if (aviso.enabled == false)
        //      {
        //          aviso.enabled = true;
        //      }
        //      aviso.rectTransform.position = cam.WorldToViewportPoint(transform.position);
        //  } 
        #endregion

        if (este)
        {
            //hacer que no empiecen a spawnear hasta que saltas por primera vez
            //hacer que esta plataforma se destruya al toque

            //apagar el script del spawner por defecto y prenderlo cuando apretes el espacio/saltes con el celu
            w += Time.deltaTime;
            if (w >= 3)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Movement();
        }


        if (_playerTouchThis == true)
        {
            waitReturn += Time.deltaTime;

            if (_pool != null)
            {
                if (waitReturn >= _returnTime)
                {
                    _pool.Return(this);
                }
            }
            else
            {
                Destroy(gameObject);
            }

            #region Old
            //waitReturn += Time.deltaTime;

            //if (_pool != null && waitReturn >= _returnTime)
            //{
            //    _pool.Return(this);
            //} 
            #endregion
        }
    }

    public void LastStep()
    {
        if (SaltarinManager.instance.LastStep != this)
        {
            SaltarinManager.instance.LastStep = this;
        }
    }

    void Movement()
    {
        transform.position -= (new Vector3(0, 0, _speed) * Time.deltaTime);
    }

    public ObjectPool<PlataformaScript> _pool;
    public virtual void Recicle(ObjectPool<PlataformaScript> pool)
    {
        _pool = pool;
        waitReturn = 0;
        _playerTouchThis = false;
    }
}
