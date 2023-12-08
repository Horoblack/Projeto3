using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{

    public Items item;
    public static ItemPickup InstancePickup;
    public GameObject playerGO;
    public Animator playerAnim;
    public bool Picked;
    [Range(1f,200f)] public float distancia = 20f;

    public void Awake()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        Picked = false;
        InstancePickup = this;
        playerAnim = playerGO.GetComponent<Animator>();
    }

    public void Pickup()
    {
        StartCoroutine(PickupAnim());
     
      
    }

    private IEnumerator PickupAnim()
    {
        PlayerMove.instance.enabled = false;
        playerAnim.SetTrigger("Pickup");
        yield return new WaitForSeconds(playerAnim.GetCurrentAnimatorStateInfo(0).length - 0.3f);
        playerAnim.ResetTrigger("Pickup");
        InventoryManager.Instance.Add(item);
        Destroy(gameObject);
        PlayerMove.instance.enabled = true;


    }

    private void Update()
    {

        float distanceToPlayer = Vector3.Distance(transform.position, playerGO.transform.position);
        if (distanceToPlayer < distancia)
        {
            if (Input.GetKeyDown(KeyCode.E) )
                Pickup();
        }
    }
}
