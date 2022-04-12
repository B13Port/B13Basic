using Sirenix.OdinInspector;
using System;
using System.Threading;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main Ins = null;
    [NonSerialized] public InformationCollection.ICMgr iCMgr = null;
    [NonSerialized] public B13Port.AD.ADMgr aDMgr = null;
    //public UILoadAsyncScene loadScenes;
    private void Awake()
    {

        DontDestroyOnLoad(this);
        Ins = this;
        LogHelper.Init(false);
#if UNITY_EDITOR
        LogHelper.Init(true);
#endif
        Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
        XDebug.Log("**********Main**********", LogHelper.B13Port);
        iCMgr = InformationCollection.ICMgr.Creat();
        RedayConfig.Instance.Init();
        //loadScenes.InitView();
        //BIManager.Instance.isReadyAction = GoGame;
        //BIManager.Instance.InitHttpInfo();
        //AppsFlyerObjectScript.callback = BIManager.Instance.AppsFlyerCallBack;
    }

    //private void GoGame()
    //{
    //    aDMgr = DuLu.AD.ADMgr.Creat();
    //    loadScenes.GoGame();
    //}

    private void Start()
    {
        iCMgr.SendEvent(IcmgrEventName.flow_Load);
#if UNITY_EDITOR
        //ToAFBack();
#endif
    }

    //private void ToAFBack()
    //{
    //    BIManager.Instance.AppsFlyerCallBack(false);
    //}
}
