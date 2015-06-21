using UnityEngine;
using System.Collections;

public class logofade : MonoBehaviour
{
    public GameObject 텍스쳐;

    void Start()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(.5f);
        TweenAlpha.Begin(텍스쳐.gameObject, 2f, 1f);
        yield return new WaitForSeconds(4f);

        TweenAlpha.Begin(텍스쳐.gameObject, 2f, 0f);
        yield return new WaitForSeconds(2.2f);

        NextSceneCall();
    }

    void NextSceneCall()
    {
        Application.LoadLevel("2_GameScene");
    }
}