using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    private bool isPaused;
    public GameObject pausePanel;
    public GameObject playerGo;
    public string cena;
    public GameObject opcoesPanel;


    // Start is called before the first frame update
    void Start()
    {
        
        Time.timeScale = 1f;
        playerGo = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape))
       {
            PauseScreen();
       } 

    }

    public void ContinuarJogo()
    {
        playerGo.GetComponent<PlayerMove>().enabled = true;
        isPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    void PauseScreen()
    {
        if (isPaused)
        {
            playerGo.GetComponent<PlayerMove>().enabled = true;
            isPaused = false;
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
        }
        else
        {
            isPaused = true;
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
            playerGo.GetComponent<PlayerMove>().enabled = false;
        }
    }

    public void MostrarOpcoes()
    {
      

        // Ativa o painel de opções
        opcoesPanel.SetActive(true);

        // Garante que o jogo não esteja pausado
        if (isPaused)
        {
            ContinuarJogo();
        }
    }

    public void VoltarPainelPause()
    {
        opcoesPanel.SetActive(false);
    }

    public void VoltarMenu()
    {
       
        SceneManager.LoadScene("Menu");
    }
}
