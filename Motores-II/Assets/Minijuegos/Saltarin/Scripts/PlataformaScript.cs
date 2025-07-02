using UnityEngine;

public class PlataformaScript : MonoBehaviour
{
    #region Intento de aviso
    //[SerializeField] Camera cam;
    //public Image _avisoPrefab, aviso;
    //public Canvas _canvas; 
    #endregion

    float waitDeath = 0;

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

        if (_playerTouchThis == true)
        {
            //Destroy(gameObject, 3);

            waitDeath += Time.deltaTime;

            if (_pool != null)
            {
                _pool.Return(this);
            }
        }
    }

    public ObjectPool<PlataformaScript> _pool;
    public virtual void Recicle(ObjectPool<PlataformaScript> pool)
    {
        _pool = pool;
        waitDeath = 0;
        _playerTouchThis = false;
    }
}
