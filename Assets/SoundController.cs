using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource dashSound;
    public float volumeDash = 0.5f;

    private bool isDashSoundPlaying = false;

    public AudioSource itemPickupSound;
    public float volumeItemPickup = 0.5f;

    public PlayerMove playerMove;
   

    private static SoundController instance;

    public static SoundController Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    { 

        // Certifique-se de atribuir o objeto PlayerMove ao playerMove no Unity Inspector.
        playerMove = FindObjectOfType<PlayerMove>();
        
    }

    public bool IsDashSoundPlaying()
    {
        return isDashSoundPlaying;
    }

    public bool IsPlayerDead()
    {
        // Certifique-se de ajustar conforme a lógica específica do seu jogo.
        return playerMove != null && playerMove.IsDead;
    }

    private void Update()
    {
        if (playerMove != null && !playerMove.IsDead)
        {
            // Controle do som do dash
            if (Input.GetKeyDown(KeyCode.Space) && playerMove.IsDashAnimationPlaying())
            {
                PlayDashSound();
            }

            // Controle do som de pegar itens
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayItemPickupSound();
            }

            
        }

        
    }

    public void StartDashSound(bool isStarting)
    {
       

        if (IsPlayerDead())
        {
            // Não reproduza o som se o jogador estiver morto
            return;
        }

        if (isStarting && !isDashSoundPlaying)
        {
            // Inicia o som do Dash apenas se o Dash está começando e o som não está sendo reproduzido
            isDashSoundPlaying = true;
            PlayDashSound();
        }
        else if (!isStarting && isDashSoundPlaying)
        {
            // Para o som do Dash apenas se o Dash está terminando e o som está sendo reproduzido
            isDashSoundPlaying = false;
            // Adicione qualquer lógica adicional necessária quando o Dash terminar
        }
    }


    void PlayDashSound()
    {
        Debug.Log("Playing Dash Sound");
        if (dashSound != null)
        {
            dashSound.volume = volumeDash;
            dashSound.Play();
        }
    }

    public void StopDashSound()
    {
        isDashSoundPlaying = false;
        // Adicione qualquer lógica adicional necessária quando o Dash terminar
    }

    void PlayItemPickupSound()
    {
        if (itemPickupSound != null)
        {
            itemPickupSound.volume = volumeItemPickup;
            itemPickupSound.Play();
        }
    }

    
}
