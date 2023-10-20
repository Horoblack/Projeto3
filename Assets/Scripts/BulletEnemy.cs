using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{

    public int despawnTime = 0;
    public int damageBullet = 1;
    public bool isRed;


    void FixedUpdate()
    {

        if (despawnTime >= 100)
        {
            Destroy(this.gameObject);
        }
        despawnTime += 1;
    }

    private void OnTriggerEnter(Collider outro)
    {

        if (outro.CompareTag("Wall"))
            Destroy(gameObject);

       else if (outro.CompareTag("Player"))
        {
            outro.TryGetComponent<PlayerMove>(out PlayerMove player);
           player.TakeDamage(damageBullet);
            Destroy(gameObject);
        }
  



    }
}
