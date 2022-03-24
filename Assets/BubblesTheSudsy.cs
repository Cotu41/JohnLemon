using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblesTheSudsy : MonoBehaviour
{

    bool murdermode = false;

    public float riseSpeed = 0.3f;
    public float attackSpeed = 10f;

    public CharacterController player;

    public delegate void BossEvent();
    public static event BossEvent FightBegins, InflictStatusEffects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player") && !murdermode)
        {
            destroyPunyPlayer();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.Equals("Player") && murdermode)
        {
            player.TakeDamage(1000);
        }

    }


    public void destroyPunyPlayer()
    {
        murdermode = true;
        FightBegins();
        StartCoroutine(FloatUp());
    }

    IEnumerator FloatUp()
    {
        while(transform.position.y < 1.99f)
        {
           
            transform.position = Vector3.Lerp(transform.position, transform.position + (Vector3.up * 2), Time.deltaTime * riseSpeed);
            yield return null;

        }
        StartCoroutine(Annihilate());
    }

    IEnumerator Annihilate()
    {
        yield return new WaitForSeconds(6);
        transform.LookAt(player.transform);
        while(true)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime*attackSpeed);
            yield return null;
        }
    }

    void useStatusFog()
    {
        GetComponentInChildren<ParticleSystem>().Play();
        InflictStatusEffects();
    }
}
