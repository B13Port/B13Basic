using UnityEngine;

namespace InformationCollection
{

    public abstract class ICConfigBase : ScriptableObject
    {
        public bool isEnabled;

        public string appID;

        public abstract string IcMgrPath { get; }

    }
}