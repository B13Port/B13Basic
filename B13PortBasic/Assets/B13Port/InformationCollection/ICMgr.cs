
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Type = System.Type;

namespace InformationCollection
{
    public class ICMgr : MonoBehaviour
    {
        List<ICMgrBase> ICBases;

        static ICMgr _ins = null;
        public static ICMgr Creat()
        {
            if (_ins != null) return _ins;

            GameObject go = new GameObject("ICMgr");
            DontDestroyOnLoad(go);
            _ins = go.AddComponent<ICMgr>();
            _ins.Init();
            return _ins;
        }

        void Init()
        {
            ICBases = new List<ICMgrBase>();

            var iCConfigBaseObjs = Resources.LoadAll("InformationCollection", typeof(ICConfigBase));

            foreach (var item in iCConfigBaseObjs)
            {
                var iCConfigBase = item as ICConfigBase;
                if (iCConfigBase.isEnabled)
                {
                    var iCMgrBase = CreateInstance<ICMgrBase>(iCConfigBase.IcMgrPath);
                    ICBases.Add(iCMgrBase);
                    iCMgrBase.Init(iCConfigBase);
                }
            }
        }

        T CreateInstance<T>(string fullName)
        {
            var path = fullName;
            var o = Type.GetType(path);
            var obj = Activator.CreateInstance(o, true);
            return (T)obj;
        }

        public void SendEvent(string key)
        {
            for (int i = 0; i < ICBases.Count; i++)
            {
                ICBases[i].SendEvent(key);
            }
        }

        public void SendEvent(string key, string value)
        {
            for (int i = 0; i < ICBases.Count; i++)
            {
                ICBases[i].SendEvent(key, value);
            }

        }


        public void SendEvent(string eventName, Dictionary<string, string> dict)
        {

            for (int i = 0; i < ICBases.Count; i++)
            {
                ICBases[i].SendEvent(eventName, dict);
            }

        }




        private void OnApplicationPause(bool pause)
        {
            for (int i = 0; i < ICBases.Count; i++)
            {
                ICBases[i].OnApplicationPause(pause);
            }
        }

    }
}