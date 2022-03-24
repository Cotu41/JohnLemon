using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInfoManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BubblesTheSudsy.FightBegins += BubblesTheSudsy_FightBegins;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void BubblesTheSudsy_FightBegins()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
