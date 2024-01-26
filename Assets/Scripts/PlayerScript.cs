using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerScript : MonoBehaviour
{
    public KeyCode rightKey, leftKey, jumpKey;
    public float speed, rayDistance, jumpForce, bounceSpeed, bounceSpring;
    public LayerMask groundMask;
    

    private Rigidbody2D rb;
    private SpriteRenderer _rend;
    private Animator _animator;
    private Vector2 dir;
    private bool _intentionToJump;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _rend = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        dir = Vector2.zero;
        if (Input.GetKey(rightKey))
        {
            _rend.flipX = false;
            dir = Vector2.right;
            
        }
        else if (Input.GetKey(leftKey))
        {
            _rend.flipX = true;
            dir = new Vector2(-1, 0);

        }

        // _intentionToJump = false;
        if (Input.GetKey(jumpKey))
        {
            _intentionToJump = true;

        }

        #region ANIMACIONES
        // ANIMACIONES (PROXIMA DIA ORGANIZARLO EN OTRO SCRIPT)
        if (dir != Vector2.zero)
        {
            // estamos andando
            _animator.SetBool("isWalking", true);
        }
        else
        {
            // estamos parados
            _animator.SetBool("isWalking", false);
        }


        if (IsGrounded())
        {
            //estamos tocando el suelo
            _animator.SetBool("isJumping", false);
        }
        else
        {
            //no estamos tocando el suelo
            _animator.SetBool("isJumping", true);
        }
        
        
        #endregion
    }

    private void FixedUpdate()
    {
        // if (dir != Vector2.zero)
        {
            float currentYVel = rb.velocity.y;
            Vector2 nVel = dir * speed;
            nVel.y = currentYVel;

            rb.velocity = nVel;
        }

        if (_intentionToJump && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce * rb.gravityScale, ForceMode2D.Impulse);
        }
        _intentionToJump = false;
    }

    public void Bounce()
    {
        rb.velocity = new Vector2(rb.velocity.x, bounceSpeed);
    }

    public void BounceSpring()
    {
        rb.velocity = new Vector2(rb.velocity.x, bounceSpring);
    }

    public void SlimeDeath()
    {
        rb.gravityScale = 0;
        speed = 0;
        _animator.SetBool("isDEAD", true);
    }

    private bool IsGrounded()
    {
        RaycastHit2D collision = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, groundMask);
        if (collision)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * rayDistance);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Spikes>())
        {
            SlimeDeath();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.GetContact(0).normal.x <= -1 && collision.gameObject.CompareTag("Enemy"))
        {
            SlimeDeath();
        }

        if (collision.GetContact(0).normal.x >= -1 && collision.gameObject.CompareTag("Enemy"))
        {
            SlimeDeath();
        }
    }
}
