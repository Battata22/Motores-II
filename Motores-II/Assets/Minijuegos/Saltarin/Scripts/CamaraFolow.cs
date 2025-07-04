using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFolow : Rewind
{
    [SerializeField] Transform _camPos;

    public override void Save()
    {
        //parametros para pasar, pos, onfloor?
        mementoState.Rec(transform.position);
    }

    public override void Load()
    {
        if (!mementoState.IsRemember()) return;

        var remember = mementoState.Remember();

        transform.position = (Vector3)remember.parameters[0];
    }

    public override void RemoveMe()
    {

    }

    void Update()
    {
        transform.position = _camPos.position;
    }

}
