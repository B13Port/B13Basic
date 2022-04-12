using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace B13Port.Common
{
    public class BasetData
    {
        public string info;
        public int status;
        public string data;
    }

    #region CallBackStruct

    #region BiCfgFarm
    public class BiCfgFarm
    {
        public BIDataConfig data;
    }

    public class BIDataConfig
    {
        public int BINeoBuxSwitc;
        public InsertADConfig[] insertADCfg;
    }

    public class InsertADConfig
    {
        public string cfgName;
        public int noneADTime;
        public int AdTimeMinRange;
        public int AdTimeMaxRange;
        public float inMinRangeRate;
        public float inMidRangeRate;
        public float inMaxRangeRate;
    }
    #endregion

    #region LoginBack
    public class LoginNetData
    {
        public int user_id;
        public string authorization;
        public string date;
        public string country;
        public int country_code;
        public int login_days;
        public int is_organic;
    }

    #endregion

    #endregion


    #region PostStruct
    public class PhoneData
    {
        public string name;
        public string os;
    }
    public class BILoginData
    {
        public string bundle_id;
        public string udid;
        public string afid;
        public string version = "0";
        public string device_info;
        public string client_id;
        public string timestamp;
        public string signature;
    }
    #endregion







}
