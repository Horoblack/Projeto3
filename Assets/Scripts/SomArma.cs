using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomArma : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] shootSounds; // Array para armazenar os sons de tiro
    public float volume = 0.5f;

    public AudioClip reloadSound; 
    public float reloadVolume = 0.5f; 

    private void OnEnable()
    {
        // Registra o método para ser chamado quando um tiro for disparado.
        Shooting.OnShoot += HandleShootEvent;
    }

    private void OnDisable()
    {
        // Remove o método registrado ao desativar o script.
        Shooting.OnShoot -= HandleShootEvent;
    }

    private void HandleShootEvent(bool shot)
    {
        if (shot)
        {
            // Se um tiro foi disparado, reproduza o som.
            PlayShootSound();
        }
    }

    private void PlayShootSound()
    {
        if (audioSource != null && shootSounds.Length > 0)
        {
            // Escolhe um índice aleatório do array de sons de tiro
            int randomIndex = Random.Range(0, shootSounds.Length);

            // Define o som a ser reproduzido com base no índice aleatório
            AudioClip selectedSound = shootSounds[randomIndex];

            // Define o som selecionado para o AudioSource
            audioSource.clip = selectedSound;

            // Define o volume e reproduz o som
            audioSource.volume = volume;
            audioSource.Play();
        }
    }

    public void PlayReloadSound()
    {
        if (audioSource != null && reloadSound != null)
        {
            // Define o som de recarga para o AudioSource
            audioSource.clip = reloadSound;

            // Define o volume e reproduz o som
            audioSource.volume = reloadVolume;
            audioSource.Play();
        }
    }
}
