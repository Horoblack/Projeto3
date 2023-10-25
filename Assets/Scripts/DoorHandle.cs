using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandle : MonoBehaviour
{
    public Animator doorAnim;
  

    void OnTriggerEnter(Collider playerEnter)
    {
        if (playerEnter.CompareTag("Player") && DoorCards.blueCard)
        {
            Debug.Log("abre");
            doorAnim.SetBool("isOpen",true);          
        }
      
    }


    void OnTriggerExit(Collider playerExit)
    {
        if (playerExit.CompareTag("Player"))
        {
            Debug.Log("fecha");
            doorAnim.SetBool("isOpen", false);

        }
    }
  
}
