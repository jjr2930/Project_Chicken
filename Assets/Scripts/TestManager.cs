using UnityEngine;
using System.Collections;

public class TestManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SoundManager.Instance.PlayBGM("Broken");
	}

    // Update is called once per frame
    void OnGUI()
    {
        if (GUILayout.Button("sound1"))
        {
            SoundManager.Instance.PlaySound("Attack");
        }

        if (GUILayout.Button("sound2"))
        {
            SoundManager.Instance.PlaySound("TargetPoint");
        }

        if (GUILayout.Button("AxeAction"))
        {
            var tileRoot = GameObject.Find("GroundTile(Clone)");
            var tiles = tileRoot.GetComponentsInChildren<MeshRenderer>();
            //find rendom tile
            int rnd = Random.Range(0, tiles.Length);

            var selectedTile = tiles[rnd];

            IGEffectMngr.Instance.GenerateAxePosImg(selectedTile.transform.position, 2.0f);

            SoundManager.Instance.PlaySound("TargetPoint");
        }

    }
}
