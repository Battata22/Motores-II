using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseCondition : MonoBehaviour
{
    [SerializeField] PlayerBehaivour _playerScript;
    [SerializeField] SpawnerPlataformas _plataformasSpawner;
    [SerializeField] IScreen _canvasDerrota, _canvasVictoria;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _errorClip;
    [SerializeField] float _metrosParaVictoria;
    [SerializeField] float _distParaPerder;
    [SerializeField] bool yaSalioVictoria = false;
    [SerializeField] bool _yaSumePuntos;

    void Start()
    {
        _playerScript = PlayerBehaivour.Instance;

        _yaSumePuntos = false;

        yaSalioVictoria = false;

        _canvasDerrota = ScreenManager.instance.CanvasDerrota;
        _canvasVictoria = ScreenManager.instance.CanvasVictory;
    }


    void Update()
    {
        //---------------------CAMBIAR A SISTEMA DE TIEMPO--------------------------------
        if (transform.position.z >= _metrosParaVictoria && yaSalioVictoria == false && _playerScript._onFloor == true)
        {
            Victoria();
        }

        if (transform.position.y <= -_distParaPerder)
        {
            Perdida();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Victoria();
        }
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

    void Perdida()
    {
        //_canvasDerrota.enabled = true;
        ScreenManager.instance.ActiveScreen(_canvasDerrota);


        gameObject.SetActive(false);
        //CAMBIAR A LAS PLATAFORMAS NUEVAS
        _plataformasSpawner.enabled = false;

        SumarPoints();

        StaminaSystem.Instance.UseStamina(StaminaSystem.Instance.gameStaminaCost);
    }

    void Victoria()
    {
        //_canvasVictoria.enabled = true;
        ScreenManager.instance.ActiveScreen(_canvasVictoria);
        yaSalioVictoria = true;

        gameObject.SetActive(false);
        //CAMBIAR A LAS PLATAFORMAS NUEVAS
        _plataformasSpawner.enabled = false;

        SumarPoints();

        StaminaSystem.Instance.UseStamina(StaminaSystem.Instance.gameStaminaCost);
    }

    void SumarPoints()
    {
        if (!_yaSumePuntos)
        {
            //CAMBIAR SISTEMA DE PUNTOS A TIEMPO
            int puntos = (int)transform.position.z / 10;
            PointsManager.Instance.AddPoints(puntos);
            _yaSumePuntos = true;
        }
    }
}
