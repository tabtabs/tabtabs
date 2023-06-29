using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TabTabs.NamChanwoo
{
    public abstract class GameSystem : MonoBehaviour
    {
        public virtual void OnSystemInit() { }
        public virtual void OnSystemStart() { }
        public virtual void OnSystemStop() { }
        
    }
}

