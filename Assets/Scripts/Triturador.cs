using JetBrains.Annotations;
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
    public Animator trituradorAnim;
    public Animator chaveAnim;
    public GameObject chave;
    public AudioSource trituradorAudio;
    public AudioClip trituradorClip;

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
            
            }
        }
    }
    private IEnumerator PickupAnim()
    {
        PlayerMove.instance.enabled = false;
        playerAnim.SetTrigger("Pickup");
        trituradorAnim.SetTrigger("Triturador");
        trituradorAudio.PlayOneShot(trituradorClip);
        yield return new WaitForSeconds(trituradorAnim.GetCurrentAnimatorStateInfo(0).length +2f);
        playerAnim.ResetTrigger("Pickup");
        chaveAnim.SetTrigger("Jogar");
        yield return new WaitForSeconds(chaveAnim.GetCurrentAnimatorStateInfo(0).length + 2f);
        PlayerMove.instance.enabled = true;
        Destroy(chave);
        Instantiate(chavePrefab, new Vector3(200, 0, -53), Quaternion.identity);
        coletou = true;

    }
}
