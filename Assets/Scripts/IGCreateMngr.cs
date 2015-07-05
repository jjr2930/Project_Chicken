using UnityEngine;
using System;
using System.Linq;
using System.Collections;

public class IGCreateMngr : MonoSingle<IGCreateMngr>
{

    [SerializeField]    int _iTotalAxe = 0; //현재까지 도끼의 공격횟수
    [SerializeField]    float _fLastAxeSpawnTime = 0f; //가작 최근에 도끼가 공격했던 시간.
    [SerializeField]    float _fAxeSpawnInterval = 0f; //도끼의 출현 인터벌
    [SerializeField]    float _fItemSpawnInterval = 0f; //아이템의 출현 인터벌
    [SerializeField]    float _fLastItemSpawnTime = 0f; //가장 최근의 아이템이 출현한 시간.

    [SerializeField]    bool _bAxeStart = false;
    [SerializeField]    bool _bItemStart = false;

    Transform[] _tiles = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        ItemMakeTimer();
        AxeMakeTimer();

	}

    void ItemMakeTimer()
    {
        if (!_bItemStart)
        {
            return;
        }

        if (Time.time - _fLastItemSpawnTime >= _fItemSpawnInterval)
        {
            MakeItem();
            _fLastItemSpawnTime = Time.time;
        }
    }

    void AxeMakeTimer()
    {
        if (!_bAxeStart)
            return;

        if (Time.time - _fLastAxeSpawnTime >= _fAxeSpawnInterval)
        {
            MakeAxe();
            _fLastAxeSpawnTime = Time.time;
            SetAxeInterval();
        }
    }

    void MakeAxe()
    {
        //find rendom tile
        int rnd = UnityEngine.Random.Range(0, _tiles.Length);
        IGEffectMngr.Instance.GenerateAxePosImg(_tiles[rnd].position, 2.0f);
        SoundManager.Instance.PlaySound("TargetPoint");
    }

    void MakeItem()
    {
        int rnd = UnityEngine.Random.Range(0, _tiles.Length);
        GameObject coin = Instantiate(Resources.Load("Prefabs/Coin"), _tiles[rnd].transform.position, Quaternion.identity) as GameObject;
        coin.AddComponent
        SoundManager.Instance.PlaySound("TargetPoint");
    }
    
    void SetAxeInterval()
    {
        _fAxeSpawnInterval = Time.time / Mathf.Pow((float)_iTotalAxe, 2);
    }

    protected override void OnInit()
    {
        base.OnInit();
        var tileRoot = GameObject.Find("GroundTile(Clone)");
        _tiles = tileRoot.GetComponentsInChildren<Transform>();
    }
}
