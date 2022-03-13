using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 300.0f;
    public float jumpSpeed = 6.5f;
    
    private bool isGround;
    private Rigidbody2D rb;
    private float _forward = 1.0f;
    
    [SerializeField] private GameObject _back;

    public float forward
    {
        set { _forward = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Random.Range(-1.0f, 1.0f) > 0.9f && isGround)
        {
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            isGround = false;
        }
        float deltaX = Time.deltaTime * speed;

        if (_forward > .0f)
        {
            _back.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            this.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        }
        else if (_forward < .0f)
        {
            _back.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
            this.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        }
        
        rb.velocity = new Vector2(deltaX * _forward, rb.velocity.y);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("brick"))
        {
            isGround = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("wall") || other.gameObject.CompareTag("enemy"))
        {
            _forward = -_forward;
        }
    }
}
