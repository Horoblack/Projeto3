using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    public float creditsMoveSpeed = 1.0f;

    private void Start()
    {
        StartCoroutine(MoveCredits());
    }

    IEnumerator MoveCredits()
    {
        RectTransform creditsRectTransform = GetComponent<RectTransform>();

        // Obter a posição inicial e final dos créditos
        Vector3 startPos = creditsRectTransform.position;
        Vector3 endPos = startPos - new Vector3(0, 1000, 0); // Ajuste conforme necessário

        float startTime = Time.time;
        float duration = 18.0f; // Ajuste a duração conforme necessário

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            creditsRectTransform.position = Vector3.Lerp(startPos, endPos, t * creditsMoveSpeed);
            yield return null;
        }

        // Aguardar um pouco antes de carregar a cena do menu
        yield return new WaitForSeconds(2.0f); // Ajuste conforme necessário

        // Carregar a cena do menu
        SceneManager.LoadScene("Menu");
    }
}