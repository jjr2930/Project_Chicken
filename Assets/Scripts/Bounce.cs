using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour
{
    float lerpTime;
    float currentLerpTime;
    float perc = 1;

    public Vector3 startPos;
    public Vector3 endPos;

    bool firstInput;
    public bool justJump;

    void Start()
    {
        startPos = gameObject.transform.position;
        endPos = gameObject.transform.position;
    }

    void Update()
    {

        if (Input.GetButtonDown("up") || Input.GetButtonDown("down") || Input.GetButtonDown("left") || Input.GetButtonDown("right")
         || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            if (perc == 1)
            {
                lerpTime = 1;
                currentLerpTime = 0;
                firstInput = true;
                justJump = true;
            }
        }
        startPos = gameObject.transform.position;

        if ((Input.GetButtonDown("right") || Input.GetKeyDown(KeyCode.D)) && gameObject.transform.position == endPos)
        {
            if (endPos.x > 5.9) return;
            endPos = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        }
        if ((Input.GetButtonDown("left") || Input.GetKeyDown(KeyCode.A)) && gameObject.transform.position == endPos)
        {
            if (endPos.x < 0.1) return;
            endPos = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        }
        if ((Input.GetButtonDown("up") || Input.GetKeyDown(KeyCode.W)) && gameObject.transform.position == endPos)
        {
            if (endPos.z > 5.9) return;
            endPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
        }
        if ((Input.GetButtonDown("down") || Input.GetKeyDown(KeyCode.S)) && gameObject.transform.position == endPos)
        {
            if (endPos.z < 0.1) return;
            endPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        }

        if (firstInput == true)
        {
            currentLerpTime += Time.deltaTime * 7;
            perc = currentLerpTime / lerpTime;
            gameObject.transform.position = Vector3.Lerp(startPos, endPos, perc);
            if (perc > 0.8)
            {
                perc = 1;
            }
            if (Mathf.Round(perc) == 1)
            {
                justJump = false;
            }
        }
    }
}
