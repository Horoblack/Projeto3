using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandle : MonoBehaviour
{
    public Animator doorAnim;
    public bool needCard;
    public cardColors _cardColors;
    public float distanceToOpen = 4.0f; // Distância a partir da qual a porta pode ser aberta
    public float distanceToClose = 4.0f; // Distância a partir da qual a porta deve ser fechada
    public bool FirstTime = true;
    public AudioClip trancada;
    public AudioClip destrancada;

    public enum cardColors
    {
        blue,
        red,
        yellow,
        energy
    }

    private bool isDoorOpen = false;
    private Transform playerTransform; // Variável para armazenar a referência do jogador

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(CheckCard())
            {
             PlayerMove.instance.playerAudio.PlayOneShot(trancada);
            }
            playerTransform = other.transform; // Armazena a referência do jogador
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CloseDoor();
        }
    }

    // Adicionado método para verificar se o jogador tem o cartão correto
    private bool CheckCard()
    {
        bool hasCard = false;

        switch (_cardColors)
        {
            case cardColors.blue:
                hasCard = DoorCards.HasblueCard;
                break;
            case cardColors.red:
                hasCard = DoorCards.HasredCard;
                break;
            case cardColors.yellow:
                hasCard = DoorCards.HasYellowCard;
                break;
            case cardColors.energy:
                hasCard = DoorCards.EnergyRestored;
                break;
        }

     
        return hasCard;
    }

    private bool CanOpenDoor()
    {
        return playerTransform != null && (!needCard || (needCard && CheckCard())) && Vector3.Distance(transform.position, playerTransform.position) < distanceToOpen;
        
    }

    private bool CanCloseDoor()
    {
        return playerTransform != null && Vector3.Distance(transform.position, playerTransform.position) > distanceToClose;
    }

    private void OpenDoor()
    {
        if (!isDoorOpen)
        {
            doorAnim.ResetTrigger("close");
            doorAnim.SetTrigger("open");
            isDoorOpen = true;
        }
    }

    private void CloseDoor()
    {
        if (isDoorOpen)
        {
            doorAnim.ResetTrigger("open");
            doorAnim.SetTrigger("close");
            isDoorOpen = false;
        }
    }

    void Update()
    {
        if (!isDoorOpen && CanOpenDoor())
        {
           
            OpenDoor();
        }
        else if (isDoorOpen && CanCloseDoor())
        {
            CloseDoor();
        }

       
           // PlayerMove.instance.playerAudio.PlayOneShot(trancada);
    }
}