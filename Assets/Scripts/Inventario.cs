using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
public class Inventario : MonoBehaviour
{
    public GameObject inv;
    public bool isOpen;
    public GameObject manager;
    public GameObject playerGo;
    private float originalTimeScale;

    private void Start()
    {
        originalTimeScale = Time.timeScale;
        playerGo = GameObject.FindGameObjectWithTag("Player");
    }

    public void ButtonToggle()
    {
        isOpen = false;
    }

    public void AbreFecha()
    {


        if (!isOpen && Input.GetKeyDown(KeyCode.I))
        {
            PlayerMove.instance.runAudio.Stop();
            inv.SetActive(true);
            InventoryManager _inventoryManager = manager.GetComponent<InventoryManager>();
            _inventoryManager.ListItems();
          
            isOpen = true;
            Time.timeScale = 0f;
            playerGo.GetComponent<PlayerMove>().enabled = false;
         

        }
        else if (isOpen && Input.GetKeyDown(KeyCode.I))
        {
       
            inv.SetActive(false);
            isOpen = false;
            Time.timeScale = originalTimeScale;
            playerGo.GetComponent<PlayerMove>().enabled = true;
            InventoryManager _inventoryManager = manager.GetComponent<InventoryManager>();
            _inventoryManager.Clean();
        }
    }

private void Update()
    {
        AbreFecha();
    }
}
