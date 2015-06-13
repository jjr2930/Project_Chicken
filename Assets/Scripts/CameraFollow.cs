using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public GameObject chickenPlayer;
	Vector3 shouldPos;

	void Update () {
		shouldPos = Vector3.Lerp (gameObject.transform.position, new Vector3(chickenPlayer.transform.position.x, chickenPlayer.transform.position.y,chickenPlayer.transform.position.z), Time.deltaTime);
		gameObject.transform.position = new Vector3 (shouldPos.x, 3, shouldPos.z);
	}
}
