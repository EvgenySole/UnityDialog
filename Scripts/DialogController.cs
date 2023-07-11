using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class DialogController : MonoBehaviour
{


    public static bool isLocked = true;
    public bool oldLocked = false;

    public int numberRep = 0;

    public int numOfConvers = 1;
    public GameObject npc1;
    public GameObject npc2;

    public Speech speechScript;

    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public Animator animator1;
    public Animator animator2;

    public Animator animator1st;
    public Animator animator2nd;

    public int numberOfDialogue = 0;
    public int currentReplica = 0;

    public List<AudioClip> currentDialog1st = new List<AudioClip>();
    public List<AudioClip> currentDialog2nd = new List<AudioClip>();
    public List<AudioClip> actualDialog = new List<AudioClip>();

    public int NumberOfSpeaker = 0;
    public bool StartConversation = false;
    public int counter = 0;

    public List<GameObject> dyadki = new List<GameObject>();
    public List<List<GameObject>> dialogist = new List<List<GameObject>>();

    public GameObject DyadyaVityaPrefab;
    public List<List<Animator>> animators = new();
    public List<List<AudioSource>> sources = new();


    // Start is called before the first frame update
    void Start()
    {
        
        npc1 = GameObject.Find("Character Container");
        npc2 = GameObject.Find("Character Container (1)");
        audioSource1 = npc1.GetComponentInChildren<AudioSource>();
        audioSource2 = npc2.GetComponentInChildren<AudioSource>();
        animator1 = npc1.GetComponentInChildren<Animator>();
        animator2 = npc2.GetComponentInChildren<Animator>();
        numOfConvers = 1;

        animator1st = npc1.GetComponentInChildren<Animator>();
        animator2nd = npc2.GetComponentInChildren<Animator>();

        
        AddDyadki();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            actualDialog.Clear();
            NumberOfSpeaker = UnityEngine.Random.Range(1, 3);
            StartConversation = true;
            counter = 0;
            GetAudioDialog();
            ParseNameReplica(NumberOfSpeaker);
            if (NumberOfSpeaker == 1)
            {
                Debug.Log(sources.Count);
                for (int i = 0; i < dialogist.Count; i++)
                {
                    sources[i].ElementAt(0).clip = actualDialog[counter];
                    sources[i].ElementAt(1).clip = actualDialog[counter];
                    animators[i].ElementAt(0).SetInteger("Trig", numberRep);
                }
                Debug.Log("Sources = -- " + sources.Count);
                Debug.Log("Animators = -- " + animators.Count);
                Debug.Log("Dialogist = -- " + dialogist.Count);
                counter++;
                numberRep++;
            }
            else
            {
                Debug.Log(sources.Count);
                for (int i = 0;i < dialogist.Count; i++)
                {
                    sources[i].ElementAt(1).clip = actualDialog[counter];
                    sources[i].ElementAt(0).clip = actualDialog[counter];
                    animators[i].ElementAt(1).SetInteger("Trig", numberRep);
                }
                Debug.Log("Sources = -- " + sources.Count);
                Debug.Log("Animators = -- " + animators.Count);
                Debug.Log("Dialogist = -- " + dialogist.Count);
                counter++;
                numberRep++;
                
            }
        }
        if (StartConversation)
        {
            if (animators[0].ElementAt(0).GetNextAnimatorStateInfo(0).IsName("Idle") && NumberOfSpeaker == 1)
            {
                for (int i = 0; i < dialogist.Count; i++)
                {
                    sources[i].ElementAt(1).clip = actualDialog[counter];
                    animators[i].ElementAt(0).SetInteger("Trig", 0);
                    animators[i].ElementAt(1).SetInteger("Trig", numberRep);
                }
                NumberOfSpeaker = 2;
                numberRep++;
                counter++;
            }
            if (animators[0].ElementAt(1).GetNextAnimatorStateInfo(0).IsName("Idle") && NumberOfSpeaker == 2)
            {
                for (int i = 0; i < dialogist.Count; i++)
                {
                    sources[i].ElementAt(0).clip = actualDialog[counter];
                    animators[i].ElementAt(1).SetInteger("Trig", 0);
                    animators[i].ElementAt(0).SetInteger("Trig", numberRep);
                }
                NumberOfSpeaker = 1;
                numberRep++;
                counter++;

            }
            if (animators[0].ElementAt(1).GetCurrentAnimatorStateInfo(0).IsName("Idle") && animators[0].ElementAt(0).GetCurrentAnimatorStateInfo(0).IsName("Idle") && counter >= actualDialog.Count)
            {
                StartConversation = false;
                for (int i = 0; i < dialogist.Count; i++)
                {
                    animators[i].ElementAt(0).SetInteger("Trig", 0);
                    animators[i].ElementAt(1).SetInteger("Trig", 0);
                }
            }
        }

    }

    public void AddDyadki()
    {
        DyadyaVityaPrefab = Resources.Load<GameObject>("Prefabs/DyadyaVitya");

        GameObject temp = null;

        float xCord = 4f;
        

        for (int i = 0; i < 5; i++) 
        {
            temp = Instantiate(DyadyaVityaPrefab, new Vector3(xCord, 0f, 0f), Quaternion.identity);
            temp.name = "Dayada000" + i.ToString();
            dyadki.Add(temp);
            xCord += 4f;

        }
        xCord = 4f;
        for (int i = 0; i < 5; i++)
        {
            temp = Instantiate(DyadyaVityaPrefab, new Vector3(xCord, 0f, 3f), Quaternion.Euler(new Vector3 (0f, -180f, 0f)));
            temp.name = "Dayada000" + (i+5).ToString();
            dyadki.Add(temp);
            xCord += 4f;
        }

        for (int i = 0;i < dyadki.Count;i++)
        {
            for (int j = 0; j < dyadki.Count; j++)
            {

                

                float dist = Vector3.Distance(dyadki[i].transform.position, dyadki[j].transform.position);
                
                if (dist < 3.1f && dist > 0)
                {   
                    Debug.Log("Dist =  " + dist);
                    dialogist.Add(new List<GameObject> { dyadki[i], dyadki[j] });
                    animators.Add(new List<Animator> { dyadki[i].GetComponentInChildren<Animator>(), dyadki[j].GetComponentInChildren<Animator>() });
                    sources.Add(new List<AudioSource> { dyadki[i].GetComponentInChildren<AudioSource>(), dyadki[j].GetComponentInChildren<AudioSource>() }); 
                }

            }
        }


    }

    public void ParseNameReplica(int num)
    {
        if (num == 1)
        {
            string kekw = actualDialog[0].name;

            numberRep = int.Parse(kekw.Substring(3));
            
            //speechScript.animator.SetInteger("Trig", numberRep - 1);
        }
        else if (num == 2)
        {
            string kekw = actualDialog[0].name;
            numberRep = int.Parse(kekw.Substring(3));
            
            //speechScript.animator.SetInteger("Trig", numberRep - 1);
        }

    }

    public void GetAudioDialog()
    {
        numberOfDialogue = UnityEngine.Random.Range(1, 4);
        actualDialog.AddRange(Resources.LoadAll<AudioClip>($"Sound/Dialog{numberOfDialogue}"));
    }

    public void GetRandomMonologue()
    {
        numberOfDialogue = UnityEngine.Random.Range(1, 37);
        actualDialog.Add(Resources.Load<AudioClip>($"Sound/Monologue/Replica ({numberOfDialogue})"));
    }
}

