using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorGoUp : MonoBehaviour {
    public float arrivalHeight = 0;
    public bool arrival = false;

    public GameObject mainObject;
    public GameObject currentStagObject;
    public GameObject[] stage_Manager;

    public void StartMoveElevator(float TargetHeight)
    {
        arrivalHeight = TargetHeight;

        SortStage();
        StartCoroutine(MoveElevator());
    }

    private void SortStage()
    {
        int i = 1;

        foreach (GameObject manager in stage_Manager)
        {
            if (manager.activeSelf)
            {
                currentStagObject = GameObject.FindGameObjectWithTag("STAGE" + i + "_OBJECT");
            }
            i++;
        }

        currentStagObject.transform.SetParent(mainObject.transform);
    }

    IEnumerator MoveElevator()
    {
        Camera.main.GetComponent<ShakingCamera>().ShakeCamera(1.0f);
        yield return new WaitForSeconds(1.3f);

        while (!arrival)
        {
            transform.Translate(Vector3.up * -10.0f * Time.deltaTime);
            // 엘레베이터 상승시 오브젝트가 살짝 묻히는 현상제거를 위해 오브젝트들도 같이 상승시킨다.
            mainObject.transform.Translate(Vector3.up * 10.0f * Time.deltaTime);

            if (transform.position.y >= arrivalHeight)
            {
                arrival = true;
            }
            yield return new WaitForSeconds(0.01f);
        }

        // 초기화
        currentStagObject.transform.SetParent(null);
        arrival = false;
    }
}
