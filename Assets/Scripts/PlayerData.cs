using UnityEngine;
using System.Collections;

public class PlayerData
{
    #region property
    public int CurrentStageScore
    {
        get
        {
            return SecurePlayerPref.GetInt(MACRO.KEY_STAGEPOINT);
        }
        set
        {
            SecurePlayerPref.SetInt(MACRO.KEY_STAGEPOINT, value);
        }
    }

    public int Cash
    { 
        get
        {
            return SecurePlayerPref.GetInt(MACRO.KEY_CASH);
        }

        set
        {
            SecurePlayerPref.SetInt(MACRO.KEY_CASH, value);
        }
    }

    static public PlayerData Instance
    {
        get
        {
            if (null == _instance)
            {
                _instance = new PlayerData();
            }
            return _instance;
        }
    }

    #endregion



    private PlayerData()
    {
        Init();
    }
    private void Init()
    {
                
    }

    static private PlayerData _instance;

}
