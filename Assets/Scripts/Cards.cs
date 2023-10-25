using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cards : MonoBehaviour
{
    public List<int> CartoesDoJogador = new List<int>();
    void Awake()
    {
        if (transform.gameObject.tag != "Player")
        {
            transform.gameObject.tag = "Player";
        }
    }
}
