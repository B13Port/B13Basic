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
using UnityEngine;

public class Test : MonoBehaviour
{
    [Button]
    public void AAAA()
    {
        string path = Application.dataPath;
        string newPath = path.Replace("Assets", "APKs");

    }
}
