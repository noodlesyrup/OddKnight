using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggro : MonoBehaviour
{
    public float moveSpeed;
    public float aggroRange;
    public Transform player;
    Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float disToPlayer = Vector2.Distance(transform.position, player.position);

        if (disToPlayer < aggroRange)
        {
            Chase();
        }
        else
        {
            StopChasing();
        }
    }

    void Chase()
    {

        if (transform.position.x < player.position.x)
        {
            rb.velocity = new Vector2(moveSpeed, 0);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (transform.position.x > player.position.x)
        {     
            rb.velocity = new Vector2(-moveSpeed, 0);
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
    }

    void StopChasing()
    {
        rb.velocity = new Vector2(0, 0);
    }
}
