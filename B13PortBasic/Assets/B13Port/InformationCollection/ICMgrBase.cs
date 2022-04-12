using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InformationCollection
{
    public abstract class ICMgrBase
    {
        public abstract void Init(ICConfigBase iCConfigBase);

        public abstract void SendEvent(string key);

        public abstract void SendEvent(string key, string value);

        public abstract void SendEvent(string eventName, Dictionary<string, string> dict);


        public virtual void OnApplicationPause(bool pauseStatus)
        {

        }

    }
}