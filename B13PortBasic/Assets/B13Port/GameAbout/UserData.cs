using B13Port.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class UserData : Singletion<UserData>
{
    public PlayerData playerData;

    public UserData()
    {
        playerData = new PlayerData(DefineVales.PlayerDataKey);
    }

    public void InitUserData()
    {
    }

   
}
