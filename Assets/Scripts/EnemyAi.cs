using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;
    public NavMeshAgent agent;
    public LayerMask isGround, isPlayer;
    public bool blueCardDrop;
    public GameObject blueCard;

    [Header("Patrulhar")]
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    [Header("Atacar")]
    public float timeBetweenAtk;
    public bool atacou;
    public GameObject enemybullprefab;
    public GameObject[] bulletHolds = new GameObject[5];
    public LayerMask playerr;


    [Header("Estados")]
    public float sightRange, atkRange;
    public bool playerInRangeAtk, playerInRangeSight;

    [Header("Vida")]
    public int EnemyHealth;
    public int EnemyMaxHealth;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        EnemyHealth = EnemyMaxHealth;
    }

   

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);    

        if(Physics.Raycast(walkPoint, - transform.up, 2f , isGround))
            walkPointSet = true;    
    }
    IEnumerator Attack()
    {
        atacou = true;
        timeBetweenAtk = Random.Range(0, 5);
        
            Rigidbody rb = Instantiate(enemybullprefab, transform.position, transform.rotation).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
          
        

        yield return new WaitForSeconds(timeBetweenAtk);
        atacou = false;

    }

        private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if(!atacou)
        {
                StartCoroutine(Attack());
                
        }
       
        
    }


    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    public void TakeDamage(int damage)
    {
        EnemyHealth -= damage;
        if (EnemyHealth <= 0) Invoke(nameof(DestroyEnemy), 0f);
        
    }

    private void DestroyEnemy()
    {
        if (blueCardDrop)
            Instantiate(blueCard, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Destroy(gameObject);
    }

    private void Update()
    {
        playerInRangeSight = Physics.CheckSphere(transform.position, sightRange, isPlayer);
        playerInRangeAtk = Physics.CheckSphere(transform.position, atkRange, isPlayer);

        if(playerInRangeSight && !playerInRangeAtk) ChasePlayer();
        if(playerInRangeSight && playerInRangeAtk) AttackPlayer();

       
    }
}
