using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour {
    public GameObject bullet;
    public Transform firePos;
    public GameObject slime;

    // 조종여부, SelectUnit스크립트에서 조종 
    public bool fireControl;
    // 마우스 클릭한 지점 
    Vector3 hitPosition;
    // 대포 쿨타임 
    public bool coolDown = false;
    public int coolCount;

    public AudioSource source = null;
    public AudioClip fireAudio;

    public GameObject bulletCooltimeImage;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    void Update()
    {
        // 마우스 오른쪽 버튼 클릭시 
        // 해당 방향으로 잠시 돌아보고 Fire함수 호출 , 쿨타임 돌리기 
        if (Input.GetMouseButtonUp(0) && fireControl)
        {
            // 카메라로부터(시작점) + 커서위치까지(방향)
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //충돌 오브젝트를 저장할 구조체 변수 
            RaycastHit hitInfo;

            // Raycast(시작점+방향, 충돌오브젝트 저장변수, 최대거리), 충돌시 true 반환 
            if (Physics.Raycast(ray, out hitInfo, 100f))
            {
                //Debug.Log(" hit object : " + hitInfo.collider.name);

                hitPosition = hitInfo.point;

            }

            //거리 = 히트지점 - 현재 위치 
            Vector3 dir = hitPosition - transform.position;
            //거리 벡터에서 y값 제거
            Vector3 dirXZ = new Vector3(dir.x, 0f, dir.z);

            // 현재위치를 클릭한게 아니라면 
            if (dirXZ != Vector3.zero)
            {
                // dirXZ값을 가지고 rotation 생성 , ?????
                Quaternion targetRot = Quaternion.LookRotation(dirXZ);
                // 돌아보는 중간에 총을 쏘면 안되므로 즉시 돌아볼것 
                // 쏜 이후에 다시 가던 방향을 볼것, 코루틴처리 / 없이도 되네..  
                slime.transform.rotation = targetRot;
                //Debug.Log("targetRot : " + targetRot);

                Fire();
            }
        }
    }

    void Fire()
    {
        //Debug.Log(coolDown);
        // 쿨타운이 돌고 있지 않으면 
        if (!coolDown)
        {
            CreateBullet();

            source.PlayOneShot(fireAudio, 0.05f);
            bulletCooltimeImage.SetActive(false);
            coolDown = true;
            // 쿨타임 카운트 시작 
            StartCoroutine(CoolDownCounting());
        }

    }

    IEnumerator CoolDownCounting()
    {
        coolCount = 2;
        while (coolCount > 0)
        {
            coolCount--;
            //Debug.Log("CoolTime : " + coolCount);

            yield return new WaitForSeconds(1.0f);
        }
        coolDown = false;
        bulletCooltimeImage.SetActive(true);
    }

    void CreateBullet()
    {
        Instantiate(bullet, firePos.position, firePos.rotation);
    }

    /*
    IEnumerator Fire()
    {
        CreateBullet();

        yield return new WaitForSeconds(0.5f);
        // 쏜 이후에 다시 가던 방향을 볼것
        transform.rotation = GetComponentInParent<MouseCtrl>().frameRot;
    }
    */

    // 활성화 시  탄환 활성화 
    private void OnEnable()
    {
        bulletCooltimeImage.SetActive(true);
    }

    // 비활성화 시 탄환 비활성화 
    private void OnDisable()
    {
        bulletCooltimeImage.SetActive(false);
    }
}
