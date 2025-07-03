using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UI_Plataformas : MonoBehaviour, IObserver_
{

    [SerializeField] PlayerBehaivour _player;

    [SerializeField] TextMeshProUGUI _saltosText;

    private void Start()
    {
        _saltosText = GetComponent<TextMeshProUGUI>();

        _player = PlayerBehaivour.Instance;

        if (_player.GetComponent<IObservable_>() != null)
        {
            _player.GetComponent<IObservable_>().Suscribe(this);
        }
    }

    public void Notify(int platNumber)
    {
        _saltosText.text = platNumber + " Saltos";
    }
}
