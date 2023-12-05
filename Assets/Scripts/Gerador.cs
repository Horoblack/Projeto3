using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gerador : MonoBehaviour
{
    [Range(1f, 200f)] public float distancia = 2;
    public bool isOpen;
    public bool canBeOpen;
    public GameObject playerGO;
    public GameObject canvasGO;
    private float originalTimeScale;

    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        canvasGO.SetActive(false);
        originalTimeScale = Time.timeScale;
        canBeOpen = true;
    }

    void Update()
    {

        float distanceToPlayer = Vector3.Distance(transform.position, playerGO.transform.position);

        if(distanceToPlayer < distancia)
        {
            if (DoorCards.FuelFull && Input.GetKeyDown(KeyCode.E) && !isOpen && canBeOpen)
            {
                canvasGO.SetActive(true);
                isOpen = true;
                Time.timeScale = 0f;
                playerGO.GetComponent<PlayerMove>().enabled = false;
            }
            else if (DoorCards.FuelFull && Input.GetKeyDown(KeyCode.E) && isOpen && canBeOpen)
            {
                canvasGO.SetActive(false);
                isOpen = false;
                Time.timeScale = originalTimeScale;
                playerGO.GetComponent<PlayerMove>().enabled = true;
            }
            else if (DoorCards.EnergyRestored == true)
            {
                canvasGO.SetActive(false);
                canBeOpen = false;
                Time.timeScale = originalTimeScale;
                playerGO.GetComponent<PlayerMove>().enabled = true;
            }
        }

    }
}
