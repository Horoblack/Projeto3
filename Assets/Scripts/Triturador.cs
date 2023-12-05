using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triturador : MonoBehaviour
{
    [Range(1f, 200f)] public float distancia = 2;
    public GameObject playerGO;
    public bool coletou;
    public GameObject chavePrefab;
    private void Awake()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        coletou = false;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerGO.transform.position);
        if (distanceToPlayer < distancia)
        {
            if(Input.GetKeyDown(KeyCode.E) && !coletou)
            {
                Instantiate(chavePrefab, new Vector3(199.7f, 0, -54.33f), Quaternion.identity);
                coletou = true;

            }
        }
    }
  
}
