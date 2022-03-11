using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Collections;

public enum EnemyStates { Guard,Patrol,Chase,Dead}
public class EnemyController : MonoBehaviour
{
    private EnemyStates enemyStates;
    public float speed;
    public float radius;
    public float waitTime;
    public Animator anim;
    public Transform[] movePos;
    public Transform playerTran;

    private int i = 0;
    private bool movingRight = true;
    private float wait;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        wait = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        foundPlayer();
        SwitchStates();
        if (foundPlayer())
        {
            transform.position =
    Vector2.MoveTowards(transform.position, playerTran.position, speed * Time.deltaTime);
        }
        else
        {
            Patrol();
        }

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
}
