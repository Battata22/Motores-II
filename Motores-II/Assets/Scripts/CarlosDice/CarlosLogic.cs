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

            RemoteConfigManager.Instance.OnConfigFetched += SetData;
        }

        void SetData()
        {
            _maxRound = RemoteConfigService.Instance.appConfig.GetInt("Carlos_MaxRounds");
            _initialButtonsCount = RemoteConfigService.Instance.appConfig.GetInt("Carlos_InitialRoundButtons");
            _endless = RemoteConfigService.Instance.appConfig.GetBool("Carlos_Endless");
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }


            if (Input.GetKeyDown(KeyCode.Space))
                StartGame();
        }

        public void StartGame()
        {
            if (_gameStarted) return;
            _gameStarted = true;

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
                Lose();
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
                Win();
            else
            {
                AdButtonToList(1);
                StartCoroutine(ShowButtonsOrder(1f));
            }
        }

        void GameOver(bool lose)
        {


            if (lose)
            {
                Lose();
            }
            else
            {
                Win();
            }
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
    }
}
