/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
  ~  File：Test.cs                          
  ~  Author：B13Port                         
  ~  E-mail：bai0613@foxmail.com                    
  ~  Date：2022/04/12 16:29:20                             
  ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    private static readonly string scenePath = "Scenes";
    [Button]
    public void AAAA()
    {
        int scenescount = SceneManager.sceneCount;
        for (int i = 0; i < scenescount; i++)
        {
            Debug.LogError(SceneManager.GetSceneAt(i).name);
        }
    }
}
