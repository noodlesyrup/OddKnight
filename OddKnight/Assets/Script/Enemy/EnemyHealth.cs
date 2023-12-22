using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject effect;

    public int maxHealth = 3;
    int CurrentHealth;
    public int enemyDamage = 1;

    private RipplePostProcessor camRipple;
    public Animator camAnim;

    public SpriteRenderer[] bodyParts;
    public Color hurtColor;

    public Health health;

    private Rigidbody2D rb;

    void Start()
    {
        CurrentHealth = maxHealth;
        camRipple = Camera.main.GetComponent<RipplePostProcessor>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage)
    {
            FindObjectOfType<AudioManager>().Play("EnemyHurt");
            camAnim.SetTrigger("Shake");
            StartCoroutine(Flash());
            rb.AddForce(Vector2.right * 5, ForceMode2D.Impulse);
            CurrentHealth -= damage;
            Instantiate(effect, transform.position, Quaternion.identity);

        if (CurrentHealth <=0 )
        {
            camRipple.RippleEffect();
            camAnim.SetTrigger("Shake");
            Instantiate(effect, transform.position, Quaternion.identity );
            Dead();
        }
    }

    void Dead()
    {
        FindObjectOfType<AudioManager>().Play("EnemyDeath");
        Destroy(gameObject);
    }

    IEnumerator Flash()
    {
        for (int i = 0; i < bodyParts.Length; i++ )
        {
            bodyParts[i].color = hurtColor;
        }

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].color = Color.white;
        }
    }
}
