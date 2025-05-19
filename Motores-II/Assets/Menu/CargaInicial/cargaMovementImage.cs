using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cargaMovementImage : MonoBehaviour
{
    [SerializeField] bool chico = false, grande = false;
    [SerializeField] float speedRot;

    void Update()
    {
        if (chico == true)
        {
            transform.localEulerAngles += new Vector3(0f, 0f, speedRot) * Time.deltaTime;
        }

        if (grande == true)
        {
            transform.localEulerAngles += new Vector3(0f, 0f, -speedRot) * Time.deltaTime;
        }

    }
}
