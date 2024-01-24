using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{

    public float speed;
    public Transform target;
    public float offstet = 1.0f;
    public bool isChasing;
    public float chaseDistance;

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

        if (isChasing == true)
        {
            if (transform.position.x > target.transform.position.x)
            {
                _rend.flipX = false;

                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            if (transform.position.x < target.transform.position.x)
            {
                _rend.flipX = true;

                transform.position += Vector3.right * speed * Time.deltaTime;
            }

        }

        else
        {

            if (Vector2.Distance(transform.position, target.position) > chaseDistance)
            {
                isChasing = false;
            }

            if (Vector2.Distance(transform.position, target.position) < chaseDistance)
            {
                isChasing = true;
            } 
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
        if (collision.gameObject.CompareTag("Player"))
        {
            _rb.gravityScale = 0f;

            if (collision.GetContact(0).normal.y <= -0.9)
                {
                    _rb.gravityScale = 0f;
                    StopPlayer();
                    collision.gameObject.GetComponent<PlayerScript>().Bounce();
                    _animator.SetBool("isDying", true);
                    print("Colision");
                    Destroy(gameObject.GetComponent<BoxCollider2D>());

                    

                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    
                }
            
        }
    }

}
