using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove instance;
    public Text CdTxt;

    [Header("Movimenta��o")]
    public Rigidbody playerRb;
    [SerializeField] public Vector3 Lookpos;
    private Vector3 lookDir;
    public float spd;
    public float rotationSpd;
    private Animator playerAnim;


    [Header("Vida")]
    public Text vidaTxt;
    public int PlayerHp;
    public int PlayerMaxHp;

    MeshRenderer mr;
    Color defaultColor;
    public float timeToColor;
    private bool isDead = false;
    public float delayBeforeRestart = 4.0f;
    private bool isDashAnimationPlaying = false;

    public bool IsDashing
    {
        get { return isDashing; }
    }

    [Header("Dash")]
    public float dashDuration;
    public float DashCd;
    public float DashCdNow;
    public float Boom;
    public bool isDashing;
    Vector3 moveLado;
    private Shader originalShader;

    [Header("Sound")]
    public AudioSource playerAudio;
    public AudioSource runAudio;
    public AudioClip dash;
    public AudioClip reload;
    public AudioClip shoot;



    private void Awake()
    {
        spd = 20;
        instance = this;
        playerRb = GetComponent<Rigidbody>();
        PlayerHp = PlayerMaxHp;
        mr = GetComponent<MeshRenderer>();
        defaultColor = mr.material.color;
        playerAnim = GetComponent<Animator>();
        InventoryManager.Items.Clear();
        Shooting.ammo = Shooting.defaultAmmo;
        Shooting.maxAmmo = Shooting.defaultAmmo;
    }

    void mouseSpin()
    {
        if (!isDead && !isDashing)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
                Lookpos = hit.point;


            lookDir = Lookpos - transform.position;
            lookDir.y = 0;
            transform.LookAt(transform.position + lookDir, Vector3.up);
        }
    }

    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }

    void move()
    {

        if (!isDead)
        {

            float movimentoHorizontal = Input.GetAxisRaw("Horizontal");
            float movimentoVertical = Input.GetAxisRaw("Vertical");
            moveLado = new Vector3(movimentoHorizontal * -1, 0, movimentoVertical * -1).normalized;

            // Aplica a for�a apenas se o jogador estiver vivo
            playerRb.AddForce(moveLado *spd,ForceMode.Force);
            
            if (Input.GetKeyDown(KeyCode.Space) && DashCdNow <= 0)
                StartCoroutine(Dash());

            if (moveLado.x != 0 || moveLado.y != 0 || moveLado.z != 0)
            {
                playerAnim.SetFloat("WalkSpeed", 1);
               runAudio.enabled = true; 

            }
            else
            {
                playerAnim.SetFloat("WalkSpeed", 0);
                runAudio.enabled = false;
            }
        }


    }

    void ChangeTxt()
    {
        CdTxt.text = DashCdNow.ToString();
        if (DashCdNow <= 0)
            CdTxt.text = "PRONTO";

    }

    void counting()
    {
        DashCdNow--;
        if (DashCdNow <= 0)
            DashCdNow = 0;
    }

    public void IncreaseHealth(int value)
    {
        PlayerHp += value;
        vidaTxt.text = PlayerHp.ToString();

    }



    public void TakeDamage(int damage)
    {
        if (!isDead) // Adiciona essa verifica��o
        {
            PlayerHp -= damage;
            vidaTxt.text = $"{PlayerHp}";

            // Inicia a anima��o de levar dano
            playerAnim.SetTrigger("TakeDamage");

            // Aguarda o t�rmino da anima��o de levar dano antes de iniciar a Coroutine para piscar em vermelho
            StartCoroutine(WaitForDamageAnimation());

            if (PlayerHp <= 0)
                Invoke(nameof(die), 0.1f);
        }
    }

    IEnumerator WaitForDamageAnimation()
    {
        // Aguarda at� que a anima��o de levar dano tenha terminado
        yield return new WaitForSeconds(playerAnim.GetCurrentAnimatorStateInfo(0).length);      
    }


    void die()
    {
        // Define a flag de morte como true
        isDead = true;

        // Inicia a anima��o de morte
        playerAnim.SetTrigger("Die");


        // Obt�m a dura��o da anima��o de morte
        float deathAnimationDuration = playerAnim.GetCurrentAnimatorStateInfo(0).length;

        // Calcula o tempo total antes de reiniciar (tempo da anima��o + tempo de espera)
        float totalTimeBeforeRestart = deathAnimationDuration + delayBeforeRestart;

        // Inicia a coroutine para reiniciar a cena ap�s o t�rmino da anima��o e o tempo de espera
        StartCoroutine(RestartSceneAfterDelay(totalTimeBeforeRestart));
    }

    IEnumerator RestartSceneAfterDelay(float delay)
    {
        // Aguarda o t�rmino da anima��o de morte e o tempo de espera
        yield return new WaitForSeconds(delay);

        // Reinicia a cena
        RestartScene();
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



    void Update()
    {
        if (isDashing)
        {
            return;
        }

        move();
        mouseSpin();
        ChangeTxt();

    }

    private void FixedUpdate()
    {
        counting();
    }

    IEnumerator Dash()
    {
        playerAudio.PlayOneShot(dash);
        playerAnim.SetTrigger("Dash");
        isDashing = true;
        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            playerRb.velocity = new Vector3(moveLado.x * Boom, 0f, moveLado.z * Boom);
            yield return null;
            Vector3 moveDirection = new Vector3(playerRb.velocity.x, 0f, playerRb.velocity.z);

            if (moveDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * 2000);

            }
        }

        DashCdNow = DashCd;

        // Notifica que o dash terminou
   

        yield return new WaitForSeconds(0.4f);
        isDashing = false;
    }

   
}
