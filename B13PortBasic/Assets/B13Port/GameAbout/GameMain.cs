using Cinemachine;
using B13Port.Common;
using B13Port.UI;
using System;
using UnityEngine;
using XAsset;

public class GameMain : MonoBehaviour
{
    public static GameMain Ins = null;
    [NonSerialized]
    public Assets Assets = null;
    public UIMgr UIMgr = null;
    public ConfigMgr ConfigMgr = null;
    [NonSerialized] public AudioMgr audioMgr = null;

    public CinemachineBrain cinCamera;
    public Canvas mainCanvas;
    public RectTransform canvasRectt;
    public Camera uiCamera;
    public Camera mainCamera;

    [NonSerialized] public InformationCollection.ICMgr iCMgr = null;
    [NonSerialized] public B13Port.AD.ADMgr aDMgr = null;

    private void Awake()
    {

        Ins = this;
        XDebug.Log("**********GameMain**********", LogHelper.B13Port);
        Application.targetFrameRate = 80;
        Input.multiTouchEnabled = false;
        Assets = XAsset.Assets.Creat();
        DontDestroyOnLoad(Assets.gameObject);
        UIMgr = new UIMgr(Assets);
        ConfigMgr = new ConfigMgr(Assets);
        GameConfigMgr.LoadConfig(ConfigMgr);
        audioMgr = AudioMgr.Create(Assets);
        UserData.Instance.InitUserData();
#if UNITY_EDITOR
        iCMgr = InformationCollection.ICMgr.Creat();
        aDMgr = B13Port.AD.ADMgr.Creat();
#elif UNITY_ANDROID||UNITY_IPHONE
        iCMgr = Main.Ins.iCMgr;
        aDMgr = Main.Ins.aDMgr;
#endif        
        InitSet();

        iCMgr.SendEvent(IcmgrEventName.flow_Main);
    }


    float logOpenTime;
    bool isWGLogOpen;
    private void Update()
    {
        if (Input.anyKey)
        {
            if (!isWGLogOpen)
            {
                logOpenTime += Time.deltaTime;
                if (logOpenTime > 10)
                {
                    LogHelper.Init(true);
                    isWGLogOpen = true;
                    MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
                }
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            logOpenTime = 0;
        }
    }
    public void InitSet()
    {
        //audioMgr.SetVoice(AudioMgr.AudioType.back, UserData.Instance.playerData.gameSetData.music.Value);
        //audioMgr.SetVoice(AudioMgr.AudioType.effect, UserData.Instance.playerData.gameSetData.sound.Value);
    }

}
