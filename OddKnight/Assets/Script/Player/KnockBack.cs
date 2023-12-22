using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public float knockBackLength = 0.5f;
    public float knockBackForce = 7f;

    PlayerController pc;
    Rigidbody2D rb;
    void Start()
    {
        pc = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();

    }

    public void DoKnockBack()
    {
        StartCoroutine(DisablePlayerMovement(knockBackLength));
        rb.velocity = new Vector2(-pc.xWallForce * knockBackForce, knockBackForce);
    }

    IEnumerator DisablePlayerMovement(float time)
    {
        pc.enabled = false;
        yield return new WaitForSeconds(time);
        pc.enabled = true;
    }
}
