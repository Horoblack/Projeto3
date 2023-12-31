using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    public float creditsMoveSpeed = 1.0f;
    public AudioClip sceneaudio;
    public GameObject voltarGO;

    private void Start()
    {
        StartCoroutine(MoveCredits());
        voltarGO.SetActive(false);
    }

    IEnumerator MoveCredits()
    {
        RectTransform creditsRectTransform = GetComponent<RectTransform>();

        // Obter a posi��o inicial e final dos cr�ditos
        Vector3 startPos = creditsRectTransform.position;
        Vector3 endPos = startPos - new Vector3(0, 1000, 0); // Ajuste conforme necess�rio

        float startTime = Time.time;
        float duration = 18.0f; // Ajuste a dura��o conforme necess�rio

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            creditsRectTransform.position = Vector3.Lerp(startPos, endPos, t * creditsMoveSpeed);
            yield return null;
          
        }
        voltarGO.SetActive(true);
        // Aguardar um pouco antes de carregar a cena do menu
        yield return new WaitForSeconds(sceneaudio.length); // Ajuste conforme necess�rio

        // Carregar a cena do menu
        SceneManager.LoadScene("Menu");
    }

    private void Update()
    {
        if (voltarGO.active)
        {
            if(Input.GetKeyDown(KeyCode.Space)) {
                SceneManager.LoadScene("Menu");
            }
        }
    }
}