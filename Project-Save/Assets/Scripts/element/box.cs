using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour
{
    Vector3 targetPos;
    bool locked = false;
    float speed = 1;
    private Vector2 originalPos = new Vector2();



    // Start is called before the first frame update
    void Start()
    {
        originalPos.Set(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (locked) {
            float step = speed * Time.deltaTime;
            // move sprite towards the target location
            transform.position = Vector2.MoveTowards(transform.position, targetPos, step);
        }
    }

    void ChangeTolocked(MessageObject messageObject) {
        targetPos = messageObject.position;
        locked = true;
        GetComponent<Rigidbody2D>().mass = 1000;
        StartCoroutine(ExampleCoroutine(messageObject.timer));
    }

    IEnumerator ExampleCoroutine(float timer)
    {
        yield return new WaitForSeconds(timer);
        transform.position= new Vector2(originalPos.x, originalPos.y);
        GetComponent<Rigidbody2D>().mass = 13;
        locked = false;

    }
}
