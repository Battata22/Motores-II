using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEscalada : MonoBehaviour
{
    //a
    public void SetPos(float x)
    {
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
}
