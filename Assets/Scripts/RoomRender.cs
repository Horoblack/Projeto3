using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomRender : MonoBehaviour
{

    public GameObject fogGO;

    private void Start()
    {
        fogGO = gameObject.transform.GetChild(0).gameObject;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
           
                fogGO.gameObject.SetActive(false);
            
        }
    }

    private void OnTriggerExit(Collider otherd)
    {


                fogGO.gameObject.SetActive(true);
         
        
    }

 }
