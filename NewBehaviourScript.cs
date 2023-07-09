using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    Animator dsd;
    AnimationClip anaim;
    List<string> anims = new List<string>();
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        dsd = GetComponent<Animator>();
        anims.Add("Base Layer.New Animation");
        anims.Add("Base Layer.New Animation 12");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (dsd != null && dsd.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.New State"))
            {
                // play Bounce but start at a quarter of the way though
                dsd.Play(anims[i], 0, 0.25f);
                i++;
                if (i > 1){
                    i = 0;
                }
            }
        }
    }
}
