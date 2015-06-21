using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>이 클래스는 사운드 메니저를 통해 관리되는 클래스이다.</para>
/// <para>GameObject.SetActive()함수를 통해 켜주면 자동으로 플레이 된다.</para>
/// </summary>
public class SoundElement : MonoBehaviour
{
    public AudioClip Clip { get; set; }       //soiurce
    private AudioSource _audioPlayer = null;    //player
    public float DeactivateTime    { set; get; }


    public void SetAudio(AudioClip clip, bool IsBGM = false)
    {
        Debug.Log("setauido");
        _audioPlayer = this.GetComponent<AudioSource>();
        if (null == _audioPlayer)
        {
            _audioPlayer = this.gameObject.AddComponent<AudioSource>();
        }

        if (IsBGM)
        {
            _audioPlayer.loop = true;
        }

        Clip = clip;
        _audioPlayer.clip = Clip;
    }
    void Awake()
    {
        DeactivateTime = Time.time;
    }

    void OnEnable()
    {
        _audioPlayer.Play();
    }

    void Update()
    {
        if (!_audioPlayer.isPlaying)
        {
            this.gameObject.SetActive(false);
            DeactivateTime = Time.time;
        }
    }
}
    

