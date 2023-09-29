using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip music;

    // Start is called before the first frame update
    void Start()
    {
        var audio = GetComponent<AudioSource>();
        audio.clip = music;
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
