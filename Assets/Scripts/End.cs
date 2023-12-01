using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class End : MonoBehaviour
{
    [Range(1f, 200f)] public float distancia = 5;
    public GameObject playerGO;
    public Animator _anim;
    public Image fadeImage;
    [SerializeField] private CanvasGroup canvasGroup;



    private void Awake()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
       
       
    }
    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerGO.transform.position);
        if (distanceToPlayer < distancia && Input.GetKeyDown(KeyCode.E))
        {
           
                _anim.SetTrigger("fade");
                 StartCoroutine(Trocar());

        }
   

    }
   
   IEnumerator Trocar()
   {
        yield return new WaitForSeconds(_anim.GetCurrentAnimatorStateInfo(0).normalizedTime - 0.08f) ;
        SceneManager.LoadScene("End");
   }
     
       
    

}
