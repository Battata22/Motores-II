using UnityEngine;

public class PlataformaScript : Rewind
{
    #region Intento de aviso
    //[SerializeField] Camera cam;
    //public Image _avisoPrefab, aviso;
    //public Canvas _canvas; 
    #endregion

    [SerializeField] float _speed;
    [SerializeField] float _returnTime;
    [SerializeField] SaltarinManager _spawnerScript;
    float waitReturn = 0;

    public bool _playerTouchThis = false;
    protected override void Start()
    {
        base.Start();
        #region Intento de aviso
        //cam = Camera.main;
        //aviso = Instantiate(_avisoPrefab, _canvas.transform);
        //aviso.gameObject.GetComponent<AvisoScript>()._plat = gameObject;
        //aviso.enabled = false; 
        #endregion
    }

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

        if (!PausaInGame.Instance.isPaused)
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
        }


        #region Old
        //waitReturn += Time.deltaTime;

        //if (_pool != null && waitReturn >= _returnTime)
        //{
        //    _pool.Return(this);
        //} 
        _speed = SaltarinManager.instance.PlatSpeed;
        #endregion

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
        //RemoveMe();
        //MementoManager.instance.AddMeRewind(this);
        _pool = pool;
        waitReturn = 0;
        _playerTouchThis = false;
    }

    public override void Save()
    {
        mementoState.Rec(transform.position, waitReturn, _playerTouchThis, gameObject.activeInHierarchy);
    }

    public override void Load()
    {
        if (!mementoState.IsRemember()) return;

        var remember = mementoState.Remember();

        transform.position = (Vector3)remember.parameters[0];
        waitReturn = (float)remember.parameters[1];
        _playerTouchThis = (bool)remember.parameters[2];
        gameObject.SetActive((bool)remember.parameters[3]);
    }

    public override void RemoveMe()
    {
        MementoManager.instance.QuitMeRewind(this);
    }

}
