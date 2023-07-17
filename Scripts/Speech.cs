using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speech : MonoBehaviour
{

    public AudioSource audioSource;
    public DialogSystem _dialogueSystemScript;
    Animator anim;
    public bool IsAdded = false;
    public bool IsTalk = false;
    public bool isMoving = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        _dialogueSystemScript = GameObject.Find("Plane").GetComponent<DialogSystem>();
        anim = GetComponent<Animator>();
        IsAdded = false;
        isMoving = true;
        this.GetComponent<Animator>().SetTrigger("IsWalk");
    }

    private void Update()
    {
        if (isMoving && this.transform.parent.rotation == Quaternion.Euler(0f, 180f, 0f))
        {
            this.transform.parent.position += new Vector3(0f, 0f, -0.01f);

        }
        if (isMoving && this.transform.parent.rotation == Quaternion.Euler(0f, 0f, 0f))
        {
            this.transform.parent.position += new Vector3(0f, 0f, 0.01f);
        }
    }

    public void PlaySoundSpeech()
    {
        audioSource.Play();
    }

    public void StopSoundSpeech()
    {
        audioSource.Stop();
        anim.SetInteger("Trig", 0);
    }

}