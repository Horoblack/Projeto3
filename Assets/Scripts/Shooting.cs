using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shooting : MonoBehaviour
{
    public static System.Action<bool> OnShoot;

    [Header("Status Arma")]
    public static Shooting instance;
    public float bulletSpd;
    public float bulletRate;
    public float fireTickCd;
    public float fireTickNow;

    [Header("Munição")]
    public static int ammo;
    public static int maxAmmo;
    public static int defaultAmmo;
    public Text ammoTxt;

    [Header("Objetos")]
    public Rigidbody bulletprefab;
    public GameObject playerr;
    public Animator playerAnim;
    public AudioSource audioSource;

    // Adicione uma referência ao script PlayerMove.
    public PlayerMove playerMove;

    private void Start()
    {
        instance = this;

        playerMove = FindObjectOfType<PlayerMove>();
    }

    void Shoot()
    {
        fireTickNow = Mathf.Clamp(fireTickNow, 1, fireTickCd);
        fireTickNow--;

        // Verifique se o jogador está vivo antes de permitir disparos.
        if (playerMove != null && !playerMove.IsDead && fireTickNow <= 0 && Input.GetButton("Fire1") && ammo > 0)
        {
            playerAnim.SetTrigger("Shoot");

            Rigidbody clone = Instantiate(bulletprefab, new Vector3(transform.position.x, 1f, transform.position.z), transform.rotation);
            clone.velocity = transform.forward * bulletSpd;

            OnShoot?.Invoke(true);

            fireTickNow = fireTickCd;
            ammo--;
        }
        else if (maxAmmo > 0 && Input.GetKey(KeyCode.R))
            Reload();
       
    }

    void Reload()
    {
        ammo = maxAmmo;
        ammo = Mathf.Clamp(ammo, 1, 10);
        maxAmmo -= ammo;
    }

    void BulletTextControl()
    {
        ammoTxt.text = ammo.ToString() + "/" + maxAmmo.ToString();
    }

    private void FixedUpdate()
    {
        BulletTextControl();
        Shoot();
    }
}
