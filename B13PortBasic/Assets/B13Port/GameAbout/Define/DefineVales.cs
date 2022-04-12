

public class DefineVales
{
    public const string PlayerDataKey = "PlayerData";
}

//游戏中所有的打点名字
public class IcmgrEventName
{
    #region 游戏流程
    public const string flow_Load = "flow_Load";
    public const string flow_Main = "flow_Main";
    public const string teach_stepcount = "teach_stepcount_{0}";//新手引导第1步完成人数
    #endregion

    #region 数据采集

    public static int[] goldValues = new int[] { 1200000, 1800000, 2550000, 2850000 };
    public static string[] goldPoints = new string[]
    {
        "play_CoinBalance1200000",
        "play_CoinBalance1800000",
        "play_CoinBalance2550000",
        "play_CoinBalance2850000"
    };

    public static int[] usdValues = new int[] { 300, 500, 600, 725 };
    public static string[] usdPoints = new string[]
    {
        "play_CashBalance300",
        "play_CashBalance500",
        "play_CashBalance600",
        "play_CashBalance725"
    };

    #endregion

    #region 主玩法弹窗
    public const string Score = "Score_{0}";

    public const string ad_NewHighScore_show = "ad_NewHighScore_show"; //新高分奖励翻倍页面【新高分翻倍奖励】按钮展示
    public const string ad_NewHighScore_click = "ad_NewHighScore_click";  //新高分奖励翻倍页面【新高分翻倍奖励】按钮点击
    public const string ad_NewHighScore_success = "ad_NewHighScore_success";      //新高分奖励翻倍页面【新高分翻倍奖励】成功拉起广告
    public const string ad_NewHighScore_complete = "ad_NewHighScore_complete";         //新高分奖励翻倍页面【新高分翻倍奖励】完整看完广告

    public const string ad_AddPointPlatform_show = "ad_BossPlatform_show";//加分跳台翻倍领奖页面【加分跳台翻倍奖励】按钮展示
    public const string ad_AddPointPlatform_click = "ad_BossPlatform_click";//加分跳台翻倍领奖页面【加分跳台翻倍奖励】按钮点击
    public const string ad_AddPointPlatform_success = "ad_BossPlatform_success";  //加分跳台翻倍领奖页面【加分跳台翻倍奖励】成功拉起广告
    public const string ad_AddPointPlatform_complete = "ad_BossPlatform_complete";  //加分跳台翻倍领奖页面【加分跳台翻倍奖励】完整看完广告
    public const string ad_AmazonCardPlatform_show = "ad_AmazonCardPlatform_show";//亚马逊卡跳台翻倍领奖页面【皮肤碎片跳台】按钮展示
    public const string ad_AmazonCardPlatform_click = "ad_AmazonCardPlatform_click";//亚马逊卡跳台翻倍领奖页面【皮肤碎片跳台】按钮点击 
    public const string ad_AmazonCardPlatform_success = "ad_AmazonCardPlatform_success";//亚马逊卡跳台翻倍领奖页面【皮肤碎片跳台】成功拉起广告
    public const string ad_AmazonCardPlatform_complete = "ad_AmazonCardPlatform_complete";//亚马逊卡跳台翻倍领奖页面【皮肤碎片跳台】完整看完广告 
    #region 技能
    public const string ad_Buff_show = "ad_Buff_show";          //狂暴道具广告按钮展示
    public const string ad_Buff_click = "ad_Buff_click";         //狂暴道具广告按钮点击
    public const string ad_Buff_success = "ad_Buff_success";       //狂暴道具成功拉起广告
    public const string ad_Buff_complete = "ad_Buff_complete";      //狂暴道具完整看完广告
    public const string Buff_FreeUse = "Buff_FreeUse_{0}";      //狂暴道具第1,2,3次免费试用人次
    #endregion


    #region 抽卡

    public const string ad_PickCard_show = "ad_PickCard_show";//抽卡页面广告按钮展示（全部展示算1次）
    public const string ad_PickCard_click = "ad_PickCard_click";//抽卡页面广告按钮点击
    public const string ad_PickCard_success = "ad_PickCard_success";//抽卡页面成功拉起广告
    public const string ad_PickCard_complete = "ad_PickCard_complete";//抽卡页面完整看完广告
    #endregion

    public const string sign_signin = "sign_signin{0}";  //通过第1天签到人数

    #endregion

    #region 实物兑换

    public const string Entity_50000Gold_count = "Entity_50000Gold_count";                      //50000金币成功兑换人次/人数
    public const string Entity_300USD_count = "Entity_300USD_count";                      //50000金币成功兑换人次/人数 

    public const string ad_GiftBubble_show = "ad_GiftBubble_show";                              //实物兑换气泡广告按钮展示
    public const string ad_GiftBubble_click = "ad_GiftBubble_click";                            //实物兑换气泡广告按钮点击
    public const string ad_GiftBubble_success = "ad_GiftBubble_success";                        //实物兑换气泡成功拉起广告
    public const string ad_GiftBubble_complete = "ad_GiftBubble_complete";                      //实物兑换气泡完整看完广告

    public const string ad_GiftSpin_show = "ad_GiftSpin_show";                                  //实物兑换转盘广告按钮展示
    public const string ad_GiftSpin_click = "ad_GiftSpin_click";                                //实物兑换转盘广告按钮点击
    public const string ad_GiftSpin_success = "ad_GiftSpin_success";                            //实物兑换转盘成功拉起广告
    public const string ad_GiftSpin_complete = "ad_GiftSpin_complete";                          //实物兑换转盘完整看完广告

    public const string ad_GifRefresh_show = "ad_GiftRefresh_show";                                  //实物兑换转盘广告按钮展示
    public const string ad_GifRefresh_click = "ad_GiftRefresh_click";                                //实物兑换转盘广告按钮点击
    public const string ad_GifRefresh_success = "ad_GiftRefresh_success";                            //实物兑换转盘成功拉起广告
    public const string ad_GifRefresh_complete = "ad_GiftRefresh_complete";                          //实物兑换转盘完整看完广告

    public const string ad_GiftBalloon_show = "ad_GiftBalloon_show";                            //实物兑换转盘广告按钮展示
    public const string ad_GiftBalloon_click = "ad_GiftBalloon_click";                          //实物兑换转盘广告按钮点击
    public const string ad_GiftBalloon_success = "ad_GiftBalloon_success";                      //实物兑换转盘成功拉起广告
    public const string ad_GiftBalloon_complete = "ad_GiftBalloon_complete";                    //实物兑换转盘完整看完广告

    #endregion

    #region Raffle
    public const string ad_Raffle_show = "ad_Raffle_show";
    public const string ad_affle_click = "ad_affle_click";
    public const string ad_Raffle_success = "ad_Raffle_success";
    public const string ad_Raffle_complete = "ad_Raffle_complete";

    #endregion

    #region 刮刮卡
    public const string ad_ScratchCard_show = "ad_ScratchCard_show";
    public const string ad_ScratchCard_click = "ad_ScratchCard_click";
    public const string ad_ScratchCard_success = "ad_ScratchCard_success";
    public const string ad_ScratchCard_complete = "ad_ScratchCard_complete";
    #endregion

    #region 小游戏
    public const string ad_Slots_show = "ad_Slots_show";//老虎机翻倍奖励广告按钮展示
    public const string ad_Slots_click = "ad_Slots_click";//老虎机翻倍奖励广告按钮点击
    public const string ad_Slots_success = "ad_Slots_success";//老虎机翻倍奖励成功拉起广告
    public const string ad_Slots_complete = "ad_Slots_complete";//老虎机翻倍奖励完整看完广告
    public const string ad_Dice_show = "ad_Dice_show";//新用户丢骰子翻倍奖励广告按钮展示
    public const string ad_Dice_click = "ad_Dice_click";//老用户丢骰子翻倍奖励广告按钮点击 
    public const string ad_Dice_success = "ad_Dice_success";//丢骰子翻倍奖励成功拉起广告
    public const string ad_Dice_complete = "ad_Dice_complete";//丢骰子翻倍奖励完整看完广告
    public const string ad_Spin_show = "ad_Spin_show"; //转转盘翻倍奖励广告按钮展示
    public const string ad_Spin_click = "ad_Spin_click";//转转盘翻倍奖励广告按钮点击
    public const string ad_Spin_success = "ad_Spin_success";//转转盘翻倍奖励成功拉起广告
    public const string ad_Spin_complete = "ad_Spin_complete";//转转盘翻倍奖励完整看完广告 
    public const string ad_Piano_3chances_show = "ad_Piano_3chances_show";//九宫格3次机会广告按钮展示
    public const string ad_Piano_3chances_click = "ad_Piano_3chances_click";//九宫格3次机会广告按钮点击
    public const string ad_Piano_3chances_success = "ad_Piano_3chances_success"; //九宫格3次机会成功拉起广告
    public const string ad_Piano_3chances_complete = "ad_Piano_3chances_complete";//九宫格3次机会完整看完广告
    public const string ad_Piano_adCard_show = "ad_Piano_adCard_show";              //九宫格广告卡片广告按钮展示
    public const string ad_Piano_adCard_click = "ad_Piano_adCard_click";              //九宫格广告卡片广告按钮点击
    public const string ad_Piano_adCard_success = "ad_Piano_adCard_success";           //九宫格广告卡片成功拉起广告
    public const string ad_Piano_adCard_complete = "ad_Piano_adCard_complete";            //每日任务双倍广告失败


    #endregion

    #region 假提现
    public const string cashout_Cash_mission = "cashout_Cash{0}_mission{1}";
    public const string cashout_AmazonCard_mission = "cashout_AmazonCard{0}_mission{1}";
    public const string cashout_Gold_mission = "cashout_Gold{0}_mission{1}";
    #endregion
}

//游戏中所有的BI广告名称
public class ADStructInfoName
{
    public const string Reward_NewHighScore = "1001";//新高分翻倍奖励激励点
    public const string Reward_AddPoint_Platform = "1002";//Boss敌人翻倍奖励激励点
    public const string Reward_PickCard_Platform = "1003";//抽卡敌人翻牌奖励激励点
    public const string Reward_AmazonCard_Platform = "1004";//亚马逊卡敌人翻倍奖励激励点
    public const string Reward_Slots_Platform = "1005";          //老虎机敌人翻倍奖励激励点   
    public const string Reward_Turnplate_Platform = "1006";//转转盘敌人翻倍奖励激励点
    public const string Reward_Dice_Platform = "1007";//扔骰子敌人翻倍奖励激励点
    public const string Reward_Piano3Chances_Platform = "1008";//钢琴敌人+3张翻牌奖励激励点
    public const string Reward_PianoADCard_Platform = "1009";//钢琴敌人直接翻牌奖励激励点
    public const string Reward_EntityBubble = "1010";//实物兑换气泡激励点
    public const string Reward_EntitySpin = "1011";//实物兑换转转盘激励点
    public const string Reward_EntityRefresh = "1012";//实物兑换转盘刷新激励点
    public const string Reward_EntityBalloon = "1013";//首页悬浮气球激励点
    public const string Reward_Buff = "1015";//Buff道具激励点
    public const string Reward_Scratch = "1016";//刮刮卡激励点
    public const string Reward_Raffer = "1017";


    public const string Inter_AllReward = "1101";
    public const string Inter_EntityReturn = "1102";

    public const string Banner = "1201";

}

//红点系统通知Key
public class RedPointKey
{
    public const string CheckRedPoint = "CheckRedPoint";//签到红点
}

//配置路径
public class ConfigPath
{
    public const string AssetPathBase = "Config/AssetConfig/";
    public const string XmlPathBase = "Assets/Resources/Config/XmlConfig/";
    public const string ExcelPathBase = "/Resources/Config/ExcelConfig/";

    public const string ConstData = AssetPathBase + "ConstData";
    public const string LanguageConfig = AssetPathBase + "LocalConfig";


    public const string GameMainConfig = XmlPathBase + "GameMainConfig.xml";
    public const string CheckConfig = XmlPathBase + "CheckConfig.xml";
    public const string TrunplateConfig = XmlPathBase + "TrunplateConfig.xml";
    public const string SlotsConfig = XmlPathBase + "SlotsConfig.xml";
    public const string ThreeDiceConfig = XmlPathBase + "ThreeDiceConfig.xml";
    public const string DicePhysicConfig = XmlPathBase + "DicePhysicConfig.xml";
    public const string PianoConfig = XmlPathBase + "PianoConfig.xml";

    public const string EntityReddemConfig = XmlPathBase + "EntityReddemConfig.xml";
    public const string CashOutConfig = XmlPathBase + "CashOutConfig.xml";

    public const string LuckyBoxConfig = XmlPathBase + "LuckyBoxConfig.xml";

    public const string RaffleConfig = XmlPathBase + "RaffleConfig.xml";
    public const string ScratchConfig = XmlPathBase + "ScratchConfig.xml";


}

//游戏中的层级
public class ObjectLayMask
{
    public const int UI = 5;
    public const int River = 6;
    public const int Enemy = 7;
    public const int Bullet = 8;
    public const int Dice = 9;
    public const int DicePanel = 10;
    public const int Wall = 11;
    public const int ReleaseWall = 12;


}

//游戏中所有的音效地址（绝对目录）
public class MusicPath
{
    public const string PathBase = "Assets/Resources/Musics/";
    public const string UIBase = PathBase + "UI/";
    public const string MiniGame = PathBase + "Game/";


    public const string BGM = "Assets/Resources/Musics/BGM/BGM.mp3";

    public const string ButtonClickAudio = UIBase + "BtnClick.mp3";
    public const string RewardIncrease = UIBase + "RewardIncrease.mp3";
    public const string Card = UIBase + "CardFlip.mp3";
    public const string FlipCardReward_01 = UIBase + "CardFlipReward.mp3";
    public const string Toast = UIBase + "Toast.mp3";
    public const string PrizeWheelSpin_Last = UIBase + "PrizeWheelSpin_Last.mp3";
    public const string PrizeWheelSpin_Loop = UIBase + "PrizeWheelSpin_Loop.mp3";
    public const string RewardxN = UIBase + "RewardxN.mp3";
    #region 主玩法
    public static string[] Hit = new string[]
    {
        MiniGame + "Hit_1.mp3",
        MiniGame + "Hit_2.mp3",
        MiniGame + "Hit_3.mp3"
    };

    public static string[] ThrowDarts = new string[]
    {
        MiniGame + "ThrowDarts_1.mp3",
        MiniGame + "ThrowDarts_2.mp3",
        MiniGame + "ThrowDarts_3.mp3"
    };
    public const string BossIncoming = MiniGame + "BossIncoming.mp3";

    #endregion
    #region 实物兑换
    public const string FlyCoin = UIBase + "FlyCoin.mp3";
    public const string FlyAmazon = UIBase + "FlyAmazon.mp3";
    public const string FlyCash = UIBase + "FlyCash.mp3";
    public const string FlyFragment = UIBase + "FlyFragment.mp3";
    
    #endregion
    #region 刮刮卡
    public const string NoReward = UIBase + "NoReward.mp3";
    public const string ScratchCard = UIBase + "ScratchCard.mp3";
    #endregion

    #region 小游戏
    public const string DiceFlyAudio = MiniGame + "Main_Click_DiceFly.mp3";//骰子飞出
    public const string DiceArriveAudio1 = MiniGame + "Main_Click_DiceArrive2.wav";//骰子落地

    public const string SlotsRunAudio = MiniGame + "Game_Slots_Run.mp3";//Slots开始转动   (逻辑处理分断)
    public const string SlotsStopAudio = MiniGame + "Game_Slots_Stop.mp3";//Slots转动停止  
    public const string RouletteRunAudio = MiniGame + "Game_Roulette_Run.mp3";//轮盘开始转动
    public const string RouletteStopAudio = MiniGame + "Game_Slots_Stop.mp3";//轮盘转动结束

    #endregion
}

//预制体地址
public class PrefabsPath
{
    public const string EnemiesPath = "Prefabs/GameMain/Enemies/";
    public const string BulletsPath = "Prefabs/GameMain/Bullets/";
    public const string EffectPoolPath = "Prefabs/Effects/";
    public const string FlyAsset = "Prefabs/UI/Pool/";
}

//事件key值
public class EventKey
{
    public const string UpdateAsset = "UpdateAsset";//资源有变动时刷新
    public const string Turnplate_BallStop = "Turnplate_BallStop";//转盘小球停下
}

//UI路径地址
public class UIResourcePath
{
    public const string PathBase = "Prefabs/UI/";
    public const string UISimpleMsg = PathBase + "UISimpleMsg";

    public const string UIGameMain = PathBase + "UIGameMain";
    public const string UIPlayInsert = PathBase + "UIPlayInsert";

    public const string UIEntityReddemPanel = PathBase + "UIEntityReddemPanel/UIEntityReddemPanel";
    public const string UIEntityAwardPanel = PathBase + "UIEntityReddemPanel/UIEntityAwardPanel";
    public const string UIEntityRulePanel = PathBase + "UIEntityReddemPanel/UIEntityRulePanel";

    public const string UIBalloonBoxPanel = PathBase + "UIBalloonBoxPanel";

    public const string UICashOutPanel = PathBase + "UICash/UICashOutPanel";
    public const string UIDollarRecord = PathBase + "UICash/UIDollarRecord";
    public const string UIDollarGamePlay = PathBase + "UICash/UIDollarGamePlay";
    public const string UICashEmailPanel = PathBase + "UICash/UICashEmailPanel";



    public const string UICoinChancePanel = PathBase + "UICoinChancePanel";
    public const string UICardPanel = PathBase + "UICardPanel";

    public const string UIRafflePanel = PathBase + "UIRaffle/UIRafflePanel";
    public const string UIRaffleHelpInfo = PathBase + "UIRaffle/UIRaffleHelpInfo";


    public const string UIScratchCardPanel = PathBase + "UIScratchCardPanel";
    public const string UITouristPanel = PathBase + "UITouristPanel";

    public const string UIDiceAward = PathBase + "UIDiceAward";
    public const string UINineCard = PathBase + "UINineCard";
    public const string UIMiniGame = PathBase + "UIMiniGame";
    public const string UIAniLoad = PathBase + "UIAniLoad";
    public const string UISettingPanel = PathBase + "UISettingPanel";
    public const string UICheck = PathBase + "UICheck";
    public const string UITateUs = PathBase + "UITateUs";

    public const string UIGuidePanel = PathBase + "UIGuidePanel";
    public const string UIWG = PathBase + "UIWG"; 


}

public class HttpPostHandle
{
    public const string BICfgHandle = "server/app_conf";//BI网赚广告配置
    public const string FirstBILogin = "api/login";//首次登录

}
