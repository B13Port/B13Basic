using B13Port.Common;
using Excel;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System;

namespace EXECLConfig
{
    public class ExcelConfig
    {
        #region ExcelBaseFun

        /// <summary>
        /// 存放excel表文件夹的的路径，本例excel表放在了"Assets/DLBasic/GameAbout/Config/Excel/Demo/ExcetAssets/"当中
        /// </summary>
        public static readonly string excelsFolderPath = Application.dataPath + "/Resources/Config/ExcelConfig/";

        /// <summary>
        /// 存放Excel转化CS文件的文件夹路径
        /// </summary>
        public static readonly string assetPath = "Assets/Resources/Config/AssetConfig/";



        /// <summary>
        /// 读取excel文件内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="columnNum">行数</param>
        /// <param name="rowNum">列数</param>
        /// <returns></returns>
        public static DataRowCollection ReadExcel(string filePath, ref int columnNum, ref int rowNum)
        {
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            DataSet result = excelReader.AsDataSet();
            //Tables[0] 下标0表示excel文件中第一张表的数据
            columnNum = result.Tables[0].Columns.Count;
            rowNum = result.Tables[0].Rows.Count;
            return result.Tables[0].Rows;
        }


        //如果需要excel转.asset使用此代码执行(本地化测试)
#if UNITY_EDITOR

        [MenuItem("B13Port/Excel/CreateLocalConfigAssets")]
        public static void CreateLocalConfigAssets()
        {
            LocalCfgObject manager = ScriptableObject.CreateInstance<LocalCfgObject>();
            //赋值
            manager.localConfigItems = CreateLocal(excelsFolderPath + "LocalConfig.xlsx");
            //确保文件夹存在
            if (!Directory.Exists(ExcelConfig.assetPath))
            {
                Directory.CreateDirectory(ExcelConfig.assetPath);
            }

            //asset文件的路径 要以"Assets/..."开始，否则CreateAsset会报错
            string assetPath = string.Format("{0}{1}.asset", ExcelConfig.assetPath, "LocalConfig");
            //生成一个Asset文件
            AssetDatabase.CreateAsset(manager, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

#endif
        private static LocalConfigItem[] CreateLocal(string filePath)
        {
            //获得表数据 9列  92行
            int columnNum = 0, rowNum = 0;
            DataRowCollection collect = ExcelConfig.ReadExcel(filePath, ref columnNum, ref rowNum);
            //根据excel的定义，第二行开始才是数据

            int langCount = int.Parse(collect[0][0].ToString()) + 1;//多少种语言
            List<LocalConfigItem> langs = new List<LocalConfigItem>();

            for (int i = 1; i < langCount; i++)
            {
                LocalConfigItem item = new LocalConfigItem();
                item.key = collect[0][i].ToString();

                for (int j = 1; j < rowNum; j++)
                {
                    item.AddStructs(collect[j][0].ToString(), collect[j][i].ToString());
                }

                langs.Add(item);
            }

            return langs.ToArray();
        }
        #endregion

    }

    #region 本地化配置
    public class LocalConfig
    {
        public LocalConfigItem[] localConfigItems;

        private Dictionary<string, Dictionary<string, string>> lanagerDic = new Dictionary<string, Dictionary<string, string>>();

        public LocalConfig(string path)
        {
            localConfigItems = CreateLocal(Application.streamingAssetsPath + path);//TPDO:传入Excel地址
        }
        public void InitData()
        {
            for (int i = 0; i < localConfigItems.Length; i++)
            {
                if (!lanagerDic.ContainsKey(localConfigItems[i].key))
                {
                    var temp = localConfigItems[i].stringStruct.ToDictionary(Key => Key.key, Value => Value.value);
                    lanagerDic.Add(localConfigItems[i].key, temp);
                }
            }
        }

        private LocalConfigItem[] CreateLocal(string filePath)
        {
            //获得表数据 9列  92行
            int columnNum = 0, rowNum = 0;
            DataRowCollection collect = ExcelConfig.ReadExcel(filePath, ref columnNum, ref rowNum);
            //根据excel的定义，第二行开始才是数据

            int langCount = int.Parse(collect[0][0].ToString());//多少种语言
            List<LocalConfigItem> langs = new List<LocalConfigItem>();

            for (int i = 1; i < langCount; i++)
            {
                LocalConfigItem item = new LocalConfigItem();
                item.key = collect[0][i].ToString();

                for (int j = 1; j < rowNum; j++)
                {
                    item.AddStructs(collect[j][0].ToString(), collect[j][i].ToString());
                }

                langs.Add(item);
            }

            return langs.ToArray();
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

            if (lanagerDic[curLanguage].ContainsKey(key))
            {
                return lanagerDic[curLanguage][key];
            }
            XDebug.LogError($"没此语言Key：{key} ");
            return "";
        }
    }
    [Serializable]
    public class LocalConfigItem
    {
        public string key;
        public List<StringStruct> stringStruct = new List<StringStruct>();

        public void AddStructs(string k, string v)
        {
            StringStruct data = new StringStruct(k, v);
            stringStruct.Add(data);
        }
    }
    [Serializable]
    public class StringStruct
    {
        public string key;
        public string value;
        public StringStruct(string k, string v)
        {
            key = k;
            value = v;
        }
    }
    #endregion
}