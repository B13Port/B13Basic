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
        /// ���excel���ļ��еĵ�·��������excel�������"Assets/DLBasic/GameAbout/Config/Excel/Demo/ExcetAssets/"����
        /// </summary>
        public static readonly string excelsFolderPath = Application.dataPath + "/Resources/Config/ExcelConfig/";

        /// <summary>
        /// ���Excelת��CS�ļ����ļ���·��
        /// </summary>
        public static readonly string assetPath = "Assets/Resources/Config/AssetConfig/";



        /// <summary>
        /// ��ȡexcel�ļ�����
        /// </summary>
        /// <param name="filePath">�ļ�·��</param>
        /// <param name="columnNum">����</param>
        /// <param name="rowNum">����</param>
        /// <returns></returns>
        public static DataRowCollection ReadExcel(string filePath, ref int columnNum, ref int rowNum)
        {
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            DataSet result = excelReader.AsDataSet();
            //Tables[0] �±�0��ʾexcel�ļ��е�һ�ű������
            columnNum = result.Tables[0].Columns.Count;
            rowNum = result.Tables[0].Rows.Count;
            return result.Tables[0].Rows;
        }


        //�����Ҫexcelת.assetʹ�ô˴���ִ��(���ػ�����)
#if UNITY_EDITOR

        [MenuItem("B13Port/Excel/CreateLocalConfigAssets")]
        public static void CreateLocalConfigAssets()
        {
            LocalCfgObject manager = ScriptableObject.CreateInstance<LocalCfgObject>();
            //��ֵ
            manager.localConfigItems = CreateLocal(excelsFolderPath + "LocalConfig.xlsx");
            //ȷ���ļ��д���
            if (!Directory.Exists(ExcelConfig.assetPath))
            {
                Directory.CreateDirectory(ExcelConfig.assetPath);
            }

            //asset�ļ���·�� Ҫ��"Assets/..."��ʼ������CreateAsset�ᱨ��
            string assetPath = string.Format("{0}{1}.asset", ExcelConfig.assetPath, "LocalConfig");
            //����һ��Asset�ļ�
            AssetDatabase.CreateAsset(manager, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

#endif
        private static LocalConfigItem[] CreateLocal(string filePath)
        {
            //��ñ����� 9��  92��
            int columnNum = 0, rowNum = 0;
            DataRowCollection collect = ExcelConfig.ReadExcel(filePath, ref columnNum, ref rowNum);
            //����excel�Ķ��壬�ڶ��п�ʼ��������

            int langCount = int.Parse(collect[0][0].ToString()) + 1;//����������
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

    #region ���ػ�����
    public class LocalConfig
    {
        public LocalConfigItem[] localConfigItems;

        private Dictionary<string, Dictionary<string, string>> lanagerDic = new Dictionary<string, Dictionary<string, string>>();

        public LocalConfig(string path)
        {
            localConfigItems = CreateLocal(Application.streamingAssetsPath + path);//TPDO:����Excel��ַ
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
            //��ñ����� 9��  92��
            int columnNum = 0, rowNum = 0;
            DataRowCollection collect = ExcelConfig.ReadExcel(filePath, ref columnNum, ref rowNum);
            //����excel�Ķ��壬�ڶ��п�ʼ��������

            int langCount = int.Parse(collect[0][0].ToString());//����������
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
            XDebug.LogError($"û������Key��{key} ");
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