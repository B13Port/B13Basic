using UnityEngine;


namespace B13Port.AD
{
    public abstract class ADConfigBase : ScriptableObject
    {
        public bool isEnabled;

        public string appID;
        public string rewardADID;
        public string insertADID;
        public string bannerADID;

        public Color bannerBackColor;
        public bool bannerPos;//true =top
        public bool isDebug;

        public abstract string AdMgrPath { get; }
    }

}