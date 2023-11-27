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
    public float velProjetil;
    
    

    [Header("Estados")]
    public float sightRange, atkRange;
    public bool playerInRangeAtk, playerInRangeSight;

    [Header("Vida")]
    public int EnemyHealth;
    public int EnemyMaxHealth;

    MeshRenderer mr;
    public Animator anim;
    private float timeToColor;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        EnemyHealth = EnemyMaxHealth;
        anim = GetComponent<Animator>();
        mr = GetComponent<MeshRenderer>();
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
        anim.SetTrigger("Attack");
        atacou = true;
        timeBetweenAtk = Random.Range(0, 5);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length -0.5f);

        Rigidbody rb = Instantiate(enemybullprefab, transform.position, transform.rotation).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * velProjetil, ForceMode.Impulse);



        yield return new WaitForSeconds(timeBetweenAtk);
        atacou = false;
        anim.ResetTrigger("Attack");

    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!atacou && EnemyHealth > 0) 
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
        if (EnemyHealth <= 0)
        {
            StartCoroutine(DieAndDestroy());
        }
    }

    private IEnumerator DieAndDestroy()
    {
        sightRange = 0;
        atkRange = 0;
        StartCoroutine(SwitchColors());

        // Ativa a anima��o de morte
        anim.SetTrigger("Die");

        // Aguarda o t�rmino da anima��o de morte
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length -0.2f);
        
        
        DestroyEnemy();
    }

    IEnumerator SwitchColors()
    {
        // Armazena a lista de materiais original
        Material[] originalMaterials = mr.materials;

        // Cria uma nova lista de materiais com o material vermelho
        Material[] redMaterials = new Material[originalMaterials.Length];
        for (int i = 0; i < originalMaterials.Length; i++)
        {
            redMaterials[i] = new Material(originalMaterials[i]);
            redMaterials[i].color = Color.red;
        }

        // Aplica a nova lista de materiais temporariamente
        mr.materials = redMaterials;

        // Aguarda um curto per�odo de tempo
        yield return new WaitForSeconds(timeToColor);

        // Restaura a lista de materiais original
        mr.materials = originalMaterials;

    }

    public void ProcessarDano(int dano)
    {
        // L�gica de processamento de dano aqui (pode incluir efeitos visuais, som, etc.)
        TakeDamage(dano);
    }

    private void DestroyEnemy()
    {
        if (blueCardDrop)
            Instantiate(blueCard, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Destroy(gameObject);
    }

    private void Update()
    {
        if (EnemyHealth > 0) // Adicionando verifica��o para garantir que o inimigo est� vivo
        {
            playerInRangeSight = Physics.CheckSphere(transform.position, sightRange, isPlayer);
            playerInRangeAtk = Physics.CheckSphere(transform.position, atkRange, isPlayer);

            if (playerInRangeSight && !playerInRangeAtk) ChasePlayer();
            if (playerInRangeSight && playerInRangeAtk) AttackPlayer();
        }
    }
}
