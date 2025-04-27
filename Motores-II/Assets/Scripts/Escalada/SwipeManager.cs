using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    public static SwipeManager instance { get; private set; }

    [SerializeField] int maxPointsAmount;
    [SerializeField] float intervalTime;
    float timer;

    SwipeData swipeData;

    public Action<SwipeData> OnSwipe;
    public Action<SwipeData> OnSwipeEnd;


    private void Awake()
    {
        instance = this;
        swipeData.points = new List<Vector2>();
        OnSwipe += DebugeandoAndo;
    }

    void DebugeandoAndo(SwipeData data)
    {
        Debug.Log($"Swipe Realizado");
    }

    void Update()
    {
        if (Input.touchCount < 1) return;

        var touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            BeganTouch(touch);
            return;
        }
        timer += Time.deltaTime;

        if (timer > intervalTime)
        {
            AddSwipePoint(touch);
        }

        if (touch.phase == TouchPhase.Ended) EndTouch(touch);

    }

    private void BeganTouch(Touch touch)
    {
        swipeData.swipe = true;
        swipeData.points.Add(touch.position);
        timer = 0;
    }

    private void AddSwipePoint(Touch touch)
    {
        swipeData.points.Add(touch.position);

        if (maxPointsAmount < swipeData.points.Count)
        {
            swipeData.points.RemoveAt(0);
        }
        timer = 0;

        OnSwipe?.Invoke(swipeData);
    }

    private void EndTouch(Touch touch)
    {
        OnSwipeEnd?.Invoke(swipeData);

        swipeData.swipe = false;
        swipeData.points.Clear();
        timer = 0;
        return;
    }
}

public struct SwipeData
{
    public List<Vector2> points;
    public bool swipe;
}

public enum SwipeDirection
{
    Up,
    Down,
    Left,
    Right,
    UpLeft,
    DownLeft,
    UpRight,
    DownRight
}
