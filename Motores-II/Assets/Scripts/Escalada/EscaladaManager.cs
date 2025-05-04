using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.RemoteConfig;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class EscaladaManager : MonoBehaviour
{
    [SerializeField] RocaSpawner[] _spawners;
    [SerializeField] PlayerEscalada _player;

    public event Action StepTrigger;
    int _maxHeight = 100000000;
    int _currentHeight;

    public event Action OnGameOver = delegate { };

    bool _gameStarted = false;

    public static EscaladaManager Instance;

    private void Awake()
    {
        Instance = this;

        Screen.orientation = ScreenOrientation.Portrait;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.autorotateToPortraitUpsideDown = false;

        QualitySettings.vSyncCount = 1;
    }

    private IEnumerator Start()
    {
        //RemoteConfigManager.Instance.OnConfigFetched += SetData;
        SetData();

        yield return new WaitForEndOfFrame();
        StartGame();
    }

    void SetData()
    {
        _maxHeight = RemoteConfigManager.Instance.Escalada_MaxHeight;
    }

    private void Update()
    {
        UpdatePlayerTime();

        //back to menu input
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!_gameStarted)
            {
                StartGame();
                return;
            }

            StepRock(0);
        }
    }

    private void LateUpdate()
    {
        if (_timeBar != null)
            UpdateTimeBar();
    }

    void StartGame()
    {
        _gameStarted = true;
        _time = _maxTime;

        for (int i = 0; i < 3; i++)
        {
            StepRock(0);
        }

        _currentHeight = 0;
    }

    public void StepRock(float x)
    {
        var num = UnityEngine.Random.Range(0, _spawners.Length+1);
        var num2 = UnityEngine.Random.Range(0, _spawners.Length+1);

        for (int i = 0; i < _spawners.Length; i++)
        {
            if (i == num || i == num2)
                _spawners[i].canSpawn = false;
            else
                _spawners[i].canSpawn = true;

            _spawners[i].StepRock();
        }

        StepTrigger();
        _player.SetPos(x);

        _time = _maxTime;
        _currentHeight++;

        UpdateCanvasText();

        if(_currentHeight >= _maxHeight)
        {
            EndGame(false);
        }
    }

    [SerializeField] Canvas _gameOverCanvas;
    [SerializeField] TMP_Text _gameOverText;
    [SerializeField] TMP_Text _heightText;
    [SerializeField] Image _timeBar;
    [SerializeField] float _maxTime;
    float _time;

    void UpdateTimeBar()
    {
        _timeBar.fillAmount = _time / _maxTime;
    }

    private void UpdatePlayerTime()
    {
        if (!_gameStarted) return;

        _time -= Time.deltaTime;
        if(_time < 0)
        {

            Debug.Log("Me gusta el pingo");
            EndGame(true);
        }
    }

    void UpdateCanvasText()
    {
        if (_heightText != null)
            _heightText.text = $"{_currentHeight} / {_maxHeight}";
    }

    public void EndGame(bool fail)
    {
        Debug.Log("Cortala eu");

        _gameStarted = false;

        //desaactivar ataques
        //desactivar rocas
        OnGameOver();

        _player.gameObject.SetActive(false);
        _heightText.gameObject.SetActive(false);

        //Activar canvas
        if (_gameOverCanvas != null)
            _gameOverCanvas.gameObject.SetActive(true);

        if (_gameOverText != null)
        {

            if (fail)
            {
                //Debug.Log("Sos un pete");
                _gameOverText.text = "You Lose";
                _gameOverText.color = Color.red;
            }
            else
            {
                //Debug.Log("Buena campeon");
                _gameOverText.text = "You Win";
                _gameOverText.color = Color.green;
            }
        }
    }
}
