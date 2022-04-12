using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using B13Port.Common;


public class RedayConfig : MonoSingletion<RedayConfig>
{
    public LocalCfgObject languageConfig;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public override void Init()
    {
        base.Init();
        InitRedayConfig();
    }
    public void InitRedayConfig()
    {
        languageConfig = Resources.Load(ConfigPath.LanguageConfig) as LocalCfgObject;
        languageConfig.InitData();
    }

}
