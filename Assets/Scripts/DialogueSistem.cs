using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSistem : MonoBehaviour
{
    public Text interactText;
    public Text storyText;
    public Image backgroundImage;
    [Range(0.1f, 10.0f)] public float distancia = 3;
    private GameObject jogador;
    private bool isInRange = false;
    private bool storyStarted = false;

    private string[] storyParts;
    private int currentPartIndex = 0;

    private bool isPlayerBlocked = false;
    private PlayerMove playerController;

    private float originalTimeScale;
    private float textDisplayTimer = 0f; // Usado para controlar a velocidade de exibição
    private float textDisplaySpeed = 0.03f; // Velocidade de exibição do texto

    private string currentDisplayText = "";
    private int textIndex = 0;
    private bool isDisplayingText = false;
    public GameObject playerGo;
    public GameObject invGo;


    void Start()
    {
        playerGo = GameObject.FindGameObjectWithTag("Player");
        invGo = GameObject.FindGameObjectWithTag("Inventario");

        interactText.enabled = false;
        storyText.enabled = false;
        jogador = GameObject.FindWithTag("Player");
        backgroundImage.enabled = false;

        storyParts = new string[]
        {
        "Parte 1 do diálogo - Saiba que você tera que resolver alguns enigmas da nave pra pegar os cartões.",
        "Parte 2 do diálogo - Tem alguma coisa de errado acontecendo e você vai descobrir.",
        "Parte 3 do diálogo - Tenha muito cuidado por onde andar, se não quiser ser pego de surpresa.",
            // Adicione quantas partes do diálogo desejar
        };

        originalTimeScale = Time.timeScale;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, jogador.transform.position);

        if (!storyStarted && distanceToPlayer < distancia)
        {
            interactText.enabled = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                StartStory();
            }
        }
        else
        {
            interactText.enabled = false;
        }

        if (isInRange && Input.GetKeyDown(KeyCode.Space))
        {
            ContinueStory();
        }

        // Atualizar o efeito de escrita independentemente do Time.timeScale
        if (isDisplayingText)
        {
            textDisplayTimer += Time.unscaledDeltaTime; // Use Time.unscaledDeltaTime para não ser afetado pelo Time.timeScale

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
        invGo.GetComponent<Inventario>().enabled = false;

        storyStarted = true;
        interactText.enabled = false;
        storyText.enabled = true;
        backgroundImage.enabled = true;
        isInRange = true;
        isPlayerBlocked = true;

        currentDisplayText = "";
        textIndex = 0;
        isDisplayingText = true;

        storyText.text = currentDisplayText;

        // Pausar o jogo
        Time.timeScale = 0f;
    }

    private void ContinueStory()
    {
        if (isDisplayingText)
        {
            // Mostrar o texto inteiro imediatamente se o jogador clicar enquanto está sendo escrito
            currentDisplayText = storyParts[currentPartIndex];
            storyText.text = currentDisplayText;
            textIndex = storyParts[currentPartIndex].Length;
            isDisplayingText = false;
        }
        else
        {
            currentPartIndex++;

            if (currentPartIndex < storyParts.Length)
            {
                currentDisplayText = "";
                textIndex = 0;
                isDisplayingText = true;
            }
            else
            {
                FinishStory();
            }
        }
    }

    private void FinishStory()
    {
        currentPartIndex = 0;
        storyStarted = false;
        isInRange = false;
        storyText.enabled = false;
        backgroundImage.enabled = false;
        Time.timeScale = originalTimeScale;
        isPlayerBlocked = false;
        playerGo.GetComponent<PlayerMove>().enabled = true;
        invGo.GetComponent<Inventario>().enabled = true;
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
