using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Rewind : MonoBehaviour
{
    public MementoState mementoState;

    protected virtual void Awake()
    {
        mementoState = new MementoState();

        MementoManager.instance.AddMeRewind(this);
    }

    public abstract void Save();
    public abstract void Load();

    public abstract void RemoveMe();
}
