using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endMark : MonoBehaviour
{
    [SerializeField]
    [Range(1, 100)] public float velocidadeDireita = 2f;
    [Range(1, 100)] public float velocidadeRotacao = 2f;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float anguloRotacao = velocidadeRotacao * Time.deltaTime;
        Quaternion rotacaoDiagonal = Quaternion.Euler(new Vector3(anguloRotacao, anguloRotacao,0f));
        transform.Translate(Vector3.right * velocidadeDireita * Time.deltaTime, Space.World);
        transform.rotation *= rotacaoDiagonal;
    }
}
