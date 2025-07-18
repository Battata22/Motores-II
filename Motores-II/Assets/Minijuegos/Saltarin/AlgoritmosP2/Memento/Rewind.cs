using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Rewind : MonoBehaviour
{
    public MementoState mementoState;

    protected virtual void Awake()
    {
        mementoState = new MementoState();

        MementoManager.instance.AddMeRewind(this);
    }

    protected virtual void Start()
    {
        //SaltarinManager.instance.TriggerStep += ResetMe;
        //SaltarinManager.instance.TriggerStep += FakeAwake;
    }

    protected virtual void OnDestroy()
    {
        SaltarinManager.instance.TriggerStep -= ResetMe;
    }

    public void FakeAwake()
    {
        mementoState = new MementoState();

        MementoManager.instance.AddMeRewind(this);
    }

    public void ResetMe()
    {
        MementoManager.instance.QuitMeRewind(this);
    }

    public abstract void Save();
    public abstract void Load();

    public abstract void RemoveMe();
}
