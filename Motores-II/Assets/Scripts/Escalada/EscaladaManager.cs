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

    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _hitClip;
    [SerializeField] AudioClip _winClip;
    [SerializeField] AudioClip _loseClip;

    public event Action StepTrigger;
    int _maxHeight = 100000000;
    int _currentHeight;

    public event Action OnGameOver = delegate { };

    bool _gameStarted = false;

    public static EscaladaManager Instance;


    bool _needTutorial = true;

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

        if(PlayerPrefs.HasKey("Escalada_NeedTutorial"))
        _needTutorial = PlayerPrefs.GetInt("Escalada_NeedTutorial") == 1 ? true :false;


        AdsManager.Instance.rewardedAds.OnRewardAddComplete += AdReward;


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
        //if (Input.GetKey(KeyCode.Escape))
        //{
        //    //SceneManager.LoadScene("Menu");
        //    OpenPause();
        //}


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

    //[SerializeField] public Canvas pauseMenu;
    //public bool paused;

    [SerializeField] Canvas _tutorialCanvas;
    bool _inTutorial = false;//a
    void StartTutorial()
    {
        _inTutorial = true;

        _tutorialCanvas.enabled = true;
        Time.timeScale = 0;


    }

    void EndTutorial()
    {
        _tutorialCanvas.enabled=false;
        Time.timeScale = 1;

        _needTutorial=false;

        _inTutorial = false;

        PlayerPrefs.SetInt("Escalada_NeedTutorial", 0);
    }


    void StartGame()
    {
        

        _gameStarted = true;
        _time = _maxTime;

        for (int i = 0; i < 3; i++)
        {
            StepRock(0);
        }

        AddPoints(_pointsToAdd);
        _rewardButton.SetActive(false);

        _player.canSound = true;
        _currentHeight = 0;

        if(_needTutorial)
            StartTutorial();
    }

    public void StepRock(float x)
    {
        if (!_gameStarted) return;
        if (_inTutorial)
        {
            EndTutorial();
        } 

        var num = UnityEngine.Random.Range(0, _spawners.Length+1);
        var num2 = UnityEngine.Random.Range(0, _spawners.Length+1);

        for (int i = 0; i < _spawners.Length; i++)
        {
            if (i == num || i == num2)
                _spawners[i].canSpawn = false;
            else
                _spawners[i].canSpawn = true;

            if (_needTutorial)
                _spawners[1].canSpawn = true;

            //Debug.Log($"{num} {num2}");
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

        //_player.canSound = false;
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
                _audioSource.PlayOneShot(_loseClip);
            }
            else
            {
                //Debug.Log("Buena campeon");
                _gameOverText.text = "You Win";
                _gameOverText.color = Color.green;
                _audioSource.PlayOneShot(_winClip);
            }
        }

        CalculatePoints(_currentHeight);

        _multText.text = "x " + _adMult;
        _rewardButton.SetActive(true);

        StaminaSystem.Instance.UseStamina(StaminaSystem.Instance.gameStaminaCost);
    }


    [SerializeField] GameObject _rewardButton;
    [SerializeField] TMP_Text _pointsText;
    [SerializeField] TMP_Text _multText;
    [SerializeField] float _adMult;
    [SerializeField] int _heightToPoint;
    bool _doAddPoints;
    int _pointsToAdd;

    void AddPoints(int amount)
    {
        if (!_doAddPoints) return;

        //_pointsText.enabled = false;

        PointsManager.Instance.AddPoints(amount);
    }

    void CalculatePoints(int amount)
    {
        _doAddPoints = true;

        _pointsToAdd = amount / _heightToPoint;

        _pointsText.text = "your points: " + _pointsToAdd;
        _pointsText.enabled = true;
    }

    void AdReward()
    {
        float points = _currentHeight * _adMult;

        CalculatePoints((int)points);
        _rewardButton.SetActive(false);
    }

    public void PlayHitSound()
    {
        if(_audioSource == null || _hitClip == null) return;

        _audioSource.PlayOneShot(_hitClip);
    }

    private void OnDestroy()
    {
        AdsManager.Instance.rewardedAds.OnRewardAddComplete -= AdReward;
        AddPoints(_pointsToAdd);
    }
}
