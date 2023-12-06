using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triturador : MonoBehaviour
{
    [Range(1f, 200f)] public float distancia = 2;
    public GameObject playerGO;
    public Animator playerAnim;
    public bool coletou;
    public GameObject chavePrefab;
    private void Awake()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        coletou = false;
        playerAnim = playerGO.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerGO.transform.position);
        if (distanceToPlayer < distancia)
        {
            if(Input.GetKeyDown(KeyCode.E) && !coletou)
            {
                StartCoroutine(PickupAnim());
                Instantiate(chavePrefab, new Vector3(199.7f, 0, -54.33f), Quaternion.identity);
                coletou = true;

            }
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
