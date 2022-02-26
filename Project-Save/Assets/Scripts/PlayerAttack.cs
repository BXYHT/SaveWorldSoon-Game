using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage;
    public float startTime;
    public float time;

    public Animator anim;
    public PolygonCollider2D colider2D;
    // Start is called before the first frame update
    void Start()
    {
        anim =GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        colider2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            anim.SetTrigger("Attack");
            StartCoroutine(StartAttack());
        }
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(startTime);
        //colider2D.enabled = true;
        StartCoroutine(disableHitbox());
    }
    IEnumerator disableHitbox()
    {
        yield return new WaitForSeconds(time);
        //colider2D.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("attack");
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

}
 