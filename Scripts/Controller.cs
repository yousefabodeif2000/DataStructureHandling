using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dimensional.Game
{
    public abstract class Controller : MonoBehaviour
    {
        public bool destroyOnLoad;
        public abstract void Initialize();
        protected virtual void Awake()
        {
            if (destroyOnLoad)
                DontDestroyOnLoad(this);

            Initialize();
        }
    }
}
