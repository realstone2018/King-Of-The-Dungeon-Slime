
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakingCamera : MonoBehaviour
{
    public float shakes = 0f;

    public float shakeAmount = 0.7f;

    public float decreaseFactor = 1.0f;

    public Vector3 originalPos;

    public bool CameraShaking;


    void Start()

    {
        originalPos = gameObject.transform.position;

        CameraShaking = false;
    }

    public void ShakeCamera(float shaking)

    {
        shakes = shaking;

        originalPos = gameObject.transform.position;

        CameraShaking = true;
    }


    void FixedUpdate()

    {
        if (CameraShaking)
        {

            if (shakes > 0)

            {

                gameObject.transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;



                shakes -= Time.deltaTime * decreaseFactor;

            }

            else

            {

                shakes = 0f;

                gameObject.transform.localPosition = originalPos;

                CameraShaking = false;

            }
        }
    }

}

