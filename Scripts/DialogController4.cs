using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class DialogController4 : MonoBehaviour
{


    public static bool isLocked = true;
    public bool oldLocked = false;

    public int numberRep = 0;

    public int numOfConvers = 1;
    public GameObject npc1;
    public GameObject npc2;

    public Speech1 speechScript;

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

    public List<bool> StartConversList = new();
    public List<int> NumberOfSpeakerList = new();
    public List<int> NumberOfRepList = new();
    public List<int> CounterList = new();
    public List<List<AudioClip>> DialogList = new();
    public bool isStartDialog = false;
    public float timer = 0;
    public float someAmount = 100;

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
        counter = 0;
        animator1st = npc1.GetComponentInChildren<Animator>();
        animator2nd = npc2.GetComponentInChildren<Animator>();
        AddDyadki();
        InitializeDialogue();
        //FillAllLists();


    }

    void FixedUpdate()
    {
        CheckDyadki();

        if (Input.GetKeyDown(KeyCode.Space))
        {

        }

        if (Input.GetKeyDown(KeyCode.Q))
        {

            StartDialogue(counter);
        }

        for (int i = 0; i < dialogist.Count; i++)
        {


            if (StartConversList.Count != 0 && StartConversList[i])
            {
                Debug.Log("NumberOfRepList " + NumberOfRepList.Count);
                Debug.Log("dialogist " + dialogist.Count);
                Debug.Log("DialogList " + DialogList.Count);
                Debug.Log("CounterList " + CounterList.Count);
                Debug.Log("animators " + animators.Count);
                Debug.Log("sources " + sources.Count);
                if (animators[i].ElementAt(0).GetNextAnimatorStateInfo(0).IsName("Idle") && NumberOfSpeakerList[i] == 1)
                {


                    sources[i].ElementAt(1).clip = DialogList[i][CounterList[i]];
                    animators[i].ElementAt(1).SetInteger("Trig", NumberOfRepList[i]);
                    animators[i].ElementAt(0).SetInteger("Trig", 0);

                    NumberOfSpeakerList[i] = 2;
                    NumberOfRepList[i]++;
                    CounterList[i]++;
                    if (CounterList[i] > DialogList[i].Count)
                    {
                       // CounterList[i]--;
                    }

                }
                if (animators[i].ElementAt(1).GetNextAnimatorStateInfo(0).IsName("Idle") && NumberOfSpeakerList[i] == 2)
                {


                    sources[i].ElementAt(0).clip = DialogList[i][CounterList[i]];
                    animators[i].ElementAt(0).SetInteger("Trig", NumberOfRepList[i]);
                    animators[i].ElementAt(1).SetInteger("Trig", 0);

                    NumberOfSpeakerList[i] = 1;
                    NumberOfRepList[i]++;
                    CounterList[i]++;
                    if (CounterList[i] > DialogList[i].Count)
                    {
                       // CounterList[i]--;
                    }

                }
                if (animators[i].ElementAt(0).GetCurrentAnimatorStateInfo(0).IsName("Idle") && animators[i].ElementAt(1).GetCurrentAnimatorStateInfo(0).IsName("Idle") && CounterList[i] + 1 >= DialogList[i].Count)
                {
                    StartConversList[i] = false;

                    animators[i].ElementAt(0).SetInteger("Trig", 0);
                    animators[i].ElementAt(1).SetInteger("Trig", 0);


                }
            }




        }

    }

    private void FillAllLists()
    {
        for (int i = 0; i < dialogist.Count; i++)
        {

            CounterList.Add(0);
            //StartConversList.Add(UnityEngine.Random.Range(0, 2) == 0);
            NumberOfSpeakerList.Add(UnityEngine.Random.Range(1, 3));
            GetAudioDialog();
            NumberOfRepList.Add(ParseNameReplica(i));

        }


    }

    private void StartDialogue(int i)
    {
        
            if (StartConversList.Count != 0 && StartConversList[i] && !isStartDialog)
            {
                isStartDialog = true;
                if (NumberOfSpeakerList[i] == 1)
                {
                    sources[i].ElementAt(0).clip = DialogList[i][CounterList[i]];
                    sources[i].ElementAt(1).clip = DialogList[i][CounterList[i] + 1];
                    animators[i].ElementAt(0).SetInteger("Trig", NumberOfRepList[i]);
                    animators[i].ElementAt(1).SetInteger("Trig", 0);

                    CounterList[i]++;
                    NumberOfRepList[i]++;

                }
                else
                {
                    sources[i].ElementAt(1).clip = DialogList[i][CounterList[i]];
                    sources[i].ElementAt(0).clip = DialogList[i][CounterList[i] + 1];
                    animators[i].ElementAt(1).SetInteger("Trig", NumberOfRepList[i]);
                    animators[i].ElementAt(0).SetInteger("Trig", 0);

                    CounterList[i]++;
                    NumberOfRepList[i]++;


                }
            }
        
    }

    private void ConversationAction()
    {

        for (int i = 0; i < dialogist.Count; i++)
        {


            if (StartConversList.Count != 0 && StartConversList[i])
            {
                Debug.Log("NumberOfRepList " + NumberOfRepList.Count);
                Debug.Log("dialogist " + dialogist.Count);
                Debug.Log("DialogList " + DialogList.Count);
                Debug.Log("CounterList " + CounterList.Count);
                Debug.Log("animators " + animators.Count);
                Debug.Log("sources " + sources.Count);
                if (animators[i].ElementAt(0).GetNextAnimatorStateInfo(0).IsName("Idle") && NumberOfSpeakerList[i] == 1)
                {


                    sources[i].ElementAt(1).clip = DialogList[i][CounterList[i]];
                    animators[i].ElementAt(1).SetInteger("Trig", NumberOfRepList[i]);
                    animators[i].ElementAt(0).SetInteger("Trig", 0);

                    NumberOfSpeakerList[i] = 2;
                    NumberOfRepList[i]++;
                    CounterList[i]++;
                }
                if (animators[i].ElementAt(1).GetNextAnimatorStateInfo(0).IsName("Idle") && NumberOfSpeakerList[i] == 2)
                {


                    sources[i].ElementAt(0).clip = DialogList[i][CounterList[i]];
                    animators[i].ElementAt(0).SetInteger("Trig", NumberOfRepList[i]);
                    animators[i].ElementAt(1).SetInteger("Trig", 0);

                    NumberOfSpeakerList[i] = 1;
                    NumberOfRepList[i]++;
                    CounterList[i]++;

                }
                if (animators[i].ElementAt(0).GetCurrentAnimatorStateInfo(0).IsName("Idle") && animators[i].ElementAt(1).GetCurrentAnimatorStateInfo(0).IsName("Idle") && CounterList[i] >= DialogList[i].Count)
                {
                    StartConversList[i] = false;

                    animators[i].ElementAt(0).SetInteger("Trig", 0);
                    animators[i].ElementAt(1).SetInteger("Trig", 0);


                }
            }




        }
    }

    private void InitializeDialogue()
    {
        CounterList.Clear();
        StartConversList.Clear();
        NumberOfSpeakerList.Clear();
        NumberOfRepList.Clear();
        DialogList.Clear();
    }




    public void AddDyadki()
    {
        DyadyaVityaPrefab = Resources.Load<GameObject>("Prefabs/DyadyaVitya");
        GameObject temp;
        float xCord = 4f;


        for (int i = 0; i < 5; i++)
        {
            temp = Instantiate(DyadyaVityaPrefab, new Vector3(xCord, 0f, 0f), Quaternion.Euler(new Vector3(0f, 0f, 0f)));
            temp.name = "Dayada000" + i.ToString();
            dyadki.Add(temp);
            xCord += 4f;

        }
        xCord = 4f;
        for (int i = 0; i < 5; i++)
        {
            temp = Instantiate(DyadyaVityaPrefab, new Vector3(xCord, 0f, 10f), Quaternion.Euler(new Vector3(0f, -180f, 0f)));
            temp.name = "Dayada000" + (i + 5).ToString();
            dyadki.Add(temp);
            xCord += 4f;
        }
    }

    public void CheckDyadki()
    {
        int temp;
        for (int i = 0; i < dyadki.Count; i++)
        {

            for (int j = 0; j < dyadki.Count; j++)
            {
                if (i != j && !dyadki[i].GetComponentInChildren<Speech1>().isAdded && !dyadki[j].GetComponentInChildren<Speech1>().isAdded)
                {
                    float dist = Vector3.Distance(dyadki[i].transform.position, dyadki[j].transform.position);


                    if (dist < 3.01f && dist > 0)
                    {

                        temp = UnityEngine.Random.Range(1, 3);
                        Debug.Log("Random " + temp);
                        dyadki[i].GetComponentInChildren<Speech1>().isAdded = true;
                        dyadki[j].GetComponentInChildren<Speech1>().isAdded = true;
                        dyadki[i].GetComponentInChildren<Speech1>().isMoving = false;
                        dyadki[j].GetComponentInChildren<Speech1>().isMoving = false;
                        if (temp == 1)
                        {
                            StartConversList.Add(true);
                            CounterList.Add(0);
                            NumberOfSpeakerList.Add(UnityEngine.Random.Range(1, 3));
                            GetAudioDialog();
                            Debug.Log("KEKW");
                            
                            while (timer < someAmount)
                            { 
                                timer += Time.deltaTime;
                            }
                            timer = 0f;

                            NumberOfRepList.Add(ParseNameReplica(counter));


                            dialogist.Add(new List<GameObject> { dyadki[i], dyadki[j] });
                            dyadki[i].GetComponentInChildren<Speech1>().isAdded = true;
                            dyadki[j].GetComponentInChildren<Speech1>().isAdded = true;
                            dyadki[i].GetComponentInChildren<Speech1>().isMoving = false;
                            dyadki[j].GetComponentInChildren<Speech1>().isMoving = false;
                            animators.Add(new List<Animator> { dyadki[i].GetComponentInChildren<Animator>(), dyadki[j].GetComponentInChildren<Animator>() });
                            sources.Add(new List<AudioSource> { dyadki[i].GetComponentInChildren<AudioSource>(), dyadki[j].GetComponentInChildren<AudioSource>() });
                            Debug.Log("Dialogist " + dialogist.Count);
                            StartDialogue(counter);
                        }
                        else
                        {
                            dyadki[i].GetComponentInChildren<Speech1>().isBack = true;
                            dyadki[j].GetComponentInChildren<Speech1>().isBack = true;
                        }




                    }




                }
            }

        }


        for (int i = 0; i < dialogist.Count; i++)
        {
            for (int j = 0; j < dialogist.Count; j++)
            {
                if (dialogist[i][0].name == dialogist[j][1].name && dialogist[i][1].name == dialogist[j][0].name)
                {
                    dialogist.RemoveAt(j);
                }
            }
            float dist = Vector3.Distance(dialogist[i][0].transform.position, dialogist[i][1].transform.position);
            if (dist > 3.01f)
            {
                dialogist[i][0].GetComponentInChildren<Speech1>().isAdded = false;
                dialogist[i][1].GetComponentInChildren<Speech1>().isAdded = false;
                dialogist.RemoveAt(i);

                Debug.Log(dialogist.Count);
            }
        }
    }

    public int ParseNameReplica(int num)
    {
        int numberRep1 = 0;
        string kekw = DialogList[num].ElementAt(0).name;
        numberRep1 = int.Parse(kekw.Substring(3));
        counter++;
        return numberRep1;

    }



    public void GetAudioDialog()
    {
        List<AudioClip> temp;

        temp = new();
        numberOfDialogue = UnityEngine.Random.Range(1, 4);
        temp.AddRange(Resources.LoadAll<AudioClip>($"Sound/Dialog{numberOfDialogue}"));
        DialogList.Add(temp);
    }

    public void GetRandomMonologue()
    {
        numberOfDialogue = UnityEngine.Random.Range(1, 37);
        actualDialog.Add(Resources.Load<AudioClip>($"Sound/Monologue/Replica ({numberOfDialogue})"));
    }
}

