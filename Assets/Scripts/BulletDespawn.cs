using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDespawn : MonoBehaviour
{

    public int despawnTime = 0;
    public int damageBullet;

    public bool isBlue;
    public bool isRed;
  


    void FixedUpdate()
    {
      
        if (despawnTime >= 100)
        {
            Destroy(this.gameObject);
        }
        despawnTime += 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Object"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }

        else if (other.CompareTag("Wall"))
            Destroy(gameObject);

        else if (other.CompareTag("Enemy"))
        {
            other.TryGetComponent<EnemyAiExplosive>(out EnemyAiExplosive enemy);
            enemy.TakeDamage(damageBullet);
            Destroy(gameObject);
           
        }
    }

}
