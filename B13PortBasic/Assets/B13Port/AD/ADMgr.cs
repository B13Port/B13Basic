using System;
using UnityEngine;
using System.Collections.Generic;

namespace B13Port.AD
{

    public class ADMgr : MonoBehaviour
    {
        static ADMgr _ins = null;
        private List<ADMgrBase> ADBases;

        public static ADMgr Creat()
        {
            if (_ins != null) return _ins;
            GameObject go = new GameObject("ADMgr");
            DontDestroyOnLoad(go);
            _ins = go.AddComponent<ADMgr>();
            _ins.Init();
            return _ins;
        }


        private void Init()
        {
            ADBases = new List<ADMgrBase>();

            var ADConfigBaseObjs = Resources.LoadAll("ADConfig", typeof(ADConfigBase));

            foreach (var item in ADConfigBaseObjs)
            {
                var ADConfigBase = item as ADConfigBase;
                if (ADConfigBase.isEnabled)
                {
                    var aDMgrBase = CreateInstance<ADMgrBase>(ADConfigBase.AdMgrPath);
                    ADBases.Add(aDMgrBase);
                    aDMgrBase.Init(ADConfigBase);
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

        public void LoadAwardAD()
        {
            for (int i = 0; i < ADBases.Count; i++)
            {
                ADBases[i].LoadAwardAD();
            }
        }

        public void LoadInsertAD()
        {
            for (int i = 0; i < ADBases.Count; i++)
            {
                ADBases[i].LoadInsertAD();
            }
        }

        public void PlayAwardAD(ADStruct aDStruct)
        {
            for (int i = 0; i < ADBases.Count; i++)
            {
                ADBases[i].PlayAwardAD(aDStruct);
            }
        }

        public void PlayInsertAD(ADStruct aDStruct)
        {
            for (int i = 0; i < ADBases.Count; i++)
            {
                ADBases[i].PlayInsertAD(aDStruct);
            }
        }

        public void CreateBannerAD()
        {
            for (int i = 0; i < ADBases.Count; i++)
            {
                ADBases[i].CreateBannerAD();
            }
        }


        public void HideBannerAD()
        {
            for (int i = 0; i < ADBases.Count; i++)
            {
                ADBases[i].HideBannerAD();
            }
        }

        public void ShowBannerAD(ADStruct aDStruct)
        {
            for (int i = 0; i < ADBases.Count; i++)
            {
                ADBases[i].ShowBannerAD(aDStruct);
            }
        }

        public bool ADIsReady(ADType ADIsReady)
        {
            int redayAD = 0;
            for (int i = 0; i < ADBases.Count; i++)
            {
                bool isreday = ADBases[i].ADIsReady(ADIsReady);
                redayAD += isreday ? 1 : 0;
            }
            return redayAD > 0;
        }


        private void OnApplicationPause(bool pause)
        {
            for (int i = 0; i < ADBases.Count; i++)
            {
                ADBases[i].OnApplicationPause(pause);
            }
        }
    }
}
