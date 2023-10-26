using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    public string cena;
    public GameObject optionsPanel;


    public void Jogar()
    {
        SceneManager.LoadScene(cena);
    }

    public void SairDoJogo()
    {
        //Editor Unity
        UnityEditor.EditorApplication.isPlaying = false;

        //Jogo Compilado/buildado (descomentar essa de baixo e comentar a de cima).
        //Application.Quit();
    }

    public void ShowOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void VoltarAoMenu()
    {
        optionsPanel.SetActive(false);
    }
}
