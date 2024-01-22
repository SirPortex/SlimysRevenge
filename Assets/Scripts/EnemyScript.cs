using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{

    public float speed;

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
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<CapsuleCollider2D>();
        GameObject objectCollision = collision.gameObject;

        if(objectCollision.name == "SlimyPlayer")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
