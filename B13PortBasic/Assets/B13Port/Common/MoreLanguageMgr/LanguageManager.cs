using UnityEngine.UI;

namespace B13Port.Common
{
    public enum LanguageType
    {
        CHINESE,
        ENGLISH,
        JAPANESE,
        KOREAN
    }

    public delegate void ChangeLanguage();
    public delegate void ChangeFont(UnityEngine.Font font);
    public class LanguageManager : MonoSingletion<LanguageManager>
    {

        //定义一个委托事件，用于改变语言时，可以改变当前所显示的文本
        public event ChangeLanguage ChangeLangeuageEvent;
        public event ChangeFont ChangeFont;
        public override void Init()
        {
            base.Init();
            DontDestroyOnLoad(this);
        }
        public void SentEvent()
        {
            ChangeLangeuageEvent?.Invoke();
        }

        public void LanguageChangeFont(UnityEngine.Font font)
        {
            ChangeFont?.Invoke(font);
        }
        /// <summary>
        /// 通过key获取数据，设置Text的文本显示内容
        /// </summary>
        public void SetText(Text text, string target, string extraStr = "")
        {
            if (string.IsNullOrEmpty(target))
            {
                return;
            }
            if (string.IsNullOrEmpty(extraStr))
            {
                text.text = RedayConfig.Instance.languageConfig.GetStrByKey(target);
            }
            else
            {
                text.text = string.Format(RedayConfig.Instance.languageConfig.GetStrByKey(target), extraStr);
            }


        }

    }
}