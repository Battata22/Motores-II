using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarlosDice
{
    public class CarlosButtons : MonoBehaviour
    {
        [SerializeField] KeyCode _interactKey;
        [SerializeField] float _lightTime;

        private void Update()
        {
            if (Input.GetKeyDown(_interactKey))
            {

            }
        }

        void PlayerPress()
        {

        }

        public void CallLightUp()
        {
            StartCoroutine(LightUp());
        }

        IEnumerator LightUp()
        {

        }


    }
}
