using UnityEngine;
using System.Collections;

public class IGUIPointMngr : MonoSingle<IGUIPointMngr> {
    [SerializeField]
    private UILabel _score;
    [SerializeField]
    private UILabel _cash;

    
    void Update()
    {
        string temp = string.Format("Score:{0}", PlayerPrefs.GetInt(MACRO.KEY_STAGEPOINT));
        _score.text = temp;

        temp = string.Format("Cash:{0}", PlayerPrefs.GetInt(MACRO.KEY_CASH));
        _cash.text = temp;
    }
}
