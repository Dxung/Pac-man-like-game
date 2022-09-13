using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletParticleSystem : MonoBehaviour
{
    
    

    //this script will work when pellet be touched by player
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("da va cham");
        }
    }
}
