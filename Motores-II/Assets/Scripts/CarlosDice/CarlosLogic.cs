using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
        [SerializeField] int _initialButtonsCount;

        [SerializeField] float _timeBtwButtons;

        [SerializeField] bool _detectPlayerInput = false;

        [SerializeField] int _currentButtonIndex = 0;
        [SerializeField] int _maxRound;
        [SerializeField] int _currentRound = 0;

        bool _gameStarted = false;

        private void Awake()
        {
            _detectPlayerInput = false;
        }

        private void Start()
        {
            foreach(var button in _posibleButtons)
            {
                button.Initialize(this);
            }
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
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

            if (_currentRound > _maxRound)
                Win();
            else
            {
                AdButtonToList(1);
                StartCoroutine(ShowButtonsOrder(1f));
            }
        }

        void Lose()
        {
            Debug.Log("<color=red>Perdiste gil</color>");

            _gameStarted = false;

            _currentRound = 0;
            _currentButtonIndex = 0;

            foreach (var button in _posibleButtons)
            {
                button.SetActive(false);
            }

            _buttonsList.Clear();
             
        }

        void Win()
        {
            Debug.Log("<color=green>Ganaste gil</color>");
            
            _gameStarted = false;

            _currentRound = 0;
            _currentButtonIndex = 0;

            _buttonsList.Clear();
        }

        IEnumerator ShowButtonsOrder(float initialDelay)
        {
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
        }
    }
}
