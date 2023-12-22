using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    public Health health;
    public int enemyDamage = 1;
    bool hit = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!hit)
            {       
                hit = true;
                EnemyDamage();
            }   
        }
    }

    void EnemyDamage()
    {
        StartCoroutine(Damage());
    }

    IEnumerator Damage()
    {
        health.playerHealth = health.playerHealth - enemyDamage;
        health.UpdateHealth();
        this.enabled = false;
        yield return new WaitForSeconds(1.5f);
        this.enabled = true;
        hit = false;
    }
}
