using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultPointsObstacle : Obstacle
{

    [SerializeField] float _height;
    [SerializeField] float _timer;
    float wait;
    [SerializeField] float duration;

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
        StartCoroutine(Activate());
        TurnOff();
    }

    IEnumerator Activate()
    {
        PlayerBehaivour.Instance.activatedMultiply = true;
        yield return new WaitForSeconds(duration);
        PlayerBehaivour.Instance.activatedMultiply = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerBehaivour>() != null)
        {
            Touch();
        }
    }
}
