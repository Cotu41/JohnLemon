using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    public GameEnding gameEnding;

    bool m_IsPlayerInRange;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == player)
        {
            player.GetComponent<CharacterController>().TakeDamage(33);
        }
    }


}
