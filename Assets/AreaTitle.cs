using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaTitle : MonoBehaviour
{
    Image renderer;
    AudioSource sound;


    

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Image>();
        sound = GetComponent<AudioSource>();
    }

    


    // Update is called once per frame
    void Update()
    {
        
    }
}
