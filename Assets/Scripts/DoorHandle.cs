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
        red,
        yellow
    }

    void OnTriggerEnter(Collider playerEnter)
    {
        if (playerEnter.CompareTag("Player") && needCard == false)
        {
            doorAnim.ResetTrigger("close");
            doorAnim.SetTrigger("open");
        }
        else if(playerEnter.CompareTag("Player") && needCard)
        {
           switch (_cardColors)
            {
                case cardColors.blue:
                    if (DoorCards.HasblueCard)
                    {
                        doorAnim.ResetTrigger("close");
                        doorAnim.SetTrigger("open");
                    }
                    else
                        Debug.Log("não possui!");
                    break;

                case cardColors.red:
                    if (DoorCards.HasredCard)
                    {
                        doorAnim.ResetTrigger("close");
                        doorAnim.SetTrigger("open");
                    }
                    else
                        Debug.Log("não possui!");
                    break;

                case cardColors.yellow:
                    if (DoorCards.HasYellowCard)
                    {
                        doorAnim.ResetTrigger("close");
                        doorAnim.SetTrigger("open");
                    }
                    else
                        Debug.Log("não possui!");
                    break;
            }
        }
    }


    void OnTriggerExit(Collider playerEnterr)
    {
        if (playerEnterr.CompareTag("Player"))
        {
            doorAnim.ResetTrigger("open");
            doorAnim.SetTrigger("close");

        }
    }
  
}
