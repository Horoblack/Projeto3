using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomPassos : MonoBehaviour
{
    public AudioSource audioSource;
    public float volume = 0.5f;
    public float cooldownTime = 1.0f; // Tempo de cooldown após o término do dash

    private bool estaAndando = false;
    private float lastStepTime;
    private bool isCooldown = false;
    private float cooldownTimer = 0f;

    // Adicione uma referência ao script PlayerMove.
    public PlayerMove playerMove;

    private void Start()
    {
        // Certifique-se de atribuir o objeto PlayerMove ao playerMove no Unity Inspector.
        playerMove = FindObjectOfType<PlayerMove>();

        // Inscreva-se nos eventos de início e fim do dash
        
    }

    private void OnDestroy()
    {
        // Certifique-se de cancelar a inscrição nos eventos ao destruir o objeto
      
    }

    private void OnDashStart()
    {
        // Pausa o som de passos quando o dash começa
        if (estaAndando)
        {
            audioSource.Stop();
            estaAndando = false;
        }
    }

    private void OnDashEnd()
    {
        // Configura o cooldown após o término do dash
        isCooldown = true;
        cooldownTimer = 0f;
    }

    private void Update()
    {
        // Lógica para detecção de movimento, ajuste conforme sua implementação.
        if (playerMove != null && !playerMove.IsDead)
        {
            // Controle normal do som de passos.
            if (!playerMove.IsDashing && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
            {
                if (!estaAndando && !isCooldown)
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
                    // Verifica se tempo suficiente passou desde o último passo
                    if (!isCooldown || (Time.time - lastStepTime >= cooldownTime))
                    {
                        audioSource.Stop();
                        estaAndando = false;
                        lastStepTime = Time.time;
                    }
                }
            }

            // Atualiza o cooldown após o término do dash
            if (isCooldown)
            {
                cooldownTimer += Time.deltaTime;

                if (cooldownTimer >= cooldownTime)
                {
                    isCooldown = false;
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


