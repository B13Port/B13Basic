using InformationCollection;
using System.Collections.Generic;

namespace IC.IcDuLu
{
    public class DuLuAnalysisMgr : ICMgrBase
    {
        public override void Init(ICConfigBase iCConfigBase)
        {
            XDebug.Log("DuLuAnalysisMgr 初始化", LogHelper.ICMgr);
        }

        public override void SendEvent(string key)
        {
            XDebug.Log("DuLuAnalysisMgr SendEvent key：" + key, LogHelper.ICMgr);
        }

        public override void SendEvent(string key, string value)
        {
            XDebug.Log($"DuLuAnalysisMgr SendEvent key{key} value{value}", LogHelper.ICMgr);
        }

        public override void SendEvent(string eventName, Dictionary<string, string> dict)
        {
            XDebug.Log($"DuLuAnalysisMgr SendEvent eventName{eventName}", LogHelper.ICMgr);
        }

    }
}