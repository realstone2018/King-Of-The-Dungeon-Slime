using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter01_BGM : MonoBehaviour
{

    private AudioSource source = null;
    public AudioClip stage01_BGM;
    public AudioClip stage01_BGM2;

    void Start()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(playBGM1());
        StartCoroutine(playBGM2());
    }

    IEnumerator playBGM1()
    {
        yield return new WaitForSeconds(10.0f);

        while (true)
        {
            source.PlayOneShot(stage01_BGM, 0.05f);
            yield return new WaitForSecondsRealtime(70f);
        }
    }

    IEnumerator playBGM2()
    {
        yield return new WaitForSeconds(5.0f);

        while (true)
        {
            source.PlayOneShot(stage01_BGM2, 0.2f);
            yield return new WaitForSecondsRealtime(290f);
        }
    }
}
