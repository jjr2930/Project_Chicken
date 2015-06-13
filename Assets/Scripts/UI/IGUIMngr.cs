using UnityEngine;
using System.Collections;

public class IGUIMngr : MonoSingle<IGUIMngr>
{
    [SerializeField]
    IGUIPointMngr _pointMngr;

    protected override void OnInit()
    {
        base.OnInit();
        _pointMngr = GetComponentInChildren<IGUIPointMngr>();
    }

    void OnGUI()
    {
        if (GUILayout.Button("reset"))
        {
            PlayerPrefs.SetInt(MACRO.KEY_CASH, 0);
            PlayerPrefs.SetInt(MACRO.KEY_STAGEPOINT, 0);

            PlayerPrefs.Save();
        }
    }

   
}
