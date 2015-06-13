using UnityEngine;
using System.Collections;

/// <summary>
/// 1. 초기 맵 생성 5*5
/// </summary>

public class MapGenerator : MonoBehaviour {

    public GameObject Map_Root;
    public GameObject[] Map_Tile;

    void Awake()
    {
        Map_Root = GameObject.Find("Map");
    }

	void Start ()
    {
		초기맵생성 ();
    }

	void 초기맵생성()
	{
        int tileNum = 0;

		for (int j = 0; j <= 6; j++) {
			for (int i = 0; i <= 6; i++) {
				GameObject NormalMap = Instantiate (Resources.Load ("GroundTile"), new Vector3 (i, 0, j), Quaternion.identity) as GameObject;
				NormalMap.transform.parent = Map_Root.transform;

                Map_Tile[tileNum++] = NormalMap;
			}
		}
	}
}
