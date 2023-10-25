using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class ItemCard : MonoBehaviour
{

    public int IDDesteCartao = 0;
    [Range(0.1f, 10.0f)] public float distanciaDoCartao = 3;
    public KeyCode TeclaPegar = KeyCode.E;
    public AudioClip somPegarCartao;
    AudioSource emissorDeSom;
    GameObject Jogador;
    Cards _listaDeCartoes;
    bool jaPegou;

    void Start()
    {
        jaPegou = false;
        Jogador = GameObject.FindWithTag("Player");
        if (Jogador != null)
        {
            _listaDeCartoes = Jogador.GetComponent<Cards>();
        }
        emissorDeSom = GetComponent<AudioSource>();
        emissorDeSom.playOnAwake = false;
        emissorDeSom.loop = false;
    }

    void Update()
    {
        if (Jogador != null && _listaDeCartoes != null)
        {
            if (jaPegou == false)
            {
                float distancia = Vector3.Distance(Jogador.transform.position, transform.position);
                if (distancia < distanciaDoCartao)
                {
                    if (Input.GetKeyDown(TeclaPegar))
                    {
                        _listaDeCartoes.CartoesDoJogador.Add(IDDesteCartao);
                        jaPegou = true;
                        StartCoroutine("DestruirObjeto");
                    }
                }
            }
        }
    }

    IEnumerator DestruirObjeto()
    {
        MeshRenderer renderer = GetComponentInChildren<MeshRenderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }
        if (somPegarCartao != null)
        {
            emissorDeSom.clip = somPegarCartao;
            emissorDeSom.PlayOneShot(emissorDeSom.clip);
        }
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
