using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
class Coin : MonoBehaviour
{
    float _destroyTime = 5.0f;
    bool _isCrash = false;
    void Start()
    {
        Invoke("InvokeDestroy", _destroyTime);
    }

    void OnCollisionEnter(Collider other)
    {
        //먹어버린 처리를 해준다.
        if ("Player" == other.tag)
        {
            _isCrash = true ;
            this.GetComponent<Collider>().enabled = false;
            this.GetComponent<Animator>().SetTrigger("Eaten");
            Destroy(this.gameObject, 2.0f);
        }
    }

    void InvokeDestroy()
    {
        if (_isCrash)
        {
            return;
        }

        Destroy(this.gameObject);
    }
}

