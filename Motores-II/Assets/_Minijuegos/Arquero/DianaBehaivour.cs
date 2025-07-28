using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DianaBehaivour : MonoBehaviour
{

    [SerializeField] float _destroyTime;
    float wait;
        
    void Update()
    {
        TimeBomb();
    }

    void TimeBomb()
    {
        if (!PausaInGame.Instance.isPaused)
        {
            wait += Time.deltaTime;
            if (wait >= _destroyTime)
            {
                Destroy(gameObject);
            }
        }
        
    }
}
