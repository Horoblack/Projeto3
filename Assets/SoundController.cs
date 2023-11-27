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

    // Refer�ncia ao script PlayerMove para verificar o estado do jogador.
    public PlayerMove playerMove;

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
        // Certifique-se de ajustar conforme a l�gica espec�fica do seu jogo.
        return playerMove != null && playerMove.IsDead;
    }

    private void Update()
    {
        // Verifique se o jogador est� vivo antes de permitir a reprodu��o de sons.
        if (playerMove != null && !playerMove.IsDead)
        {
            // Controle do som do dash
            if (Input.GetKeyDown(KeyCode.Space))
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

    void PlayDashSound()
    {
        if (dashSound != null)
        {
            dashSound.volume = volumeDash;
            dashSound.Play();
        }
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
