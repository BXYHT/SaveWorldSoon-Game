using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gtrigger : MonoBehaviour
{
    public GameObject block;
    public float timer=6;
    public float cameraMaxRanger = 7;

    private GameObject cameraController;

    private void Start()
    {
        cameraController = GameObject.FindWithTag("CameraController");
        if(cameraController)
            Debug.Log(cameraController);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("box"))
        {
            MessageObject messageObject = new MessageObject();
            messageObject.position = transform.position;
            messageObject.timer = timer;
            messageObject.camearRanger = cameraMaxRanger;
            Debug.Log("send message");
            block.SendMessage("move", timer, SendMessageOptions.DontRequireReceiver);
            collision.gameObject.SendMessage("ChangeTolocked", messageObject, SendMessageOptions.DontRequireReceiver);
            cameraController.SendMessage("ChangeCamera", messageObject, SendMessageOptions.DontRequireReceiver);
        }
    }

}
