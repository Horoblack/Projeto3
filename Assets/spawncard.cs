using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawncard : MonoBehaviour
{
    public GameObject cardPrefab;
    void Start()
    {
        Instantiate(cardPrefab, new Vector3(263.05f, 0.5f, -2.7f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
