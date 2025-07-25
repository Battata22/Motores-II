using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] protected BulletFactory _bulletFactory;

    [SerializeField] protected float _cd;
    [SerializeField] protected float _speed;
    [SerializeField] protected BulletMovementType _type;
    [SerializeField] protected Team _team;
    [SerializeField] protected Vector3 dir;
    [SerializeField] protected float _bulletLifeTime;
    [SerializeField] protected int _damage;
    protected float _lastSpawn;

    protected Action<Vector3> Spawn = delegate { };

    [SerializeField] BasePlayer _player;

    protected void Awake()
    {
        dir = Vector3.up;

        SetBulletType(_type);
    }

    protected virtual void FixedUpdate()
    {
        if (Time.time > _lastSpawn + _cd)
        {
            _lastSpawn = Time.time;
            Spawn(dir);
            if(_player)
                _player.View.Shoot();
        }
    }

    public void SetDirection(Vector2 newDir)
    {
        dir = newDir;
    }

    protected void LinearBullet(Vector3 dir)
    {
        Debug.Log($"Spawener lineal {_damage}");
        _bulletFactory.SetTeam(_team)
            .SetLifeTime(_bulletLifeTime)
            .SetSpawnPos(transform)
            .SetDamage(_damage)
            .SetMovement(BulletMovementType.Linear, _speed, dir).Pool.Get().SetPool(_bulletFactory.Pool);
    }

    protected void HelixBullet(Vector3 dir)
    {
        _bulletFactory.SetTeam(_team);
        _bulletFactory.SetSpawnPos(transform)
            .SetLifeTime(_bulletLifeTime)
            .SetDamage(_damage)
            .SetMovement(BulletMovementType.Sen, _speed, dir).Pool.Get().SetPool(_bulletFactory.Pool);  
        _bulletFactory.SetSpawnPos(transform)
            .SetLifeTime(_bulletLifeTime)
            .SetDamage(_damage)
            .SetMovement(BulletMovementType.Cos, _speed, dir).Pool.Get().SetPool(_bulletFactory.Pool);
    }

    public BulletSpawner SetCoolDown(float cd)
    {
        _cd = cd;
        return this;
    }
    public BulletSpawner SetSpeed(float speed)
    {
        _speed = speed;
        return this;
    }
    
    public BulletSpawner SetDamage(int damage)
    {
        _damage = damage;
        return this;
    }

    protected void StaticBullet(Vector3 dir)
    {
        _bulletFactory.SetTeam(_team);
        _bulletFactory.SetSpawnPos(transform)
            .SetDamage(_damage);
        _bulletFactory.SetMovement(BulletMovementType.Static, _speed, dir).Pool.Get().SetPool(_bulletFactory.Pool);
    }

    public void SetBulletType( BulletMovementType type)
    {
        _type = type;
        switch (_type)
        {
            case BulletMovementType.Linear:
                Spawn = LinearBullet;
                break;
            case BulletMovementType.Sen:
                Spawn = HelixBullet;
                break;
            case BulletMovementType.Cos:
                Spawn = HelixBullet;
                break;
            case BulletMovementType.Static:
                Spawn = StaticBullet;
                break;
        }
    }

}
