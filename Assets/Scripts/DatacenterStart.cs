using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatacenterStart : MonoBehaviour
{
  public Animator animator;
    public AudioSource puzzleAudio;
    public AudioClip puzzleClip;
  [Range(1f, 200f)] public float distancia = 20f;
  public GameObject playerGO;
    public int id;
    private void Awake()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        id = transform.GetSiblingIndex();
    }

           
    
    
    public void Action()
    {
        animator.enabled = true;
        animator.SetTrigger("pop");
        puzzleAudio.PlayOneShot(puzzleClip);

    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerGO.transform.position);
        if (distanceToPlayer < distancia)
        {
            if(Input.GetKeyDown(KeyCode.E)) {
                if (!BackPuzzle.simonIsSaying)
                {
                    Action();
                    BackPuzzle.Instance.PlayerAction(this);
                }
            }
        }
    }
}
