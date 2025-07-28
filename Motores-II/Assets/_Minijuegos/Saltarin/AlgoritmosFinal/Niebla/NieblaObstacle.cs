using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NieblaObstacle : Obstacle
{
    [SerializeField] NieblaBehaivour _niebla;
    [SerializeField] float _altura;
    [SerializeField] float _timer;
    float wait;

    protected override void Start()
    {
        base.Start();
    }


    protected override void Update()
    {
        base.Update();
        wait += Time.deltaTime;

        if (wait >= _timer)
        {
            TurnOff();
        }
    }

    protected override void FollowLider(float altura = 1)
    {
        base.FollowLider(_altura);
    }

    public override void TurnOn()
    {
        base.TurnOn();
        wait = 0;
    }

    public override void Touch()
    {
        base.Touch();
        //active la niebla
        _niebla.TurnOn();
        TurnOff();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerBehaivour>() != null)
        {
            Touch();
        }
    }
}
