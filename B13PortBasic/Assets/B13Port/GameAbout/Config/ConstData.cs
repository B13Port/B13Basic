using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using System;

//游戏常量
public class ConstData : ScriptableObject
{
    [LabelText("UI置灰Shader")] public Material grayUI;
    [LabelText("隐私政策")] public string policy = "https://cube2048.royalcasualgame.com/privacypolicy.html";
    [LabelText("用户协议")] public string terms = "https://cube2048.royalcasualgame.com/termsofservice.html";
    [LabelText("评价商店地址")] public string shopUrl = "https://play.google.com/store/apps/details?id=com.huoguo.jump.gp";



    [LabelText("评价弹窗时间")] public float reviveTime = 300;

    [LabelText("提现渠道图集")] public SpriteAtlas PayFunIcon;
    [LabelText("提现icon图集")] public SpriteAtlas PaypalIcon;
    [LabelText("实物兑换图片")] public SpriteAtlas EntitySpriteAtlas;
    [LabelText("全局掉落图集")] public SpriteAtlas dropAtlas;
}
