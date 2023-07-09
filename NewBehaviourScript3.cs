using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript3 : MonoBehaviour
{
    private float dist;
    private bool isRotate = true;
    private float angle = 0;
    private Transform cub2;
    private int i = 0;
    public bool isStartDialog = false;
    public AudioClip talk;
    public AudioSource source1;
    // Start is called before the first frame update
    void Start()
    {
        talk = Resources.Load<AudioClip>("Talk1");
        source1 = this.GetComponent<AudioSource>();
        source1.clip = talk;
        cub2 = GameObject.Find("CubecsClone").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        dist = Vector3.Distance(transform.position, cub2.position);
        
        if (dist > 3.8){
            this.transform.position += new Vector3(0, 0, 0.01f);
        }
        else if (isRotate) 
        {   
            angle += 0.1f;
            transform.GetChild(0).Rotate(0, -0.4f, 0, Space.Self);
        }
        else 
        {
            //this.transform.position += new Vector3(0, 0, 0.01f);
        }
        if (isStartDialog)
        {
            //StartCoroutine(sayDialog1());

            //
        }



        if (!DialogController.isLocked  && DialogController.numOfConvers == 1){
            //source1.Play();     
            StartCoroutine(sayDialog2());
            DialogController.isLocked = true;
            isStartDialog = false;
            DialogController.numOfConvers = 2;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("AAA");
        print("AAA");
        isRotate = false;
        isStartDialog = true;
        DialogController.setNumOfConvers();
        //DialogController.isLocked = true;
        
    }
    IEnumerator sayDialog2(){ 
        yield return new WaitForSeconds(1);
        source1.Play();
        yield return new WaitForSeconds(3);
        isStartDialog = true;
        DialogController.isLocked = false;
        StopAllCoroutines();
    }
    IEnumerator sayDialog1(){ 
        DialogController.dialog = "Привет Алекс";
        yield return new WaitForSeconds(2);
         DialogController.dialog = "Как дела?";
         yield return new WaitForSeconds(2);
        DialogController.dialog = "Очень хорошо";
         yield return new WaitForSeconds(2);
        DialogController.dialog = "Поеду в США погулять";
         yield return new WaitForSeconds(2);
    }
}
