using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback_Test : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("dv tag" + collision.gameObject.tag);
        if(collision.gameObject.tag == "Enemy")
        {
            

        }
    }
}
