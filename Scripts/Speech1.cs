using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Speech1 : MonoBehaviour
{
    public AudioSource audioSource;
    public Animator animator;

    public List<AudioClip> currentDialog1st = new List<AudioClip>();
    public List<AudioClip> currentDialog2nd = new List<AudioClip>();
    public List<AudioClip> actualDialog = new List<AudioClip>();

    public DialogController dialogController;

    public bool isAdded = false;
    public bool isMoving = false;
    public bool isBack = false;
    public bool isRotatedLeft = false;
    public bool isRotatedRight = false;





    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        dialogController = GameObject.Find("Plane").GetComponent<DialogController>();
        isAdded = false;
        isMoving = true;
        this.GetComponent<Animator>().SetTrigger("IsWalk");




    }


    // Update is called once per frame
    void Update()
    {
        if (isMoving && this.transform.parent.rotation == Quaternion.Euler(0f, -180f, 0f))
        {
            this.transform.parent.position += new Vector3(0f, 0f, -0.05f);

        }
        if (isMoving && this.transform.parent.rotation == Quaternion.Euler(0f, 0f, 0f))
        {
            this.transform.parent.position += new Vector3(0f, 0f, 0.05f);
        }
        if ((isBack && this.transform.parent.rotation == Quaternion.Euler(0f, 0f, 0f)) || isRotatedLeft)
        {
            isRotatedLeft = true;
            isBack = false;
            this.transform.parent.position += new Vector3(0f, 0f, -0.05f);
            this.transform.parent.rotation = Quaternion.Euler(0f, -180f, 0f);
        }
        if ((isBack && this.transform.parent.rotation == Quaternion.Euler(0f, -180f, 0f)) || isRotatedRight)
        {
            isRotatedRight = true;
            isBack = false;
            this.transform.parent.position += new Vector3(0f, 0f, 0.05f);
            this.transform.parent.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        if (!isMoving)
        {
            this.GetComponent<Animator>().ResetTrigger("IsWalk");
            this.GetComponent<Animator>().SetTrigger("IsIdle");
        }
        if (isRotatedLeft || isRotatedRight)
        {
            this.GetComponent<Animator>().ResetTrigger("IsIdle");
            this.GetComponent<Animator>().SetTrigger("IsWalk");            
        }

    }

    public void StartSound()
    {
        audioSource.Play();
    }

    public void StopSound()
    {
        audioSource.Stop();
        dialogController.animator1st.SetInteger("Trig", 0);
        dialogController.animator2nd.SetInteger("Trig", 0);
    }


}
