using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointAdder : MonoBehaviour, ILifeObserver
{
    [SerializeField] bool _doAddPoints;
    [SerializeField] TMP_Text _pointsText;
    [SerializeField] int _maxPoints;


    float _life, _maxLife;
    int _points;

    [SerializeField] GameObject _observable;

    private void Awake()
    {
        if (_observable.GetComponent<ILifeObservable>() != null)
        {
            _observable.GetComponent<ILifeObservable>().Subscribe(this);
        }
        else
        {
            gameObject.SetActive(false);
        }

        EventManager.Subscribe("PlayerDeath", CalculatePoint);
        EventManager.Subscribe("BossDeath", CalculatePoint);
        
    }

    private void OnDestroy()
    {

        EventManager.Unsubscribe("PlayerDeath", CalculatePoint);
        EventManager.Unsubscribe("BossDeath", CalculatePoint);

        AddPoints(_points);

        if (_observable.GetComponent<ILifeObservable>() != null)
            _observable.GetComponent<ILifeObservable>().Unsubscribe(this);

        StaminaSystem.Instance.UseStamina(StaminaSystem.Instance.gameStaminaCost);
    }

    void CalculatePoint(params object[] noUse)
    {
        //calculando
        //puntos maximos cuando matas al boss
        //porcentage de puntos basado en vida faltante de boss

        //25 = maxlife (500)
        // ?? = life (100)
        //primera vez usando regla de 3 feels good man 

        _points = (int)(_maxPoints * ((_maxLife - _life)/ _maxLife));

        if (_points < 0) _points = 0;
        if (_points > _maxPoints) _points = _maxPoints;

        _pointsText.text = $"Points : {_points}";
        _pointsText.enabled = true;

        _doAddPoints = true;
    }

    //EventManager.Trigger("PlayerDeath");
    //EventManager.Trigger("BossDeath");

    void AddPoints(int amount)
    {
        if (!_doAddPoints) return;

        PointsManager.Instance.AddPoints(amount);

        //_pointsText.enabled = false;
    }

    public void Notify(float life, float maxLife)
    {
        _life = life;
        _maxLife = maxLife;
    }
}
