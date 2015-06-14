using UnityEngine;
using System.Collections;

public class AGEffectMngr : MonoSingle<AGEffectMngr> {
    [SerializeField]
    private Vector3 _axeEndScale = new Vector3(5.0f, 5.0f, 5.0f);

    [SerializeField]
    private GameObject _axeImgPF = null;

    [SerializeField]
    private GameObject _axeEffect = null;

    [SerializeField]
    private GameObject _axe = null;

    protected override void OnInit()
    {
        base.OnInit();
        _axeImgPF = Resources.Load("AxePosImg") as GameObject;
    }

    public void GenerateAxePosImg(Vector3 position, float duration)
    {
        var obj = Instantiate(_axeImgPF, position + Vector3.up * 0.1f, Quaternion.Euler(90.0f,0.0f,0.0f)) as GameObject;
        StartCoroutine(CorAxePosImgAction(obj, duration));
    }

    public void StartAxeAtkAction(Vector3 destination, float duration)
    {
        Axe.Instance.ATKID = 10;
        StartCoroutine(CorAxeMove(destination,duration));
    }

    public void StartAxeAtkEffect(Vector3 position, float duration)
    {
        var obj = Instantiate(_axeEffect, position, Quaternion.Euler(-90.0f,0.0f,0.0f)) as GameObject;
        Destroy(obj, duration);        
    }
    /// <summary>
    /// test code
    /// </summary>
    void OnGUI()
    {
        if (GUILayout.Button("action Start"))
        {
            var tileRoot = GameObject.Find("GroundTile(Clone)");
            var tiles = tileRoot.GetComponentsInChildren<MeshRenderer>();
            //find rendom tile
            int rnd = Random.Range(0, tiles.Length);

            var selectedTile = tiles[rnd];
            
            GenerateAxePosImg(selectedTile.transform.position, 2.0f);
        }
    }

    #region help method
    IEnumerator CorAxePosImgAction(GameObject go, float duration)
    {
        Vector3 nowScale = go.transform.localScale;
        Vector3 destScale = _axeEndScale;
        float startTime = Time.time;

        while (Time.time - startTime <= duration)
        {
            go.transform.localScale = Vector3.Lerp(nowScale, destScale, (Time.time - startTime) / duration);
            yield return null;
        }
        //1.0f is dummy value
        Destroy(go);
        StartAxeAtkAction(go.transform.position + new Vector3(0, 1.06f, 2.6f),0.2f);
    }

    IEnumerator CorAxeMove(Vector3 destination,float duration)
    {
        Vector3 startPosition = destination + Vector3.up * 5.0f;
        float startTime = Time.time;

        while (Time.time - startTime <= duration)
        {
            _axe.transform.position = Vector3.Lerp(startPosition, destination, (Time.time - startTime) / duration);
            yield return null;
        }
        StartAxeAtkEffect(destination - new Vector3(0, 1.06f,2.6f), 2.0f);
    }

    #endregion
}
