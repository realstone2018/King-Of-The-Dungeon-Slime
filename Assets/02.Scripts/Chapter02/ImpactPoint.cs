using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactPoint : MonoBehaviour {
    public GameObject dust;
    public GameObject explosionTrigger;

    private AudioSource source = null;
    public AudioClip dropAudio;
    public float volume = 0.2f;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "DropStake")
        {
            dust.SetActive(true);
            source.PlayOneShot(dropAudio, volume);
            StartCoroutine(ExplosionTriggerOn());
        }
    }

    IEnumerator ExplosionTriggerOn()
    {
        explosionTrigger.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        explosionTrigger.SetActive(false);
    }
}
