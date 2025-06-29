using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class PointsManager : MonoBehaviour
{

    #region Singleton
    public static PointsManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public int _points;

    void Start()
    {
        //cargar los antiguos
        LoadLastPoints();
    }

    public void AddPoints(int points)
    {
        _points += points;
        //Guardar datos
        SaveLastPoints();
    }
    public void AddPoints(float points)
    {
        _points += Convert.ToInt32(points);
        //Guardar datos
        SaveLastPoints();
    }

    public void SubstractPoints(int points)
    {
        _points -= points;
        if (_points < 0)
        {
            _points = 0;
        }
        SaveLastPoints();
    }

    public void SaveLastPoints()
    {
        LastPointsData data = new();
        data.Points = _points;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.dataPath + "/PointsManager/LastPointsInfo.json", json);
    }

    public void LoadLastPoints()
    {
        if (File.Exists(Application.dataPath + "/PointsManager/LastPointsInfo.json") == true)
        {
            string json = File.ReadAllText(Application.dataPath + "/PointsManager/LastPointsInfo.json");
            LastPointsData data = JsonUtility.FromJson<LastPointsData>(json);

            _points = data.Points;
        }
        else
        {
            _points = 0;
        }
    }
}

public class LastPointsData
{
    public int Points;
}
