using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpeech : MonoBehaviour
{
    public bool isBusy;
    public AudioClip clip;
    public AudioSource source;

    public int numOfConvers;

    
    // Start is called before the first frame update
    void Start()
    {
        this.source = this.GetComponent<AudioSource>();
        isBusy = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCubeAnim(){
        print("Trigger");
        isBusy = true;
        if (source.clip != null){
            source.Play();
        }
    }

    public void EndCubeAnim(){
        print("EndTrigger");
        isBusy = false;
        source.Stop();
    }
}
