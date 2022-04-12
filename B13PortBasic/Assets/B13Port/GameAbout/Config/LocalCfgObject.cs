using B13Port.Common;
using EXECLConfig;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LocalCfgObject : ScriptableObject
{
    public LocalConfigItem[] localConfigItems;

    private Dictionary<string, Dictionary<string, string>> lanagerDic = new Dictionary<string, Dictionary<string, string>>();

    public string[] allLanges;
    public void InitData()
    {
        lanagerDic.Clear();
        for (int i = 0; i < localConfigItems.Length; i++)
        {
            if (!lanagerDic.ContainsKey(localConfigItems[i].key))
            {
                var temp = localConfigItems[i].stringStruct.ToDictionary(Key => Key.key, Value => Value.value);
                lanagerDic.Add(localConfigItems[i].key, temp);
            }
        }
        allLanges = GetLanges();
    }

    public string[] GetLanges()
    {
        string[] re = new string[lanagerDic.Count];
        int index = 0;
        foreach (var item in lanagerDic)
        {
            re[index] = item.Key;
            index++;
        }
        return re;
    }

    public string HasLang(string key)
    {
        if (lanagerDic.ContainsKey(key))
        {
            return key;
        }
        return "English";
    }

    public string GetStrByKey(string key)
    {
        string curLanguage = CommonTool.GetMachineLanage();


        curLanguage = HasLang(curLanguage);


        if (lanagerDic[curLanguage].ContainsKey(key))
        {
            return lanagerDic[curLanguage][key];
        }
        XDebug.LogError($"没此语言Key：{key} ");
        return $"没此语言Key：{key} ";
    }

    public string GetStrByKey(string key, string curLanguage)
    {

        curLanguage = HasLang(curLanguage);


        if (lanagerDic[curLanguage].ContainsKey(curLanguage))
        {
            return lanagerDic[curLanguage][curLanguage];
        }
        XDebug.LogError($"没此语言Key：{curLanguage} ");
        return $"没此语言Key：{curLanguage} ";
    }
    public string GetStrByKeyLocal(string key)
    {
        string curLanguage = CommonTool.GetMachineLanage();

        curLanguage = HasLang(curLanguage);

        if (!lanagerDic.ContainsKey(curLanguage))
        {
            curLanguage = "English";
        }
        if (lanagerDic[curLanguage].ContainsKey(key))
        {
            return lanagerDic[curLanguage][key];
        }
        XDebug.LogError($"没此语言Key：{key} ");
        return $"没此语言Key：{key} ";
    }
}
