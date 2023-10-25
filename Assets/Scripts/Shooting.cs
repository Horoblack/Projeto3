using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shooting : MonoBehaviour
{
    [Header("Status Arma")]
    public static Shooting instance;
    public float bulletSpd;
    public float bulletRate;
    public float fireTickCd;
    public float fireTickNow;

    [Header("Munição")]
    public int ammo;
    public int maxAmmo;
    public Text ammoTxt;

    [Header("Objetos")]
    public Rigidbody bulletprefab;
    public GameObject playerr;
    public Animator playerAnim;

    private void Start()
    {
        instance = this;
    }
    void Shoot()
    {
      
       fireTickNow = Mathf.Clamp(fireTickNow, 1, fireTickCd);
       fireTickNow--;
        if (fireTickNow <= 0 && Input.GetButton("Fire1") && ammo > 0)
        {
            playerAnim.SetTrigger("Shoot");
            Rigidbody clone = Instantiate(bulletprefab, new Vector3(transform.position.x, 1f, transform.position.z), transform.rotation);
            clone.velocity = transform.forward * bulletSpd;
            fireTickNow = fireTickCd;
            ammo--;
        }
        else if (maxAmmo > 0 && Input.GetKey(KeyCode.R))
            Reload();

        else if (ammo == 0 && maxAmmo == 0)
            Debug.Log("Sem munição!");
    }
   
    //Munição
    void Reload()
    {
        ammo = maxAmmo;
        ammo = Mathf.Clamp(ammo, 1, 10);
        maxAmmo -= ammo;

    }

    void BulletTextControl()
    {
        ammoTxt.text = ammo.ToString() +"/" + maxAmmo.ToString();
    }


    private void FixedUpdate()
    {
      BulletTextControl();
        //disparar
        Shoot();
    }
}
