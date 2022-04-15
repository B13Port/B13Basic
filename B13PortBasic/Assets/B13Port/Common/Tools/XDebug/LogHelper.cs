public class LogHelper
{
    public const uint B13Port = 0x000001;
    public const uint NetInfo = 0x000004;
    public const uint Test = 0x000008;
    public const uint ICMgr = 0x000016;
    public const uint ADMgr = 0x000032;
    public const uint Editor = 0x0000642;
    public const uint NOLOG = 0;
    public const uint ALLLOG = uint.MaxValue;

    public static void Init(bool isOpen)
    {
        XDebug.AddKeyInfo(B13Port, "B13Port");
        XDebug.AddKeyInfo(ICMgr, "ICMgr");
        XDebug.AddKeyInfo(ADMgr, "ADMgr");
        XDebug.AddKeyInfo(NetInfo, "NetInfo");
        XDebug.AddKeyInfo(Editor, "Editor");
        XDebug.AddKeyInfo(Test, "测试");
        XDebug.SetLogActive(isOpen ? ALLLOG : NOLOG);
    }

    public static void OnGameOver()
    {
        XDebug.SafeReleaseWriteOut();
    }
}