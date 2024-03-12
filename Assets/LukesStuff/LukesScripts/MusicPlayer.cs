using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    private int playing = 0;

    public AudioClip[] music = new AudioClip[3];
    private AudioSource src;
    // Start is called before the first frame update
    void Start()
    {
        playing = Random.Range(0, music.Length);
        this.src = GetComponent<AudioSource>();
        src.clip = music[playing];
        src.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(!src.isPlaying)
        {
            playing = Random.Range(0, music.Length);
            src.clip = music[playing];
            src.Play();
        }
    }
}
