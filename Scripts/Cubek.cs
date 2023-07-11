using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubek : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Keypad8))
        {
            this.transform.position += new Vector3(0, 0, -0.1f);
        }
        if (Input.GetKey(KeyCode.Keypad2))
        {
            this.transform.position += new Vector3(0, 0, 0.1f);
        }
        if (Input.GetKey(KeyCode.Keypad6))
        {
            this.transform.position += new Vector3(-0.1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.Keypad4))
        {
            this.transform.position += new Vector3(0.1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.Keypad0))
        {
            this.transform.position += new Vector3(0, 1, 0);
        }    

    }
}
