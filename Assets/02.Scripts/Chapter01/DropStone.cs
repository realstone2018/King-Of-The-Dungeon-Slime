using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropStone : MonoBehaviour {

    public GameObject explosion;

    private AudioSource source = null;
    public AudioClip dropAudio;
    public float volume = 0.2f;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // 종유석이 Map에 충돌시 낙호 오디오클립 출력 
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == 14)
        {
            //Debug.Log("Collider 14 ");
            source.PlayOneShot(dropAudio, volume);
            StartCoroutine(ExplosionTriggerOn());
        }
    }

    // explosion 트리거 잠시 생성했다가 삭제,  슬라임들은 이 트리거에 충돌시 HP 감소 
    IEnumerator ExplosionTriggerOn()
    {
        explosion.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        explosion.SetActive(false);
    }
}
