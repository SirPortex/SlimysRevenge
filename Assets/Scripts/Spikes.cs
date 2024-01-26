using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerScript>())
        {
            print("Has Muerto, vaya manco, aprende a jugar palerdo");
        }

        if (collision.gameObject.GetComponent<EnemyScript>())
        {
            print("El enemigo ha muerto, que suerte la tuya");
        }
    }
}
