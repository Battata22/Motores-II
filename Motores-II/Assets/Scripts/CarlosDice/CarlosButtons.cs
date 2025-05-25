using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CarlosDice
{
    public class CarlosButtons : MonoBehaviour
    {
        [SerializeField] KeyCode _interactKey;
        [SerializeField] float _lightTime;
        [Space]
        [SerializeField] Color _onColor;
        [SerializeField] Color _offColor;
        [Space]
        [SerializeField] AudioSource _source;
        [SerializeField] AudioClip _lightOn;

        Image _myImage;

        [SerializeField] bool _active = false;
        CarlosLogic _myLogic;

        private void Awake()
        {
            _myImage = GetComponent<Image>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(_interactKey))
            {
                PlayerPress();
            }
        }

        public void Initialize(CarlosLogic newLogic)
        {
            _active = false;
            _myLogic = newLogic;
        }

        public void SetActive(bool value)
        {
            _active = value;
        }

        public void PlayerPress()
        {
            if (!_active) return;

            CallLightUp();
            //if (_myLogic != null)
                if (_myLogic.CheckButton(this))
                    PlaySound();
           
        }

        public void CallLightUp()
        {
            StartCoroutine(LightUp());
        }

        IEnumerator LightUp()
        {
            _myImage.color = _onColor;
            

            yield return new WaitForSeconds(_lightTime);

            _myImage.color = _offColor;
        }

        public void PlaySound()
        {
            _source.PlayOneShot(_lightOn);
        }

    }
}
