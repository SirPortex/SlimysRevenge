using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    private List<GameObject> audioList;
    // Start is called before the first frame update
    void Awake() // Si no se pone "private" o "public" siempre es "private" por defecto
    {

        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioList = new List<GameObject>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Recreando el BackGroundMusic desde el codigo
    public AudioSource PlayAudio(AudioClip audioClip, string gameObjectName, bool isLoop = false, float volume = 1.0f)
    {
        //Creamos un objecto
        GameObject audioObject = new GameObject(gameObjectName);
        //Hijos del audiomanager, objectos creados
        audioObject.transform.SetParent(transform);
        //Añadimos el componente de AudioSource y le añadimos el clip de audio
        AudioSource srcComponent = audioObject.AddComponent<AudioSource>();
        //Asignamos el clip
        srcComponent.clip = audioClip;
        // El clip esta bucle
        srcComponent.loop = isLoop;
        //Empieza a sonar
        srcComponent.Play();
        //Añadimos un objecto a la lista para hacer un seguimiento
        audioList.Add(audioObject);


        //devuelve el componente para dar libertad a la hora de editarlo
        return srcComponent;
    }

    public void ClearAudios()
    {
        foreach(GameObject audioObjet in audioList)
        {
            Destroy(audioObjet);
        }

        audioList.Clear();
    }
}
