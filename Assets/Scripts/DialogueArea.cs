using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueArea : MonoBehaviour
{
    
    public Text storyText;
    public Image backgroundImage;
    private GameObject jogador;
    [Range(0.1f, 10.0f)] public float distancia = 3;

    private bool dialoguePlayed = false; // Vari�vel para verificar se o di�logo j� foi reproduzido
    private string[] storyParts;
    private int currentPartIndex = 0;

    private bool isPlayerBlocked = false;
    private PlayerMove playerController;

    private float originalTimeScale;
    private float textDisplayTimer = 0f; // Usado para controlar a velocidade de exibi��o
    private float textDisplaySpeed = 0.03f; // Velocidade de exibi��o do texto

    private string currentDisplayText = "";
    private int textIndex = 0;
    private bool isDisplayingText = false;
    public GameObject playerGo;
    


    void Start()
    {
        playerGo = GameObject.FindGameObjectWithTag("Player");
        
        
        storyText.enabled = false;
        jogador = GameObject.FindWithTag("Player");
        backgroundImage.enabled = false;

        storyParts = new string[]
        {
        "Ol�, meu nome � ..... sou a inteligencia artificial da nave, voc� acaba de acordar de um sono profundo.",
        "A Nave esta sendo invadida por criaturas fora do comum, e s� voc� podera me ajudar a elimina-las.",
        "Para isso preciso que voc� encontre 3 cart�es, e de eles para mim, assim eu posso ter acesso as armas da nave.",
            // Adicione quantas partes do di�logo desejar
        };

        originalTimeScale = Time.timeScale;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !dialoguePlayed) // Verifica se o jogador colidiu e se o di�logo ainda n�o foi reproduzido
        {
            StartStory();
            dialoguePlayed = true; // Marca o di�logo como reproduzido para evitar repeti��es
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FinishStory();
        }
    }


    void Update()
    {
        if (isPlayerBlocked && Input.GetKeyDown(KeyCode.Space))
        {
            if (isDisplayingText)
            {
                // Mostrar o texto inteiro imediatamente se o jogador pressionar espa�o enquanto est� sendo escrito
                currentDisplayText = storyParts[currentPartIndex];
                storyText.text = currentDisplayText;
                textIndex = storyParts[currentPartIndex].Length;
                isDisplayingText = false;
            }
            else
            {
                // Passar para a pr�xima parte do di�logo quando o jogador pressionar espa�o
                currentPartIndex++;
                if (currentPartIndex < storyParts.Length)
                {
                    currentDisplayText = "";
                    textIndex = 0;
                    isDisplayingText = true;
                }
                else
                {
                    // Se todas as partes do di�logo foram exibidas, encerrar o di�logo
                    FinishStory();
                }
            }
        }

        // Atualizar o efeito de escrita independentemente do Time.timeScale
        if (isDisplayingText)
        {
            textDisplayTimer += Time.unscaledDeltaTime; // Use Time.unscaledDeltaTime para n�o ser afetado pelo Time.timeScale

            if (textDisplayTimer >= textDisplaySpeed)
            {
                DisplayNextCharacter();
                textDisplayTimer = 0f; // Resetar o timer
            }
        }
    }

    private void StartStory()
    {
        playerGo.GetComponent<PlayerMove>().enabled = false;
        

        storyText.enabled = true;
        backgroundImage.enabled = true;
        isPlayerBlocked = true;

        currentDisplayText = "";
        textIndex = 0;
        isDisplayingText = true;

        storyText.text = currentDisplayText;

        // Pausar o jogo
        Time.timeScale = 0f;
    }

    

    private void FinishStory()
    {
        currentPartIndex = 0;
        storyText.enabled = false;
        backgroundImage.enabled = false;
        Time.timeScale = originalTimeScale;
        isPlayerBlocked = false;
        playerGo.GetComponent<PlayerMove>().enabled = true;
        
    }

    private void DisplayNextCharacter()
    {
        if (textIndex < storyParts[currentPartIndex].Length)
        {
            currentDisplayText += storyParts[currentPartIndex][textIndex];
            storyText.text = currentDisplayText;
            textIndex++;
        }
        else
        {
            isDisplayingText = false;
        }
    }
}
