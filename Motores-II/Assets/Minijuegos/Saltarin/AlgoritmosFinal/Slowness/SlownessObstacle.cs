using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlownessObstacle : Obstacle
{
    [SerializeField] float _height;
    [SerializeField] float _timer;
    [SerializeField] SplashBehaivour splashScript;
    [SerializeField] MeshRenderer[] meshRenderers;
    [SerializeField] float duration;
    float wait;

    protected override void Start()
    {
        _collider = GetComponent<Collider>();

        meshRenderers = GetComponentsInChildren<MeshRenderer>();

        TurnOff();
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
        foreach (var renderer in meshRenderers)
        {
            renderer.enabled = true;

        }
        _collider.enabled = true;


        wait = 0;
    }

    public override void TurnOff()
    {
        foreach (var renderer in meshRenderers)
        {
            renderer.enabled = false;

        }
        _collider.enabled = false;
    }

    public override void Touch()
    {
        base.Touch();
        //activar slow
        StartCoroutine(SaltarinManager.instance.SlowSpeed(duration));
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
