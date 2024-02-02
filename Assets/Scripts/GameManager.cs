using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private float time;

    public void Awake()
    {
        //SINGLETON
        if (!instance) //si instance no tiene info
        {
            instance = this; //instance se asigna a este objeto
            DontDestroyOnLoad(gameObject); // se indica que este obj no se destruya  con la carga de escenas
        }
        else //si instance tiene info
        {
            Destroy(gameObject); //se destruye el gameObject, para que no haya dos o mas gms en el juego
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }

    public float GetTime()
    {
        return time;
    }
}
