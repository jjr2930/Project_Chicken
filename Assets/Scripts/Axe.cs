using UnityEngine;
using System.Collections;

public class Axe : MonoSingle<Axe> {
    public int ATKID
    {
        get;
        set;
    }  //콜리전은 느려서 체킹이 안되고 트리거는 여러번 일어남 따라서 ID값을 가지고 관리해야함 -1은 없는거임

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("crash collision");
        if (other.transform.name == "chicken")
        {
            Debug.Log("end");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("crash triegger");
        if (other.transform.name == "chicken")
        {
            if (ATKID != -1)
            {
                ATKID = -1;
                Debug.Log("end");
            }

            else
            {
                return;
            }
        }
    }
}
