using UnityEngine;
using System.Collections;

public class DeviceController : MonoBehaviour {

    void Update()
    {
        if (Screen.orientation == ScreenOrientation.LandscapeRight || Screen.orientation == ScreenOrientation.LandscapeLeft)
        {
            Camera.main.orthographicSize = 4.4f;
        }
        else if (Screen.orientation == ScreenOrientation.Portrait)
        {
            Camera.main.orthographicSize = 7f;
        }
    }
}
