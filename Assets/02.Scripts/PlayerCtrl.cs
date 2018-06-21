using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// wasd 를 통한 슬라임 컨트롤 ,  슬라임 조작 방식 변경으로 사용하진 않음
public class KeyboardController : MonoBehaviour {

    private float h = 0.0f;
    private float v = 0.0f;
    private Transform tr;
    public float moveSpeed = 10.0f;
    public float rotSpeed = 100.0f;
    public float jumpPower = 150.0f;

    public Quaternion rotation = Quaternion.identity;

    void Start () {
        tr = GetComponent<Transform>();
        StartCoroutine(this.Jump());
	}
	
	void Update () {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.World);
        // tr.Rotate(Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse X"));

        // 움직임 상화좌우, 대각선 이동 
        InputMove();
    }

    void InputMove()
    {
        if (Input.GetButton("W"))           //앞으로
        {
            if (Input.GetButton("D"))
            {
                Debug.Log("W&D");
                tr.rotation = Quaternion.Euler(0, 45, 0);
            }
            else if (Input.GetButton("A"))
            {
                Debug.Log("W&A");
                tr.rotation = Quaternion.Euler(0, -45, 0);
            }
            else
            {
                Debug.Log("W");
                tr.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else if (Input.GetButton("S"))    // 뒤로
        {
            if (Input.GetButton("D"))
            {
                Debug.Log("S&D");
                tr.rotation = Quaternion.Euler(0, 135, 0);
            }
            else if (Input.GetButton("A"))
            {
                Debug.Log("S&A");
                tr.rotation = Quaternion.Euler(0, -135, 0);
            }
            else
            {
                Debug.Log("Back");
                tr.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
        else if (Input.GetButton("D"))     // 오른쪽
        {
            Debug.Log("Right");
            tr.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (Input.GetButton("A"))    // 왼쪽이동
        {
            Debug.Log("Left");
            tr.rotation = Quaternion.Euler(0, -90, 0);
        }
        else                    // 정지
        {

        }
    }

    IEnumerator Jump()
    {
        while (true) {
            yield return new WaitForSeconds(0.8f);
            //Debug.Log("Jump Jump");
            this.GetComponent<Rigidbody>().AddForce(transform.up * jumpPower);
        }
    }
}
