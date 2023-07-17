using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class DialogController2 : MonoBehaviour
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

    public List<bool> StartConversList = new();
    public List<int> NumberOfSpeakerList = new();
    public List<int> NumberOfRepList = new();
    public List<int> CounterList = new();
    public List<List<AudioClip>> DialogList = new();


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
        FillAllLists();


    }

    private void FillAllLists()
    {
        for (int i = 0; i < dialogist.Count; i++)
        {

            CounterList.Add(0);
            StartConversList.Add(UnityEngine.Random.Range(0, 2) == 0);
            NumberOfSpeakerList.Add(UnityEngine.Random.Range(1, 3));
            GetAudioDialog();
            NumberOfRepList.Add(ParseNameReplica(i));

        }

        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            actualDialog.Clear();
            CounterList.Clear();
            StartConversList.Clear();
            NumberOfSpeakerList.Clear();
            NumberOfRepList.Clear();
            DialogList.Clear();
            FillAllLists();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {

            for (int i = 0; i < dialogist.Count; i++)
            {
                if (StartConversList.Count != 0 && StartConversList[i])
                {
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

            //StartCoroutine(Dialogue());

        }

        for (int i = 0; i < dialogist.Count; i++)
        {


            if (StartConversList.Count != 0 && StartConversList[i])
            {
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


    // Update is called once per frame
    void Update123()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            actualDialog.Clear();

            NumberOfSpeaker = UnityEngine.Random.Range(1, 3);
            StartConversation = true;
            counter = 0;
            GetAudioDialog();
            //CurrentForming();
            ParseNameReplica(NumberOfSpeaker);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //StartCoroutine(Dialogue());
            if (NumberOfSpeaker == 1)
            {
                Debug.Log(sources.Count);

                for (int i = 0; i < dialogist.Count; i++)
                {
                    sources[i].ElementAt(0).clip = actualDialog[counter];
                    sources[i].ElementAt(1).clip = actualDialog[counter + 1];
                    animators[i].ElementAt(0).SetInteger("Trig", numberRep);
                    animators[i].ElementAt(1).SetInteger("Trig", 0);
                }


                audioSource1.clip = actualDialog[counter];
                audioSource2.clip = actualDialog[counter + 1];
                animator1st.SetInteger("Trig", numberRep);


                counter++;
                numberRep++;
            }
            else
            {
                Debug.Log(sources.Count);

                for (int i = 0; i < dialogist.Count; i++)
                {
                    sources[i].ElementAt(1).clip = actualDialog[counter];
                    sources[i].ElementAt(0).clip = actualDialog[counter + 1];
                    animators[i].ElementAt(1).SetInteger("Trig", numberRep);
                    animators[i].ElementAt(0).SetInteger("Trig", 0);
                }


                audioSource2.clip = actualDialog[counter];
                audioSource1.clip = actualDialog[counter + 1];
                animator2nd.SetInteger("Trig", numberRep);


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
                    animators[i].ElementAt(1).SetInteger("Trig", numberRep);
                    animators[i].ElementAt(0).SetInteger("Trig", 0);
                }


                audioSource2.clip = actualDialog[counter];
                animator1st.SetInteger("Trig", 0);
                animator2nd.SetInteger("Trig", numberRep);

                NumberOfSpeaker = 2;
                numberRep++;
                counter++;
            }
            if (animators[0].ElementAt(1).GetNextAnimatorStateInfo(0).IsName("Idle") && NumberOfSpeaker == 2)
            {

                for (int i = 0; i < dialogist.Count; i++)
                {
                    sources[i].ElementAt(0).clip = actualDialog[counter];
                    animators[i].ElementAt(0).SetInteger("Trig", numberRep);
                    animators[i].ElementAt(1).SetInteger("Trig", 0);
                }

                audioSource1.clip = actualDialog[counter];
                animator2nd.SetInteger("Trig", 0);
                animator1st.SetInteger("Trig", numberRep);

                NumberOfSpeaker = 1;
                numberRep++;
                counter++;

            }
            if (animators[0].ElementAt(0).GetCurrentAnimatorStateInfo(0).IsName("Idle") && animators[0].ElementAt(1).GetCurrentAnimatorStateInfo(0).IsName("Idle") && counter >= actualDialog.Count)
            {
                StartConversation = false;

                for (int i = 0; i < dialogist.Count; i++)
                {
                    animators[i].ElementAt(0).SetInteger("Trig", 0);
                    animators[i].ElementAt(1).SetInteger("Trig", 0);
                }



                animator1st.SetInteger("Trig", 0);
                animator2nd.SetInteger("Trig", 0);

            }
        }

    }

    IEnumerator Dialogue()
    {
        yield return null;
        if (StartConversation)
        {
            for (int i = 0; i < currentDialog1st.Count; i++)
            {
                if (NumberOfSpeaker == 1 && animator1st.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    animator2nd.SetInteger("Trig", 0);
                    audioSource1.clip = currentDialog1st[i];
                    animator1st.SetInteger("Trig", numberRep++);

                    yield return new WaitWhile(() => animator1st.GetInteger("Trig") != 0);
                    NumberOfSpeaker = 2;
                    //audioSource1st.clip = null;
                }
                if (NumberOfSpeaker == 2 && animator2nd.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    animator1st.SetInteger("Trig", 0);
                    audioSource2.clip = currentDialog2nd[i];
                    animator2nd.SetInteger("Trig", numberRep++);

                    yield return new WaitWhile(() => animator2nd.GetInteger("Trig") != 0);
                    NumberOfSpeaker = 1;
                    //audioSource2nd.clip = null;
                }
            }

            animator1st.SetInteger("Trig", 0);
            animator2nd.SetInteger("Trig", 0);
            StartConversation = false;
        }
    }

    void CurrentForming()
    {

        for (int j = 0; j < actualDialog.Count; j++)
        {
            if (j % 2 == 0)
            {
                currentDialog1st.Add(actualDialog[j]);
            }
            else
            {
                currentDialog2nd.Add(actualDialog[j]);
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
            temp = Instantiate(DyadyaVityaPrefab, new Vector3(xCord, 0f, 3f), Quaternion.Euler(new Vector3(0f, -180f, 0f)));
            temp.name = "Dayada000" + (i + 5).ToString();
            dyadki.Add(temp);
            xCord += 4f;
        }

        /*for (int i = 0;i < dyadki.Count;i++)
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
        }*/

        for (int i = 0; i < 5; i++)
        {
            dialogist.Add(new List<GameObject> { dyadki[i], dyadki[i + 5] });
            animators.Add(new List<Animator> { dyadki[i].GetComponentInChildren<Animator>(), dyadki[i + 5].GetComponentInChildren<Animator>() });
            sources.Add(new List<AudioSource> { dyadki[i].GetComponentInChildren<AudioSource>(), dyadki[i + 5].GetComponentInChildren<AudioSource>() });
        }
    }

    public int ParseNameReplica(int num)
    {
        int numberRep1 = 0;
        string kekw = DialogList[num].ElementAt(0).name;
        numberRep1 = int.Parse(kekw.Substring(3));
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

