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

namespace B13Port.EditorExtend
{
    public class AutoBuild : EditorWindow
    {
        private static readonly string scenePath = "Scenes";
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

            string secnepath = Path.Combine(Application.dataPath, scenePath);
            // 遍历获取目录下所有 .unity 文件
            string[] files = Directory.GetFiles(secnepath, "*.unity", SearchOption.AllDirectories);


            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
            {
                scenes = files,
                locationPathName = BundleName("apk", ref path),
                targetGroup = BuildTargetGroup.Android,
                target = BuildTarget.Android,
                options = BuildOptions.None
            };

            BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            BuildSummary summary = report.summary;

            if (summary.result == BuildResult.Succeeded)
            {
                XDebug.Log($"build android/apk successed:{summary.totalSize / 1024 / 1024} M");
                System.Diagnostics.Process.Start(path);
            }
            else if (summary.result == BuildResult.Failed)
            {
                XDebug.LogError("build android/apk failed");
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


            string secnepath = Path.Combine(Application.dataPath, scenePath);
            // 遍历获取目录下所有 .unity 文件
            string[] files = Directory.GetFiles(secnepath, "*.unity", SearchOption.AllDirectories);
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
            {
                scenes = files,
                locationPathName = BundleName("aab", ref path),
                targetGroup = BuildTargetGroup.Android,
                target = BuildTarget.Android,
                options = BuildOptions.None
            };

            BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            BuildSummary summary = report.summary;

            if (summary.result == BuildResult.Succeeded)
            {
                XDebug.Log($"build android/aab successed:{summary.totalSize / 1024 / 1024} M");
                System.Diagnostics.Process.Start(path);
            }
            else if (summary.result == BuildResult.Failed)
            {
                XDebug.LogError("build android/aab failed");
            }
        }

        public static string BundleName(string suffixName, ref string bundlepath)
        {
            string path = Application.dataPath;
            bundlepath = path.Replace("Assets", "APKs");
            if (!Directory.Exists(bundlepath))
                Directory.CreateDirectory(bundlepath);
            string name = Application.productName;
            string version = Application.version;
            string time = DateTime.Now.ToString("MM_dd_HH_mm");
            return $"{bundlepath}/{name}_{version}_{time}.{suffixName}";
        }
    }
}