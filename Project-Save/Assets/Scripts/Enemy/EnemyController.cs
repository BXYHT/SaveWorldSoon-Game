using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Collections;

public enum EnemyStates { Guard,Patrol,Chase,Dead}
public enum EnemyType {Short,Far,Treat,Defend }
public class EnemyController : MonoBehaviour
{
    private EnemyStates enemyStates;
    public EnemyType enemyType;
    public float speed;
    public float radius;
    public float waitTime;
    public Animator anim;
    public Transform[] movePos;
    public Transform playerTran;
    public LayerMask layer;

    private int i = 0;
    private bool movingRight = true;
    private float wait;

    public int health;
    private int maxHealth;
    public int damage;
    public float treatTime;
    private float currentTreatTime;
    public float treatRadius;
    private bool isTreat;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        isTreat = false;
        anim = GetComponent<Animator>();
        wait = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        foundPlayer();
        SwitchStates();
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        SwitchEnemy();

    }

    private void SwitchEnemy()
    {
        //近战敌人追击
        if (enemyType == EnemyType.Short)
        {
            if (foundPlayer())
            {
                transform.position = Vector2.MoveTowards(transform.position, playerTran.position, speed * Time.deltaTime);
            }
            else
            {
                Patrol();
            }

        }
        //远程敌人
        else if (enemyType == EnemyType.Far)
        {
            if (foundPlayer())
            {
                transform.position = Vector2.MoveTowards(transform.position, playerTran.position, speed * Time.deltaTime);
            }
            else
            {
                Patrol();
            }
        }
        //治疗敌人
        else if (enemyType == EnemyType.Treat)
        {
            Collider2D o = Physics2D.OverlapCircle(gameObject.transform.position, treatRadius, layer);
            if (o.tag == "Enemy" && isTreat == false)
            {

                currentTreatTime = treatTime;
                isTreat = true;
            }
            currentTreatTime -= Time.deltaTime;
            if (currentTreatTime<0 && o.GetComponent<EnemyController>().health < o.GetComponent<EnemyController>().maxHealth && isTreat)
            {
                isTreat = false;
                o.GetComponent<EnemyController>().TakeDamage(-2);
            }

        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    private void Patrol()
    {
        transform.position =
                    Vector2.MoveTowards(transform.position, movePos[i].position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, movePos[i].position) < 0.1f)
        {
            if (waitTime > 0)
            {
                waitTime -= Time.deltaTime;
            }
            else
            {
                if (movingRight)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingRight = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;
                }

                if (i == 0)
                {
                    i = 1;
                }
                else
                {
                    i = 0;
                }
                waitTime = wait;
            }
        }
    }

    void SwitchStates()
    {
        
        switch (enemyStates)
        {
            case EnemyStates.Guard:
                break;
            case EnemyStates.Patrol:
                break;
            case EnemyStates.Chase:
                break;
            case EnemyStates.Dead:
                break;
        }
    }

    bool foundPlayer()
    {
        if(playerTran != null)
        {
            float distance = (transform.position - playerTran.position).sqrMagnitude;
            if (distance < radius)
            {
                return true;
            }
        }
        return false;
    }


    bool foundEnemy()
    {

        Collider2D o = Physics2D.OverlapCircle(gameObject.transform.position, treatRadius, layer);
        if (o.tag == "Enemy")
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(gameObject.transform.position, treatRadius);
    }
}
