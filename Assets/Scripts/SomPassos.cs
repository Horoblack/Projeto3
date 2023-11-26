using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomPassos : MonoBehaviour
{
    public AudioSource audioSource;
    public float volume = 0.5f;

    private bool estaAndando = false;

    // Adicione uma refer�ncia ao script PlayerMove.
    public PlayerMove playerMove;

    private void Start()
    {
        // Certifique-se de atribuir o objeto PlayerMove ao playerMove no Unity Inspector.
        playerMove = FindObjectOfType<PlayerMove>();
    }

    private void Update()
    {
        // L�gica para detec��o de movimento, ajuste conforme sua implementa��o.
        if (playerMove != null && !playerMove.IsDead)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                if (!estaAndando)
                {
                    audioSource.volume = volume;
                    audioSource.loop = true;
                    audioSource.Play();
                    estaAndando = true;
                }
            }
            else
            {
                if (estaAndando)
                {
                    audioSource.Stop();
                    estaAndando = false;
                }
            }
        }
        else
        {
            // Pare o som se o jogador estiver morto.
            if (estaAndando)
            {
                audioSource.Stop();
                estaAndando = false;
            }
        }
    }
}

