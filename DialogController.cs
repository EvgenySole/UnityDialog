using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    public static string dialog = "";
    string oldDialog = "1";

    public static bool isLocked = false;
    public bool oldLocked = true;
    public static int numOfConvers = 0;

    public int num = 2;

    // Start is called before the first frame update
    void Start()
    {
        //numOfConvers = Random.Range(1, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (dialog.Length != 0 && dialog != oldDialog){
            print(dialog);
            oldDialog = dialog;
        }
        
        num = numOfConvers;
        oldLocked = isLocked;
    }

    void SetDialog(string dialog){
       // this.dialog = dialog;
    }

    public static void setNumOfConvers(){
        numOfConvers = Random.Range(1, 2);
    }
}
