using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpSpeed = 6.5f;

    private bool isGround;
    private Rigidbody2D rb;
    private Animator am;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        am = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            isGround = false;
            am.SetBool("Grounded", isGround);
            am.SetTrigger("Jump");
        }
        float deltaX = Input.GetAxis("Horizontal") * speed;

        if (deltaX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (deltaX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        
        rb.velocity = new Vector2(deltaX, rb.velocity.y);
        am.SetFloat("AirSpeedY", rb.velocity.y);
        if (rb.velocity.x != 0)
        {
            am.SetInteger("AnimState", 1);
        }
        else
        {
            am.SetInteger("AnimState", 0);
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("brick"))
        {
            isGround = true;
            am.SetBool("Grounded", isGround);
        }

        Cloud cloud = other.collider.GetComponent<Cloud>();
        if (cloud != null)
        {
            transform.parent = cloud.transform;
        }
        else
        {
            transform.parent = null;
        }
    }
}
