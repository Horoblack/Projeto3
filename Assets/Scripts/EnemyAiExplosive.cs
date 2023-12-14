using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAiExplosive : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;
    public NavMeshAgent agent;
    public LayerMask isGround, isPlayer;
    public bool blueCardDrop;
    public GameObject blueCard;
    public GameObject particula;
    public AudioSource alienAudio;
    public AudioClip alienAudioClip;
    public bool sematar = false;

    [Header("Patrulhar")]
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    [Header("Atacar")]
    public float radius;
    public int exploDamage;
    public LayerMask playerr;


    [Header("Estados")]
    public float sightRange, atkRange;
    public bool playerInRangeAtk, playerInRangeSight;
    public Animator alienAnim;

    [Header("Vida")]
    public int EnemyHealth;
    public int EnemyMaxHealth;

    MeshRenderer mr;
    Color defaultColor;
    public float timeToColor = 0.1f;

    private void Awake()
    {
        alienAnim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        EnemyHealth = EnemyMaxHealth;
        mr = GetComponent<MeshRenderer>();
        defaultColor = mr.material.color;
    }



    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, isGround))
            walkPointSet = true;
    }
    public void Explode()
    {
       

        Collider[] collider = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider atingidos in collider)
        {
            if (atingidos.CompareTag("Player"))
            {
                if (atingidos.TryGetComponent<PlayerMove>(out PlayerMove _playermove) && !sematar)
                {
                    sematar = true;
                    alienAnim.SetTrigger("Explosion");
                    
                    _playermove?.TakeDamage(exploDamage);

                    StartCoroutine(SeMata());      
                }
            }
        
        }
        
    }
    public IEnumerator SeMata()
    {
      
        alienAnim.SetTrigger("Die");
        alienAudio.PlayOneShot(alienAudioClip);
        yield return new WaitForSeconds(alienAudioClip.length -0.6f);
        Instantiate(particula, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.01f);
        Destroy(gameObject);
    }


    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);
      
    }


    private void ChasePlayer()
    {
        transform.LookAt(player);
        agent.SetDestination(player.position);
        alienAnim.SetBool("isWalking", true);
    }

    public void TakeDamage(int damage)
    {
        EnemyHealth -= damage;
        StartCoroutine(SwitchColors());
        if (EnemyHealth <= 0) 
            StartCoroutine(DieAndDestroy());

    }

    IEnumerator SwitchColors()
    {

        mr.material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        mr.material.color = defaultColor;

    }

    private IEnumerator DieAndDestroy()
    {
        // Ative a anima��o de morte
        sightRange = 0;
        alienAnim.SetTrigger("Die");
        yield return new WaitForSeconds(alienAnim.GetCurrentAnimatorStateInfo(0).length);
        alienAudio.PlayOneShot(alienAudioClip);
        yield return new WaitForSeconds(0.44f);


        // Realize as a��es ap�s a anima��o de morte
        DestroyEnemy();
    }
    private void DestroyEnemy()
    {
        Instantiate(particula, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void DontChase()
    {
        alienAnim.SetBool("isWalking", false);
    }

    private void FixedUpdate()
    {
        playerInRangeSight = Physics.CheckSphere(transform.position, sightRange, isPlayer);
        playerInRangeAtk = Physics.CheckSphere(transform.position, atkRange, isPlayer);

        if (playerInRangeSight && !playerInRangeAtk) 
            ChasePlayer();
        else 
            DontChase();
     
        if (playerInRangeSight && playerInRangeAtk) {
            Explode();
                 
        }
      
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}