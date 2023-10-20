using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private Animator anim;
    public GameObject bladePoint1;
    public GameObject bladePoint2;
    public float radius;
    public LayerMask enemies;

    public int damage;

    private void Start()
    {
        anim = GetComponent<Animator>();

    }

    public void BeginAtk1()
    {
        Collider[] enemy = Physics.OverlapSphere(bladePoint2.transform.position, radius, enemies);



        foreach (Collider enemyGameObj in enemy)
        {

            if (enemyGameObj.CompareTag("BulletRed"))
                Destroy(enemyGameObj.gameObject);
            
            else
                enemyGameObj.GetComponent<EnemyAi>().TakeDamage(damage);

        }

    }
    public void BeginAtk2()
    {

        Collider[] enemy = Physics.OverlapSphere(bladePoint2.transform.position, radius, enemies);



        foreach (Collider enemyGameObj in enemy)
        {

            if (enemyGameObj.CompareTag("BulletBlue")) 
                Destroy(enemyGameObj.gameObject);
            
            else
                enemyGameObj.GetComponent<EnemyAi>().TakeDamage(damage);
       
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            anim.SetBool("isAtkLeft", true);

        if (Input.GetMouseButtonDown(1))
        
            anim.SetBool("isAtkRight", true);
         
        
    }

    public void endAtk()
    {
        anim.SetBool("isAtkLeft", false);
        anim.SetBool("isAtkRight", false);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(bladePoint1.transform.position, radius);
        Gizmos.DrawWireSphere(bladePoint2.transform.position, radius);
    }
}
