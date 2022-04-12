using B13Port.Common;

public class PlayerData : SStruct
{
    public static UnityStorage unityStorage;
    public PlayerData(string key)
    {
        if (unityStorage == null) unityStorage = new UnityStorage();
        Init(unityStorage, key);
    }
}