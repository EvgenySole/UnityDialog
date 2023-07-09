using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript2 : MonoBehaviour
{
    private float dist;
    private bool isRotate = true;
    private float angle = 0;
    private Transform cub2;
    private int i = 0;
    public bool isStartDialog = false;
    public AudioClip talk1;
    public AudioSource source2;
    // Start is called before the first frame update
    void Start()
    {
        talk1 = Resources.Load<AudioClip>("Talk2");
        source2 = this.GetComponent<AudioSource>();
        source2.clip = talk1;
        cub2 = GameObject.Find("Cubecs").transform;
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, cub2.position);
        if (dist > 3.8){
            this.transform.position += new Vector3(0, 0, -0.01f);
        }
        else if (isRotate) 
        {   
            transform.GetChild(0).Rotate(0, -0.4f, 0, Space.Self);
        } else 
        {
            //this.transform.position += new Vector3(0, 0, -0.01f);
        }
        if (isStartDialog)
        {
           //StartCoroutine(sayDialog());
            //isStartDialog = false;
        }
        print(source2.isPlaying);
        if (!DialogController.isLocked  && DialogController.numOfConvers == 2){
            //source1.Play();     
            StartCoroutine(sayDialog2());
            DialogController.isLocked = true;
            isStartDialog = false;
            DialogController.numOfConvers = 1;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("AAA");
        print("AAA");
        isRotate = false;
        isStartDialog = true;
        DialogController.setNumOfConvers();
        //source2.Play();
       // DialogController.isLocked = false;
       // StartCoroutine(sayDialog2());
        //source2.clip = Resources.Load<AudioClip>("Talk2");
        
    }
    IEnumerator sayDialog2(){ 
        
        yield return new WaitForSeconds(1);
        //source2.enabled = false;
        source2.Play();
        yield return new WaitForSeconds(1);
        isStartDialog = true;
        DialogController.isLocked = false;
        StopAllCoroutines();
    }
    
     IEnumerator sayDialog(){ 
        yield return new WaitForSeconds(1);
        DialogController.dialog = "Привет Джорож";
        yield return new WaitForSeconds(2);
         DialogController.dialog = "Отлично, а у тебя?";
         yield return new WaitForSeconds(2);
        DialogController.dialog = "А какие же новости?";
         yield return new WaitForSeconds(2);
        DialogController.dialog = "Вау, поздравляю!";
         yield return new WaitForSeconds(2);
    } 
}
