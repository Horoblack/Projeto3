using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandle : MonoBehaviour
{
    public Animator doorAnim;
    public bool needCard;
    public cardColors _cardColors;

    public enum cardColors
    {
        blue,
        red
    }

    void OnTriggerEnter(Collider playerEnter)
    {
        if (playerEnter.CompareTag("Player") && needCard == false)
        {
            Debug.Log("abre");
            doorAnim.SetBool("isOpen",true);          
        }
        else if(playerEnter.CompareTag("Player") && needCard)
        {
           switch (_cardColors)
            {
                case cardColors.blue:
                    if (DoorCards.HasblueCard)
                        doorAnim.SetBool("isOpen", true);
                    else
                        Debug.Log("não possui!");
                    break;

                case cardColors.red:
                    if (DoorCards.HasredCard)
                        doorAnim.SetBool("isOpen", true);
                    else
                        Debug.Log("não possui!");
                    break;
            }
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
