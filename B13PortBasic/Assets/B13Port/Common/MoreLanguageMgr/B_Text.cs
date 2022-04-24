/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
  ~  File：B_Text.cs                          
  ~  Author：B13Port                         
  ~  E-mail：bai0613@foxmail.com                    
  ~  Date：2022/04/24 10:14:21                             
  ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

using B13Port.Common;
using UnityEngine.UI;


public class B_Text : Text
{
    public bool isShowLauge;
    protected override void Start()
    {
        base.Start();
        if (isShowLauge)
        {
            LanguageManager.Instance.ChangeLangeuageEvent += OnChangeText;
            OnChangeText();
        }
    }

    private void OnChangeText()
    {
        LanguageManager.Instance.SetText(this, m_Text);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        if (isShowLauge) LanguageManager.Instance.ChangeLangeuageEvent -= OnChangeText;
    }
}