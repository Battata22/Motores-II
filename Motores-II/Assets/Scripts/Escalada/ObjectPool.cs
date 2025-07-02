using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ObjectPool<T>
{
    List<T> _stock = new List<T>();
    Func<T> _factory;
    Action<T> _on;
    Action<T> _off;
    public ObjectPool(Func<T> Factory, Action<T> ObjOn, Action<T> ObjOff, int currentStock = 5)
    {
        _factory = Factory;
        _on = ObjOn;
        _off = ObjOff;

        for (int i = 0; i < currentStock; i++)
        {
            var x = _factory();
            _off(x);
            _stock.Add(x);
        }
    }

    public T Get()
    {
        T x;

        if (_stock.Count > 0)
        {
            x = _stock[0];
            _stock.Remove(x);
        }
        else
        {
            x = _factory();
        }

        _on(x);

        return x;
    }

    public void Return(T obj)
    {
        _off(obj);
        _stock.Add(obj);
    }
}
