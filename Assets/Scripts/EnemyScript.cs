using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{

    public float speed;
    public float rayDistance;
    public LayerMask player;

    private Rigidbody2D _rb;
    private SpriteRenderer _rend;
    private Animator _animator;
    private Vector2 _dir;
    private bool _gonnaDie;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rend = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        GetComponent<CapsuleCollider2D>();
        GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ChaseLeft())
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            _rend.flipX = false;
        }
        
        if (ChaseRight())
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            _rend.flipX = true;
        }
    }
    private void StopPlayer()
    {
        speed = 0.0f;
        if (speed == 0.0f)
        {
            _rend.flipX = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        float value = 5;
        if (value == 5)
        {
            if (collision.gameObject.CompareTag("Player"))
            {


                if (collision.GetContact(0).normal.y <= -0.9)
                {
                    
                    _rb.gravityScale = 0f;
                    Destroy(gameObject.GetComponent<Rigidbody2D>());
                    Destroy(gameObject.GetComponent<BoxCollider2D>());
                    StopPlayer();
                    collision.gameObject.GetComponent<PlayerScript>().Bounce();
                    _animator.SetBool("isDying", true);
                    print("Colision");
          
                }
                else
                {
                    collision.gameObject.GetComponent<PlayerScript>().SlimeDeath();
                    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    _rb.gravityScale = 0f;
                    Destroy(gameObject.GetComponent<Rigidbody2D>());
                    Destroy(gameObject.GetComponent<BoxCollider2D>());
                }
            }
        }
    }

    private bool ChaseLeft()
    {
        RaycastHit2D collisionL = Physics2D.Raycast(transform.position, Vector2.left, rayDistance, player);
        if (collisionL)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool ChaseRight()
    {
        RaycastHit2D collisionR = Physics2D.Raycast(transform.position, Vector2.right, rayDistance, player);
        if (collisionR)
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
        Gizmos.DrawRay(transform.position, Vector2.left * rayDistance);
        Gizmos.DrawRay(transform.position, Vector2.right * rayDistance);
    }

    
}
