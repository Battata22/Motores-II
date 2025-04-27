using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] float _speed;

    [SerializeField] LayerMask _player;

    public bool canMove = false;

    private void Awake()
    {
        Destroy(gameObject, 5f);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"<color=red>Le pegue a algo</color>");

        if (collision.transform.TryGetComponent<PlayerEscalada>(out var gil))
        {
            AttackHit(gil);
        }
    }

    private void Update()
    {
        Movement();
    }

    void AttackHit(PlayerEscalada gil)
    {
        Debug.Log($"<color=red>Le pegue al gil {gil.name}</color>");
    }

    void Movement()
    {
        if (!canMove) return;
        transform.position += Vector3.down * _speed * Time.deltaTime;
    }
}
