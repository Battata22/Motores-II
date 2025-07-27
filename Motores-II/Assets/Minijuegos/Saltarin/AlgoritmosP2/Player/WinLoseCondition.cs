using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseCondition : MonoBehaviour
{
    [SerializeField] PlayerBehaivour _playerScript;
    [SerializeField] SpawnerPlataformasPool _plataformasSpawner;
    [SerializeField] IScreen _canvasDerrota, _canvasVictoria;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _errorClip;
    [SerializeField] float _puntosParaVictoria;
    [SerializeField] float _distParaPerder;
    [SerializeField] bool yaSalioVictoria = false;
    [SerializeField] bool _yaSumePuntos;
    [SerializeField] AudioClip _winSound, _loseSound;
    [SerializeField] AudioSource _audioSourceWinLose;
 
    void Start()
    {
        _playerScript = PlayerBehaivour.Instance;

        _yaSumePuntos = false;

        yaSalioVictoria = false;

        _canvasDerrota = ScreenManager.instance.CanvasDerrota;
        _canvasVictoria = ScreenManager.instance.CanvasVictory;

        _audioSourceWinLose.clip = null;
    }


    void Update()
    {
        //---------------------CAMBIAR A SISTEMA DE TIEMPO--------------------------------
        if (PlayerBehaivour.Instance.Saltadas >= _puntosParaVictoria && yaSalioVictoria == false && _playerScript._onFloor == true)
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

        _audioSourceWinLose.clip = _loseSound;
        _audioSourceWinLose.Play();

        SumarPoints();

        StaminaSystem.Instance.UseStamina(StaminaSystem.Instance.gameStaminaCost);

        gameObject.SetActive(false);

    }

    void Victoria()
    {
        ScreenManager.instance.ActiveScreen(_canvasVictoria);
        yaSalioVictoria = true;

        _audioSourceWinLose.clip = _winSound;
        _audioSourceWinLose.Play();

        SumarPoints();

        StaminaSystem.Instance.UseStamina(StaminaSystem.Instance.gameStaminaCost);

        gameObject.SetActive(false);
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
