using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMessage : MonoBehaviour
{
    // Start is called before the first frame update
    public delegate void MessageEvent(string message);
    public static event MessageEvent OnMessageShown, OnMessageHidden;


    public string message = "dog ahead";

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag.Equals("Player"))
            OnMessageShown(message);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag.Equals("Player"))
            OnMessageHidden(message);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
