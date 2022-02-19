using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 newPos;
    private Vector2 originalPos=new Vector2();
    private Vector2 targetPos;
    public float speed = 1f;

    bool locked = true;
    void Start()
    {

        Debug.Log("x"+ transform.position.x + "y"+ transform.position.y);
        originalPos.Set(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (!locked) {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, step);
        }
    }

    void move(float timer) {
        locked = false;
        targetPos.Set(newPos.x, newPos.y);
        StartCoroutine(ExampleCoroutine(timer));
    }

    IEnumerator ExampleCoroutine(float timer)
    {
        yield return new WaitForSeconds(timer);
        targetPos.Set(originalPos.x, originalPos.y);
    }
}
