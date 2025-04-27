using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFolow : MonoBehaviour
{
    [SerializeField] Transform _camPos;
    
    void Update()
    {
        transform.position = _camPos.position;
    }

}
