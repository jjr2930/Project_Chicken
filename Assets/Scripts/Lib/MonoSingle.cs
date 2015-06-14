using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonoSingle<T> : MonoBehaviour where T:MonoBehaviour
{
    static public T Instance
    {
        get
        {
            if (null == _instance)
            {
                //find this object
                _instance = Object.FindObjectOfType<T>();

                if (null == _instance)//fail to found, make this class
                {
                    string name = typeof(T).ToString();
                    var obj = new GameObject(name);
                    _instance = obj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }
    static private T _instance;

    private bool _bIsInit = false;

    public void Init()
    {
        if (!_bIsInit)
        {
            OnInit();
        }
        _bIsInit = true;        
    }

    protected virtual void OnInit()
    {

    }
}
