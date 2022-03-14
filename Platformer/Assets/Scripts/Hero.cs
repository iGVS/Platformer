using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hero : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    private Animator anim;
    public LayerMask Ground;
    public bool inAir;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private States State
    {
        get { return (States)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }


    private void Awake()
    {
        speed = 3f;
        jumpForce = 10f;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Finish1")
        {            
            SceneManager.LoadScene(2);            
        }
        if (other.gameObject.tag == "Finish2")
        {
            SceneManager.LoadScene(3);            
        }
        if (other.gameObject.tag == "Finish3")
        {
            SceneManager.LoadScene(0);
        }
    }

    private void Update()
    {
        if (!inAir) State = States.Stay;

        if (Input.GetButton("Horizontal"))
            Run();

    if (Input.GetKeyDown(KeyCode.Space) && !inAir)
    {
        inAir = true;
        Jump();
        State = States.Jump;
    }
}

private void Run()
    {
        if (!inAir) State = States.Run;
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

        sprite.flipX = dir.x < 0.0f;
    }
 
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        //rb.AddForce(Vector2.up * jumpForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            inAir = false;
    }
}

public enum States
{
    Stay,
    Run,
    Jump
}