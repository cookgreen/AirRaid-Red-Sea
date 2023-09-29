using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerUnTrigger : MonoBehaviour
{
    public AudioClip soundToPlay;
    public GameObject untriggeredObject;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = soundToPlay;
        audioSource.Play();

        untriggeredObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
