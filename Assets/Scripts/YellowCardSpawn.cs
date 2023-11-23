using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowCardSpawn : MonoBehaviour
{
    [Range(1f, 200f)] public float distancia = 2;
    public GameObject playerGO;
    public GameObject yellowGO;

    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
    }
    IEnumerator yellowSpawn()
    {
        Instantiate(yellowGO, new Vector3(196.98f,1, -5.1f), Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
    }

    void Update()
    {

        float distanceToPlayer = Vector3.Distance(transform.position, playerGO.transform.position);

        if (distanceToPlayer < distancia)
        {
            if (DoorCards.EnergyRestored && Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(yellowSpawn());
            }

        
        }

    }
}
