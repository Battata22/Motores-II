using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.RemoteConfig;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CarlosDice
{
    public class CarlosLogic : MonoBehaviour
    {
        //prender bottones
        //guardar orden
        //arrancar gameplay
        //si le erras cagaste
        //cuando se termina añade x botones y loopea

        [SerializeField] CarlosButtons[] _posibleButtons;
        [SerializeField] List<CarlosButtons> _buttonsList;
        [SerializeField] Image _center;
        [SerializeField] int _initialButtonsCount;

        [SerializeField] float _timeBtwButtons;

        [SerializeField] bool _detectPlayerInput = false;

        [SerializeField] int _currentButtonIndex = 0;
        [SerializeField] TMP_Text _roundText;
        [SerializeField] int _maxRound;
        [SerializeField] int _currentRound = 0;

        bool _endless = false;
        bool _gameStarted = false;

        //ads
        [SerializeField] GameObject _rewardButton;
        [SerializeField] TMP_Text _pointsText;
        [SerializeField] TMP_Text _multText;
        [SerializeField] float _adMult;
        bool _doAddPoints;
        int _pointsToAdd;

        private void Awake()
        {
            _detectPlayerInput = false;

            Screen.orientation = ScreenOrientation.LandscapeLeft;
            Screen.autorotateToPortrait = false;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
            Screen.autorotateToPortraitUpsideDown = false;

            QualitySettings.vSyncCount = 1;
        }

        private void Start()
        {
            foreach(var button in _posibleButtons)
            {
                button.Initialize(this);
            }

            //RemoteConfigManager.Instance.OnConfigFetched += SetData;
            SetData();

            AdsManager.Instance.rewardedAds.OnRewardAddComplete += AdReward;
        }

        void SetData()
        {
            _maxRound = RemoteConfigManager.Instance.Carlos_MaxRound;
            _initialButtonsCount = RemoteConfigManager.Instance.Carlos_InitialButtonsCount;
            _endless = RemoteConfigManager.Instance.Carlos_Endless;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene("Menu");
            }


            if (Input.GetKeyDown(KeyCode.Space))
                StartGame();
        }

        public void StartGame()
        {
            if (_gameStarted) return;
            _gameStarted = true;

            AddPoints(_pointsToAdd);
            _rewardButton.SetActive(false);


            _currentRound = 1;

            AdButtonToList(_initialButtonsCount);
        }

        void AdButtonToList(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var button = _posibleButtons[Random.Range(0, _posibleButtons.Length)];
                _buttonsList.Add(button);
            }
            StartCoroutine(ShowButtonsOrder(1f));
        }

        public void CheckButton(CarlosButtons button)
        {
            if (button != _buttonsList[_currentButtonIndex])
            {
                //Lose();
                GameOver(true);
                return;
            }

            _currentButtonIndex++;
            if (_currentButtonIndex >= _buttonsList.Count)
                CompleteRound();
        }

        void CompleteRound()
        {
            _currentButtonIndex = 0;

            _currentRound++;

            foreach (var button in _posibleButtons)
            {
                button.SetActive(false);
            }

            if (!_endless && _currentRound > _maxRound)
            {
                //Win();
                GameOver(false);
            }
            else
            {
                AdButtonToList(1);
                StartCoroutine(ShowButtonsOrder(1f));
            }
        }

        void GameOver(bool lose)
        {
            //add point to json
            //pointToAdd = _currentRound;
            //if rewardAd
            //pointToAdd = poinToAdd * rewardMul

            CalculatePoints(_currentRound);

            if (lose)
            {
                Lose();
            }
            else
            {
                Win();
            }

            _multText.text = "x " + _adMult;
            _rewardButton.SetActive(true);
        }

        void AddPoints(int amount)
        {
            if (!_doAddPoints) return;

            _pointsText.enabled = false;

            PointsManager.Instance.AddPoints(amount);
        }

        void CalculatePoints(int amount)
        {
            _doAddPoints = true;

            _pointsToAdd = amount;

            _pointsText.text = "your points: " + _pointsToAdd;
            _pointsText.enabled = true;
        }

        void AdReward()
        {
            float points = _pointsToAdd * _adMult;

            CalculatePoints((int)points);
            _rewardButton.SetActive(false);
        }

        void Lose()
        {
            Debug.Log("<color=red>Perdiste gil</color>");

            _center.color = Color.red;


            _gameStarted = false;

            _currentRound = 0;
            _currentButtonIndex = 0;

            foreach (var button in _posibleButtons)
            {
                button.SetActive(false);
            }

            _buttonsList.Clear();

            if (_roundText != null)
                _roundText.text = $"YOU LOSE";
        }

        void Win()
        {
            Debug.Log("<color=green>Ganaste gil</color>");

            _center.color = Color.green;


            _gameStarted = false;

            _currentRound = 0;
            _currentButtonIndex = 0;

            _buttonsList.Clear();

            if (_roundText != null)
                _roundText.text = $"YOU WIN";
        }

        void UpdateCanvasText()
        {
            if (!_endless && _roundText != null)
                _roundText.text = $"{_currentRound} / {_maxRound}";
        }

        IEnumerator ShowButtonsOrder(float initialDelay)
        {
            UpdateCanvasText();

            _center.color = Color.yellow;

            yield return new WaitForSeconds(initialDelay);

            foreach(var button in _buttonsList)
            {
                button.CallLightUp();

                yield return new WaitForSeconds(_timeBtwButtons);
            }

            foreach(var button in _posibleButtons)
            {
                button.SetActive(true);

                yield return null;
            }

            _center.color = Color.black;
        }

        private void OnDestroy()
        {
            AdsManager.Instance.rewardedAds.OnRewardAddComplete -= AdReward;
            AddPoints(_pointsToAdd);
        }
    }
}
