using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float lakas = 0f;
    public float lakasy = 0f;

    [Range(1, 100)]
    public float moveSpeed = 1f;
    [Range(1, 20)]
    public float jumpForce = 1f;
    private float moveInput;
    public float knockBacky;
    public float knockBackx;

    private bool facingRight = true;
    private bool isGround;

    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    bool isTouchingFront;
    public Transform frontCheck;
    public LayerMask whatIsFront;
    bool wallSliding;
    public float wallSlidingSpeed;

    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpingTime;

    bool canDash = true;
    bool isDashing;
    private float dashingPower = 40f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1.5f;

    public Rigidbody2D rb;
    private Animator anim;

    public Color hurtColor;
    public SpriteRenderer[] bodyParts;

    private RipplePostProcessor camRipple;
    public GameObject effect;
    public Animator camAnim;

    void Start()
    {
        camRipple = Camera.main.GetComponent<RipplePostProcessor>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        //Jump
        isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if (isGround == true && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("TakeOff"); 
            rb.velocity = Vector2.up * jumpForce;
            FindObjectOfType<AudioManager>().Play("Jump");
        }

        if (isGround == true)
        {
            anim.SetBool("IsJumping", false);
        }
        else
        {
            anim.SetBool("IsJumping", true);
        }

        if (Input.GetKeyDown(KeyCode.C) && canDash)
        {
            FindObjectOfType<AudioManager>().Play("Dash");
            StartCoroutine(DashRight());
        }
        if (Input.GetKeyDown(KeyCode.Z) && canDash)
        {
            FindObjectOfType<AudioManager>().Play("Dash");
            StartCoroutine(DashLeft());
        }

        //Wall Sliding
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsFront);
        if (isTouchingFront == true && isGround == false && moveInput != 0)
        {
            wallSliding = true;
        }
        else
        {
            wallSliding = false; 
        }
       
        //Wall Jump
        if (wallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
       
        if (Input.GetKeyDown(KeyCode.Space) && wallSliding == true)
        {
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", wallJumpingTime);
        }

        if (wallJumping == true)
        {
            rb.velocity = new Vector2(xWallForce * -moveInput, yWallForce);
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        //Player Movement 
        moveInput = Input.GetAxis("Horizontal");
        if (moveInput == 0)
        {
            anim.SetBool("IsRunning", false);
        }
        else
        {
            anim.SetBool("IsRunning", true);     
        }

        if (wallJumping != true)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }

        if (facingRight == false && moveInput > 0) 
        {
            Flip();
        }

        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Spike"))
        {
            FindObjectOfType<AudioManager>().Play("PHurt");
            KnockBack(collision);
            StartCoroutine(Flash());
        }
    }

    IEnumerator Flash()
    {
        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].color = hurtColor;
        }
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].color = Color.white;
        }
        Time.timeScale = 1f;
    }

    //char Flip
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

        if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }

    private void KnockBack(Collision2D collision)
    {
        rb.velocity = new Vector2(rb.velocity.x, knockBacky / 2);
        //IF enemy is in right side
        if (collision.gameObject.transform.position.x >= transform.position.x)
        {
            print("position of enemy: KANAN");
            this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-lakas, 0f));
            //rb.velocity = new Vector2(-lakas, lakasy);
        }
        else
        {
            print("position of enemy: KALIWA");
            this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(lakas, 0f));
            //rb.velocity = new Vector2(lakas, lakasy);
        }
    }
    public void Died()
    {
        StartCoroutine(Dead());
    }

    void Destroy()
    {
        Destroy(gameObject);
    }

    IEnumerator Dead()
    {
        FindObjectOfType<AudioManager>().Play("PDeath");
        camRipple.RippleEffect();
        camAnim.SetTrigger("Shake");
        Instantiate(effect, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        Destroy();
    }

    IEnumerator DashRight()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    IEnumerator DashLeft()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(-transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }


}
