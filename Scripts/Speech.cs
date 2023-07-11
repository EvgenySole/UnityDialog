using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Speech : MonoBehaviour
{
    public AudioSource audioSource;
    public Animator animator;

    public List<AudioClip> currentDialog1st = new List<AudioClip>();
    public List<AudioClip> currentDialog2nd = new List<AudioClip>();
    public List<AudioClip> actualDialog = new List<AudioClip>();
    




    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        
        
        

    }
    

    // Update is called once per frame
    void Update()
    {


    }

    public void StartSound()
    {
        audioSource.Play();
    }

    public void StopSound()
    {
        audioSource.Stop();
    }

    
}
