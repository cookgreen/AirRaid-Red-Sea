using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerTrigger : MonoBehaviour
{
    public AudioClip playSound;
    public GameObject triggeredObject;
    public GameObject triggeredObject2;

    public GameObject untriggeredObject;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = playSound;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(!audioSource.isPlaying)
        {
            gameObject.SetActive(false);

            triggeredObject.SetActive(true);

            if (triggeredObject2 != null)
            {
                triggeredObject2.SetActive(true);
            }

            if (untriggeredObject != null)
            {
                untriggeredObject.SetActive(false);
            }
        }
    }
}
