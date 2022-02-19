using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    private float orginalRanger;
    private float targetRanger;
    private float currentRanger;
    // Start is called before the first frame update
    float disChange = 0.02f;

    private float lerpDuration = 0.0f;
    void Start()
    {
        orginalRanger = vcam.m_Lens.OrthographicSize;
        targetRanger = orginalRanger;
        currentRanger = targetRanger;
    }

    // Update is called once per frame
    void Update()
    {
        if (lerpDuration > 0) {
            float temp = Mathf.Lerp(currentRanger, targetRanger, lerpDuration);
            lerpDuration += disChange;
            if (lerpDuration > 1f)
            {
                lerpDuration = 0f;
                currentRanger = targetRanger;
                SetFieldView(currentRanger);
                return;
            }
            SetFieldView(temp);
        }
    }

    private void SetFieldView(float bla)
    {
        vcam.m_Lens.OrthographicSize = bla;
    }

    void ChangeCamera(MessageObject messageObject)
    {
        Debug.Log("get message");
        if (lerpDuration > 0)
            return;
        lerpDuration = disChange;
        targetRanger = messageObject.camearRanger;
//        SetFieldView(messageObject.camearRanger);
        StartCoroutine(ExampleCoroutine(messageObject.timer));
    }

    IEnumerator ExampleCoroutine(float timer)
    {
        yield return new WaitForSeconds(timer);
//        SetFieldView(orginalRanger);
        targetRanger = orginalRanger;
        lerpDuration = disChange;

    }

}
