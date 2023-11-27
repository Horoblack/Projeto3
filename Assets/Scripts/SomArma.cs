using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomArma : MonoBehaviour
{
    public AudioSource audioSource;
    public float volume = 0.5f;

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
        if (audioSource != null)
        {
            audioSource.volume = volume;
            audioSource.Play();
        }
    }
}
