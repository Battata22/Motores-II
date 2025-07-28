using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAroundBehaivour : MonoBehaviour
{
    public static CamAroundBehaivour Instance;

    [SerializeField] float _speed;
    [SerializeField] bool on = false;
    [SerializeField] float wait;

    private void Start()
    {
        Instance = this;
    }

    private void Update()
    {
        if (on)
        {
            wait += Time.deltaTime;
            transform.eulerAngles += new Vector3(0, 0, _speed * Time.deltaTime);

            if (wait >= 0.3f)
            {
                if (transform.eulerAngles.z >= -0.7 && transform.eulerAngles.z <= 0.5)
                {
                    TurnOffRotation();
                }
            }
        }

    }

    public void TurnOnRotation()
    {
        wait = 0;
        on = true;
    }

    public void TurnOffRotation()
    {
        on = false;
        transform.eulerAngles = new Vector3(29.074f, 0, 0);
    }

}
