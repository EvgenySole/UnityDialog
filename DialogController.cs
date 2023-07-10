using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    public GameObject cube1;
    public GameObject cube2;
    public GameObject camera1;
    public Animator animator1;
    public Animator animator2;
    public AudioClip talk1;
    public AudioClip talk2;
    public AudioSource source1;
    public AudioSource source2;

    public CubeSpeech cube1Speech;
    public CubeSpeech cube2Speech;
    public int rand;
    public float dist;

    // Start is called before the first frame update
    void Start()
    {
        cube1 = GameObject.Find("Cubecs");
        cube2 = GameObject.Find("CubecsClone");
        camera1 = GameObject.Find("Main Camera");
        animator1 = cube1.GetComponentInChildren<Animator>();
        animator2 = cube2.GetComponentInChildren<Animator>();
        talk1 = Resources.Load<AudioClip>("Talk1");
        talk2 = Resources.Load<AudioClip>("Talk2");
        source1 = cube1.GetComponentInChildren<AudioSource>();
        source1.clip = talk1;
        source2 = cube2.GetComponentInChildren<AudioSource>();
        source2.clip = talk2;

        cube1Speech = cube1.GetComponentInChildren<CubeSpeech>();
        //cube1Speech.clip = talk1;
        cube2Speech = cube2.GetComponentInChildren<CubeSpeech>();
        //cube2Speech.clip = talk2;
        cube1Speech.numOfConvers = 1;
        cube2Speech.numOfConvers = 2;
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(cube1.transform.position, cube2.transform.position);
        if (Input.GetKeyDown("space") || dist < 3){
            rand = Random.Range(1, 3);
            if (rand == 1){
                cube1Speech.numOfConvers = 1;
                cube2Speech.numOfConvers = 2;
                animator1.SetInteger("Trig", 1);
            } else {
                cube1Speech.numOfConvers = 2;
                cube2Speech.numOfConvers = 1;
                animator2.SetInteger("Trig", 0);
            }
            
        }
        if (animator1.GetNextAnimatorStateInfo(0).IsName("New State") && rand == 1){
            animator1.SetInteger("Trig", -1);
            animator2.SetInteger("Trig", 0);
            rand = 2;
        }

        if (animator2.GetNextAnimatorStateInfo(0).IsName("New State") && rand == 2){
            animator2.SetInteger("Trig", -1);
            animator1.SetInteger("Trig", 1);
            rand = 1;
        }

        if (Input.GetKey("up")){
            //cube1.transform.position += new Vector3(0, 0, 0.01f);
            camera1.transform.position += new Vector3(0, 0, 0.1f);
        } 
        if (Input.GetKey("down")){
            //cube1.transform.position += new Vector3(0, 0, -0.01f);
            camera1.transform.position += new Vector3(0, 0, -0.1f);
        } 
        if (Input.GetKey("left")){
            //cube1.transform.position += new Vector3(-0.01f, 0, 0);
            camera1.transform.position += new Vector3(-0.1f, 0, 0);
        } 
        if (Input.GetKey("right")){
            //cube1.transform.position += new Vector3(0.01f, 0, 0);
            camera1.transform.position += new Vector3(0.1f, 0, 0);
        }

    }


}
