using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletHell
{
    public class ParamsMemento
    {
        public object[] parameters;

        public ParamsMemento(params object[] p)
        {
            parameters = p;
        }
    }
}