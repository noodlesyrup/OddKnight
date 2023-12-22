using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyCounter : MonoBehaviour
{
    GameObject[] enemies;
    public Text enemyCounter;

    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCounter.text = "Enemy Remaining " + enemies.Length.ToString();

        if(enemies.Length == 0)
        {
            FindObjectOfType<GameManager>().LvlComplete();
        }
    }
}
