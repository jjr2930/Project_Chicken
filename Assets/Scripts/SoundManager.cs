using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// <para>사용법 프로퍼티 instance를 통하여 객체를 얻는다.</para>
/// <para>BGM 플레이 : PlayBGM(사운드 클립이름)을 이용하여 플레이 한다.</para>
/// <para>일반 사운드 플레이 : PlaySound(사운드 이름)을 이용하여 플레이 한다.</para>
/// </summary>
public class SoundManager : MonoSingle<SoundManager>
{
    public float _fDeleteTime = 10.0f;
    public float _fCheckIntervalTime = 1.0f;
    public float _iLimitIndexForm1Cycle = 10;
    public AudioClip[] _clipList = null;
    public AudioClip[] _bgmClip = null;

    private List<SoundElement> _sounds = new List<SoundElement>();
    private SoundElement _BGM = null;
    private float _fStartTime = 0.0f;
    private int _iCheckedLastIndex = 0;

    protected override void OnInit()
    {
        base.OnInit();
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayBGM(string BGMName)
    {
        var selectedClip = _bgmClip.Where(a => a.name == BGMName).ToArray()[0];
        if (null == selectedClip)
        {

        }

        if (null == _BGM)
        {
            var BGM = MakeNewSoundObject(selectedClip, true);
        }

        _BGM.gameObject.SetActive(true);
    }

    public void PlaySound(string name)
    {
        var selected = _sounds.Where(a => a.name == name && a.gameObject.activeSelf == false).ToArray()[0];

        //없으면 만들어야지
        if (null == selected)
        {
            var clip = _clipList.Where(a => a.name == name).ToArray()[0];

            var newObj = MakeNewSoundObject(clip);

            _sounds.Add(newObj);

            selected = newObj;
        }

        var selectedClip = _clipList.Where(a => a.name == name).ToArray()[0];

        selected.SetAudio(selectedClip);

        selected.gameObject.SetActive(true);
    }

    public SoundElement MakeNewSoundObject(AudioClip clip, bool isBGM = false)
    {
        GameObject newObj = new GameObject("SoundElement");

        var script = newObj.AddComponent<SoundElement>();

        script.SetAudio(clip,isBGM);

        return script;
    }

    void Update()
    {
        if (Time.time - _fStartTime >= _fCheckIntervalTime)
        {
            int count = 0;
            while (count < _iLimitIndexForm1Cycle)
            {
                var selectedNode = _sounds[_iCheckedLastIndex];
                if (Time.time - selectedNode.DeactivateTime >= _fDeleteTime)
                {
                    var oldNode = selectedNode;
                    _sounds.RemoveAt(_iCheckedLastIndex);
                    Destroy(oldNode);
                }
                count++;
                _iCheckedLastIndex = (_iCheckedLastIndex + 1) % _sounds.Count;
            }
        }
    }
}