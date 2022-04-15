/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
  ~  File：AutoBuild.cs                          
  ~  Author：B13Port                         
  ~  E-mail：bai0613@foxmail.com                    
  ~  Date：2022/04/12 15:59:39                             
  ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

using System;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

namespace B13Port.EditorExtend
{
    public class AutoBuild : EditorWindow
    {
        private static string keystorePass = "123456";
        private static string keyaliasPass = "123456";

        [MenuItem("B13Port/BuildAPK", false, 1)]
        public static void BuildAPK()
        {
            string path = "";
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
            PlayerSettings.Android.keystorePass = keystorePass;
            PlayerSettings.Android.keyaliasPass = keyaliasPass;

            AssetDatabase.Refresh();
            EditorUserBuildSettings.exportAsGoogleAndroidProject = false;
            EditorUserBuildSettings.buildAppBundle = false;


            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
            {
                scenes = GetBuildScenes(),
                locationPathName = BundleName("apk", ref path),
                targetGroup = BuildTargetGroup.Android,
                target = BuildTarget.Android,
                options = BuildOptions.None
            };

            BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            BuildSummary summary = report.summary;

            if (summary.result == BuildResult.Succeeded)
            {
                XDebug.Log($"build android/apk successed:{summary.totalSize / 1024 / 1024} M", LogHelper.Editor);
                System.Diagnostics.Process.Start(path);
            }
            else if (summary.result == BuildResult.Failed)
            {
                XDebug.LogError("build android/apk failed", LogHelper.Editor);
            }
        }

        [MenuItem("B13Port/BuildAAB", false, 1)]
        public static void BuildAAB()
        {
            string path = "";
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
            PlayerSettings.Android.keystorePass = keystorePass;
            PlayerSettings.Android.keyaliasPass = keyaliasPass;

            AssetDatabase.Refresh();
            EditorUserBuildSettings.exportAsGoogleAndroidProject = false;
            EditorUserBuildSettings.buildAppBundle = true;

            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
            {
                scenes = GetBuildScenes(),
                locationPathName = BundleName("aab", ref path),
                targetGroup = BuildTargetGroup.Android,
                target = BuildTarget.Android,
                options = BuildOptions.None
            };

            BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            BuildSummary summary = report.summary;

            if (summary.result == BuildResult.Succeeded)
            {
                XDebug.Log($"build android/aab successed:{summary.totalSize / 1024 / 1024} M", LogHelper.Editor);
                System.Diagnostics.Process.Start(path);
            }
            else if (summary.result == BuildResult.Failed)
            {
                XDebug.LogError("build android/aab failed", LogHelper.Editor);
            }
        }
        public static string[] GetBuildScenes()
        {
            int scenescount = SceneManager.sceneCount;
            string[] retScenes = new string[scenescount];
            for (int i = 0; i < scenescount; i++)
            {
                retScenes[i] = SceneManager.GetSceneAt(i).name;
            }
            return retScenes;
        }

        public static string BundleName(string suffixName, ref string bundlepath)
        {
            string path = Application.dataPath;
            bundlepath = path.Replace("Assets", "APKs");
            if (!Directory.Exists(bundlepath))
                Directory.CreateDirectory(bundlepath);
            string name = Application.productName;
            string version = Application.version;
            string time = DateTime.Now.ToString("MM.dd_HH.mm");
            return $"{bundlepath}/{name}_{version}_{time}.{suffixName}";
        }
    }
}