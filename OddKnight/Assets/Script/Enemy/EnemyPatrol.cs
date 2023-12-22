using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    public float downDistance;
    public float frontDistance;
    private bool movingRight = true;
    public Transform groundDetect;
    public Transform wallDetect;


    void Update()
    {     
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetect.position, Vector2.down, downDistance, LayerMask.GetMask("Ground"));
        RaycastHit2D wallInfo = Physics2D.Raycast(wallDetect.position, Vector2.right, frontDistance, LayerMask.GetMask("Wall"));

        if (groundInfo.collider == false || wallInfo.collider == true)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }
}
