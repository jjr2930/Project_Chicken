using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// const is in here
/// </summary>
public class MACRO
{
    public const string KEY_CASH = "cash";
    public const string KEY_STAGEPOINT = "stagePoint";

    static public T[] GetComponentsFromArray<T>(GameObject[] originArray)
    {
        List<T> list = new List<T>();
        for(int i = 0; i< originArray.Length; i++)
        {
            T founded = originArray[i].GetComponent<T>();

            if (null == founded)
            {
                Debug.LogError("하나 배열요소에서 컴포넌트를 찾지 못했습니다.");
            }

            else
            {
                list.Add(originArray[i].GetComponent<T>());
            }
        }

        return list.ToArray();
    }
}

