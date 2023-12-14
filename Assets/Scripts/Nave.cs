using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using UnityEngine;

public class Nave : MonoBehaviour
{
    [Range(1f, 200f)] public float distancia = 2;
    public GameObject playerGO;
    public Animator playerAnim;
    public GameObject baldeGO;
    public int entregues;
    public  bool BucketUsed;
    public  bool WrenchUsed;
    public string _index;
    private bool coletou;

    private void Awake()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        BucketUsed = false;
        coletou = false;
        playerAnim = playerGO.GetComponent<Animator>();


    }

    IEnumerator GasolinaSpawn()
    {
        Instantiate(baldeGO, new Vector3(186.49f, 0.5f, -27.65f), Quaternion.identity);
        coletou = true;
        yield return new WaitForSeconds(2);
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerGO.transform.position);
        switch (entregues)
        {
            case 0:
                
                if (distanceToPlayer < distancia)
                {
                    if (DoorCards.HasBucket && Input.GetKeyDown(KeyCode.E))
                    {
                        StartCoroutine(PickupAnim());
                        var balde = InventoryManager.Items.Find(x => x.name == "Balde");
                        InventoryManager.Items.Remove(balde);
                        if (!BucketUsed)
                        {
                            entregues++;
                            BucketUsed = true;
                        }

                    }
                }
                break;
            case 1:
                
                if (distanceToPlayer < distancia)
                {
                    if (DoorCards.HasWrench && Input.GetKeyDown(KeyCode.E))
                    {
                        StartCoroutine (PickupAnim());
                        Debug.Log(DoorCards.HasWrench.ToString());
                        var chave = InventoryManager.Items.Find(x => x.name == "ChaveDeFenda");
                        InventoryManager.Items.Remove(chave);
                        if (!WrenchUsed)
                        {
                            entregues++;
                            WrenchUsed = true;
                        }

                    }


                }
                break;

                case 2:
                if(!coletou)
                 StartCoroutine(GasolinaSpawn());
                break;
        }
    }

    private IEnumerator PickupAnim()
    {
        PlayerMove.instance.enabled = false;
        playerAnim.SetTrigger("Pickup");
        yield return new WaitForSeconds(playerAnim.GetCurrentAnimatorStateInfo(0).length);
        playerAnim.ResetTrigger("Pickup");
        PlayerMove.instance.enabled = true;
    }
}
