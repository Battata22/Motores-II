using System.Collections;
using System.Collections.Generic;
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

        private void Awake()
        {
            _detectPlayerInput = false;
        }

        void StartGame()
        {
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
        }

        public void CheckButton(CarlosButtons button)
        {
            if (button == _buttonsList[_currentButtonIndex])
                Lose();

            _currentButtonIndex++;
            if (_currentButtonIndex == _buttonsList.Count - 1)
                CompleteRound();
        }

        void CompleteRound()
        {
            _currentButtonIndex = 0;

            _currentRound++;
            if (_currentRound > _maxRound)
                Win();
            else
                StartCoroutine(ShowButtonsOrder(1f));
        }

        void Lose()
        {

        }

        void Win()
        {

        }

        IEnumerator ShowButtonsOrder(float initialDelay)
        {
            yield return new WaitForSeconds(initialDelay);

            foreach(var button in _buttonsList)
            {
                button.CallLightUp();

                yield return new WaitForSeconds(_timeBtwButtons);
            }
        }
    }
}
