using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField] GameObject imagen;
    [SerializeField] float carga, speedCarga, offset;
    [SerializeField] Slider Slider;
    [SerializeField] GameObject arrow;
    [SerializeField] Material _default, tennis, DB, selected;
    [SerializeField] AudioSource _audioSourceShoot;
    [SerializeField] AudioClip _shootClip;

    public static float lastCarga;


    void Start()
    {
        PausaInGame.Instance.Paused += Pausar;
        PausaInGame.Instance.Despaused += Despausar;

        if (PlayerPrefs.GetInt("SelectedBall") == 3)
        {
            selected = tennis;
        }
        else if (PlayerPrefs.GetInt("SelectedBall") == 4)
        {
            selected = DB;
        }
        else if (PlayerPrefs.GetInt("SelectedBall") == 0)
        {
            selected = _default;
        }
    }

    private void OnDestroy()
    {
        PausaInGame.Instance.Paused -= Pausar;
        PausaInGame.Instance.Despaused -= Despausar;
    }

    void Pausar()
    {
        enabled = false;
    }
    void Despausar()
    {
        enabled = true;
    }


    void Update()
    {
        if (!PausaInGame.Instance.isPaused)
        {
            //if (Input.touchCount < 1) return;
            //else
            //{
            //    var touch = Input.GetTouch(0);
            //}

            if (Input.GetMouseButton(0))
            {
                if (imagen.active == false) imagen.SetActive(true);

                var touch = Input.GetTouch(0);

                imagen.transform.position = new Vector3(touch.position.x - offset, touch.position.y);

                Cargador();

                lastCarga = carga;
            }

            if (Input.GetMouseButtonUp(0))
            {
                var touch = Input.GetTouch(0);

                imagen.SetActive(false);

                ResetCargador();

                ShootArrow(Camera.main.ScreenToWorldPoint(touch.position));
                _audioSourceShoot.clip = _shootClip;
                _audioSourceShoot.Play();
            }
        }

    }

    bool subiendo = true;
    void Cargador()
    {
        if (carga < Slider.maxValue && subiendo == true)
        {
            carga += Time.deltaTime * speedCarga;
        }
        else if (carga >= Slider.maxValue)
        {
            subiendo = false;
        }

        if (carga > Slider.minValue && subiendo == false)
        {
            carga -= Time.deltaTime * speedCarga;
        }
        else if (carga <= Slider.minValue)
        {
            subiendo = true;
        }

        Slider.value = carga;
    }

    void ResetCargador()
    {
        carga = 0;
        subiendo = true;
    }


    void ShootArrow(Vector3 shootPos)
    {
        var spawn = Instantiate(arrow, new Vector3(shootPos.x, shootPos.y, 0), Quaternion.identity);

        spawn.GetComponent<Renderer>().material = selected;
    }
}
