using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    //public GameManager gameManager;
    public int numOfHearts;
    public int playerHealth = 5;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHearts;

    private void Awake()
    {
        UpdateHealth();
    }
    public void Update()
    {
        if (playerHealth <= 0)
        {
            Time.timeScale = 1f;
            FindObjectOfType<PlayerController>().Died();
            FindObjectOfType<GameManager>().EndGmae();
        }
    }

    public void UpdateHealth()
    {      
        if(playerHealth > numOfHearts)
        {
            playerHealth = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHearts;
            }
        }
    }
}
