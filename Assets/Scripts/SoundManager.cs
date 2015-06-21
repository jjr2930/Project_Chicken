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
            Debug.Log("해당 사운드 클립을 찾을수 없어요 확인해주세요");
            return;
        }

        if (null == _BGM)
        {
            _BGM = MakeNewSoundObject(selectedClip, true);
            _BGM.name = "BGM";
        }

        _BGM.gameObject.SetActive(true);
    }

    public void PlaySound(string name)
    {
        var list = _sounds.Where(a => a.gameObject.activeSelf == false).ToArray();
        SoundElement element = null;
        AudioClip selectedClip = null;
        //없으면 만들어야지
        if (0 == list.Length)
        {
            var clips = _clipList.Where(a => a.name == name).ToArray();

            if (clips.Length <= 0)
            {
                Debug.LogError("해당 클립이 존재하지 않아요 확인해주세요");
                return;
            }

            selectedClip = clips[0];

            var newObj = MakeNewSoundObject(selectedClip);

            _sounds.Add(newObj);

            element = newObj;
        }

        //있다면 클립만 찾아주자
        if(null == selectedClip)
        {
            var cliplist = _clipList.Where(a => a.name == name).ToArray();
            if (null == cliplist)
            {
                Debug.LogError("해당 클립이 없어요 확인해 주세요");
                return;
            }
            selectedClip = cliplist[0];
            element = list[0];
        }

        element.SetAudio(selectedClip);

        element.gameObject.SetActive(true);
    }

    public void StopSound(string name)
    {
        var searchedList = _sounds.Where(a => a.Clip.name == name && a.gameObject.activeSelf == true).ToArray();
        if (searchedList.Length <= 0)
        {
            Debug.LogError("해당이름의 사운드는 실행되고 있지 않습니다.");
            return;
        }

        SoundElement stopNode = searchedList[0];

        stopNode.gameObject.SetActive(false);

    }
    public SoundElement MakeNewSoundObject(AudioClip clip, bool isBGM = false)
    {
        GameObject newObj = new GameObject("SoundElement");

        newObj.SetActive(false);

        var script = newObj.AddComponent<SoundElement>();

        script.SetAudio(clip,isBGM);

        return script;
    }

    void Update()
    {
        if (_sounds.Count <= 0)
        {
            return;
        }
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
                    Destroy(oldNode.gameObject);
                }
                count++;
                if (_sounds.Count == 0)
                {
                    _iCheckedLastIndex = 0;
                    Debug.Log("have not element");
                    return;
                }
                _iCheckedLastIndex = (_iCheckedLastIndex + 1) % _sounds.Count;
            }
        }
    }
}