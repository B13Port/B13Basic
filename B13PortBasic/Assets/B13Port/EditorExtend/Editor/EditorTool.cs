using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
namespace B13Port.EditorExtend
{
    public class EditorTool : EditorWindow
    {
        [MenuItem("B13Port/DestoryColl", false, 1)]
        static void DestoryColl()
        {
            if (Selection.gameObjects.Length <= 0)
            {
                XDebug.LogError("请选择至少一个游戏物体");
                return;
            }
            for (int i = 0; i < Selection.gameObjects.Length; i++)
            {
                var count = Selection.gameObjects[i].transform.childCount;
                for (int j = 0; j < count; j++)
                {
                    for (int k = 0; k < Selection.gameObjects[i].GetComponentsInChildren<Collider>().Length; k++)
                    {
                        var item = Selection.gameObjects[i].GetComponentsInChildren<Collider>()[k];
                        DestroyImmediate(item);
                    }
                }
            }
        }

        [MenuItem("B13Port/CloseShadow", false, 1)]
        static void CloseShadow()
        {
            if (Selection.gameObjects.Length <= 0)
            {
                XDebug.LogError("请选择至少一个游戏物体");
                return;
            }
            for (int i = 0; i < Selection.gameObjects.Length; i++)
            {
                var count = Selection.gameObjects[i].transform.childCount;
                for (int j = 0; j < count; j++)
                {
                    for (int k = 0; k < Selection.gameObjects[i].GetComponentsInChildren<MeshRenderer>().Length; k++)
                    {
                        var compent = Selection.gameObjects[i].GetComponentsInChildren<MeshRenderer>()[k];
                        compent.shadowCastingMode = ShadowCastingMode.Off;
                        compent.receiveShadows = false;
                    }
                }
            }
        }

        [MenuItem("B13Port/Sort Object", false, 1)]
        static void SortGo()
        {
            if (Selection.gameObjects.Length <= 0)
            {
                XDebug.LogError("请选择至少一个游戏物体");
                return;
            }
            var go = Selection.gameObjects[0];
            Vector3 endPos = Vector3.zero;
            for (int i = 0; i < go.transform.childCount; i++)
            {
                endPos += Vector3.right * 2;
                if (i % 5 == 0)
                {
                    endPos.x = 0;
                    endPos += Vector3.forward * 2;
                }
                go.transform.GetChild(i).position = endPos;
            }
        }


        [MenuItem("B13Port/OpenAssets  &E")]
        public static void AssetsPath()
        {
            string path = Application.dataPath.Replace("Assets", "Assets");
            OpenWin(path);
        }

        [MenuItem("B13Port/OpenArt  &W")]
        public static void ArtPath()
        {
            string path = Application.dataPath.Replace("Project/LuckyJump3D/Assets", "Art/UI");
            OpenWin(path);
        }

        static void OpenWin(string path)
        {
            System.Diagnostics.Process.Start(path);
        }

        [MenuItem("Assets/B13Port/Copy Asset Path")]
        static void CopyAssetPath()
        {
            if (EditorApplication.isCompiling)
            {
                return;
            }
            string path = AssetDatabase.GetAssetPath(Selection.activeInstanceID);
            GUIUtility.systemCopyBuffer = path;
            XDebug.Log(string.Format("systemCopyBuffer: {0}", path));
        }

        [MenuItem("B13Port/Remove Missing-MonoBehavior Component")]
        static public void RemoveMissComponent()
        {
            string fullPath = Application.dataPath + "/Resources/Prefabs/UI/UIWindow";
            fullPath = fullPath.Replace("/", @"\");
            //List<string> pathList = GetAssetsPathByFullPath(fullPath, "*.prefab", SearchOption.AllDirectories);
            List<string> pathList = GetAssetsPathByRelativePath(new string[] { "Assets/Resources/Prefabs/UI/UIWindow" }, "t:Prefab", SearchOption.AllDirectories);
            int counter = 0;
            for (int i = 0, iMax = pathList.Count; i < iMax; i++)
            {
                EditorUtility.DisplayProgressBar("处理进度", string.Format("{0}/{1}", i + 1, iMax), (i + 1f) / iMax);
                if (CheckMissMonoBehavior(pathList[i]))
                    ++counter;
            }
            EditorUtility.ClearProgressBar();
            EditorUtility.DisplayDialog("处理结果", "完成修改，修改数量 : " + counter, "确定");
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 删除一个Prefab上的空脚本
        /// </summary>
        /// <param name="path">prefab路径 例Assets/Resources/FriendInfo.prefab</param>
        static bool CheckMissMonoBehavior(string path)
        {
            bool isNull = false;
            string textContent = File.ReadAllText(path);
            Regex regBlock = new Regex("MonoBehaviour");
            // 以"---"划分组件
            string[] strArray = textContent.Split(new string[] { "---" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < strArray.Length; i++)
            {
                string blockStr = strArray[i];
                if (regBlock.IsMatch(blockStr))
                {
                    // 模块是 MonoBehavior
                    //(?<名称>子表达式)  含义:将匹配的子表达式捕获到一个命名组中
                    Match guidMatch = Regex.Match(blockStr, "m_Script: {fileID: (.*), guid: (?<GuidValue>.*?), type: [0-9]}");
                    if (guidMatch.Success)
                    {
                        string guid = guidMatch.Groups["GuidValue"].Value;
                        if (!File.Exists(AssetDatabase.GUIDToAssetPath(guid)))
                        {
                            isNull = true;
                            textContent = DeleteContent(textContent, blockStr);
                        }
                    }

                    Match fileIdMatch = Regex.Match(blockStr, @"m_Script: {fileID: (?<IdValue>\d+)}");
                    if (fileIdMatch.Success)
                    {
                        string idValue = fileIdMatch.Groups["IdValue"].Value;
                        if (idValue.Equals("0"))
                        {
                            isNull = true;
                            textContent = DeleteContent(textContent, blockStr);
                        }
                    }
                }
            }
            if (isNull)
            {
                // 有空脚本 写回prefab
                File.WriteAllText(path, textContent);
            }
            return isNull;
        }

        /// <summary>
        /// 获取项目中某种资源的路径
        /// </summary>
        /// <param name="relativePath">unity路径格式，以 "/" 为分隔符</param>
        /// <param name="filter">unity的资源过滤模式 https://docs.unity3d.com/ScriptReference/AssetDatabase.FindAssets.html </param>
        /// <param name="searchOption"></param>
        /// <returns></returns>
        static List<string> GetAssetsPathByRelativePath(string[] relativePath, string filter, SearchOption searchOption)
        {
            List<string> pathList = new List<string>();
            string[] guids = AssetDatabase.FindAssets(filter, relativePath);
            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                pathList.Add(path);
            }

            return pathList;
        }

        // 删除操作
        static string DeleteContent(string input, string blockStr)
        {
            input = input.Replace("---" + blockStr, "");
            Match idMatch = Regex.Match(blockStr, "!u!(.*) &(?<idValue>.*?)\n");
            if (idMatch.Success)
            {
                // 获取 MonoBehavior的fileID
                string fileID = idMatch.Groups["idValue"].Value;
                Regex regex = new Regex("  - (.*): {fileID: " + fileID + "}\n");
                input = regex.Replace(input, "");
            }

            return input;
        }
    }



}

