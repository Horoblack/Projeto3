using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float x;
    public float z;
    public float spawnTime;
 

    private void OnTriggerEnter(Collider other)
    {
        


        if (other.CompareTag("Player") && DoorCards.HasredCard )
        {
            for (int i = 0; i < 10; i++)
            {
                StartCoroutine(Spawns());          
            }
            gameObject.SetActive(false);
        }

     
    }

    IEnumerator Spawns()
    {
       
        x = Random.Range(282, 260);
        z = Random.Range(-59, -42);
        Instantiate(enemyPrefab, new Vector3(x, 1, z), Quaternion.identity);
        yield return new WaitForSeconds(1);

    }
}
