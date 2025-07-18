using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogObstacle : Obstacle
{
    [SerializeField] float _height;
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

        if(wait >= _timer)
        {
            TurnOff();
        }
    }

    protected override void FollowLider(float altura = 1)
    {
        base.FollowLider(_height);
    }

    public override void TurnOn()
    {
        base.TurnOn();
        wait = 0;
    }

    public override void Touch()
    {
        base.Touch();
        StartCoroutine(PlayerBehaivour.Instance.Tronquear());
        TurnOff();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerBehaivour>() != null)
        {
            Touch();
        }
    }
}
