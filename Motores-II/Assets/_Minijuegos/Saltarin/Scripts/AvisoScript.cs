using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvisoScript : MonoBehaviour
{
    [SerializeField] float _tiempoDeParpadeo;
    [SerializeField] Image _image;
    public GameObject _plat;
    float waitParpadeo;
    void Start()
    {
        _image = GetComponent<Image>();
    }

    void Update()
    {
        waitParpadeo += Time.deltaTime;
        
        if (waitParpadeo >= _tiempoDeParpadeo)
        {
            _image.enabled = !_image.enabled;
            waitParpadeo = 0;
        }

        transform.localPosition = Camera.main.WorldToViewportPoint(_plat.transform.position);
    }
}
