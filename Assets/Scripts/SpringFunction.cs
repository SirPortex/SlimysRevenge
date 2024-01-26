using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpringFunction : MonoBehaviour
{
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float value = 5;
        if (value == 5)
        {
            if (collision.gameObject.GetComponent<PlayerScript>())
            {
                if (collision.GetContact(0).normal.y <= -0.9)
                {
                    collision.gameObject.GetComponent<PlayerScript>().BounceSpring();
                    _animator.SetBool("isBouncing", true);
                }
            }
            else
            {
                _animator.SetBool("isBouncing", false);
            }
        }
    }
}
