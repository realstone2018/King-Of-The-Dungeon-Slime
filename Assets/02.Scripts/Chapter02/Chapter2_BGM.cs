using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter2_BGM : MonoBehaviour {

    private AudioSource source = null;
    public AudioClip stage02_BGM;
    public AudioClip stage02_BGM2;

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
            source.PlayOneShot(stage02_BGM, 0.15f);
            yield return new WaitForSecondsRealtime(170f);
        }
    }

    IEnumerator playBGM2()
    {
        yield return new WaitForSeconds(3.0f);

        while (true)
        {
            source.PlayOneShot(stage02_BGM2, 0.1f);
            yield return new WaitForSecondsRealtime(62f);
        }
    }
}
