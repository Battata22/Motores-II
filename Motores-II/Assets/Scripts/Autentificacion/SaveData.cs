using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public int currency = 10;
    public float life = 100;
    public List<string> playerNames;
    public Transform target;
    public GameObject character;
    public Camera myCamera;
    public Rigidbody body;
}


// PlayerPrefs.SetString("SV", JsonUtility.ToJson(save)); //saves special class