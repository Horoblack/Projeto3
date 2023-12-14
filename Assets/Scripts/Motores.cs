using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motores : MonoBehaviour
{
    [Range(1f, 200f)] public float distancia = 2;
    public GameObject playerGO;
    public Animator playerAnim;
    public AudioSource motorAudio;
  

    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        playerAnim = playerGO.GetComponent<Animator>();
    }

    void Update()
    {

        float distanceToPlayer = Vector3.Distance(transform.position, playerGO.transform.position);

        if (distanceToPlayer < distancia)
        {
            if (DoorCards.HasBucketFull && Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(PickupAnim());
                var balde = InventoryManager.Items.Find(x => x.name == "BaldeCheio");
                InventoryManager.Items.Remove(balde);
                DoorCards.FuelFull = true;
            }
        }
        else if (DoorCards.FuelFull)
            motorAudio.enabled = true;

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
