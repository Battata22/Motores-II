using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEjemploMemento : Rewind
{
    public float life;
    public int gold;

    #region Etc
    //public float speed;

    //private void Update()
    //{
    //    var h = Input.GetAxis("Horizontal");
    //    var v = Input.GetAxis("Vertical");

    //    var dir = transform.forward * v;
    //    dir += transform.right * h;

    //    transform.position += dir.normalized * speed * Time.deltaTime;

    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        life = Random.Range(5, 101);
    //    }
    //}

    //public void SetGold(int addGold)
    //{
    //    gold += addGold;
    //} 
    #endregion

    public override void Save()
    {
        mementoState.Rec(life, gold, transform.position);
    }

    public override void Load()
    {
        if (!mementoState.IsRemember()) return;

        var remember = mementoState.Remember();

        life = (float)remember.parameters[0];
        gold = (int)remember.parameters[1];
        transform.position = (Vector3)remember.parameters[2];
    }

    public override void RemoveMe()
    {
        
    }

}
