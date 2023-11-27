using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove instance; 
    public Text CdTxt;

    [Header("Movimentação")]
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

    [Header("Dash")]
    public float dashDuration;
    public float DashCd;
    public float DashCdNow;
    public float Boom;
    public bool isDashing;
    Vector3 moveLado;
    private Shader originalShader;
    public float dashDistance;

    private void Start()
    {
        instance = this;
        playerRb = GetComponent<Rigidbody>();
        PlayerHp = PlayerMaxHp;
        mr = GetComponent<MeshRenderer>();
        defaultColor = mr.material.color;
        playerAnim = GetComponent<Animator>();
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

   
    void move()
    {
        if (!isDead) 
        {
          
            float movimentoHorizontal = Input.GetAxisRaw("Horizontal");
            float movimentoVertical = Input.GetAxisRaw("Vertical");
            moveLado = new Vector3(movimentoHorizontal *-1 , 0, movimentoVertical *-1 ).normalized;

            // Aplica a força apenas se o jogador estiver vivo
           transform.Translate(moveLado * spd * Time.deltaTime, Space.World);

            if (Input.GetKeyDown(KeyCode.Space) && DashCdNow <= 0)
                StartCoroutine(Dash());

            if (moveLado.x != 0 || moveLado.y != 0 || moveLado.z != 0)
            {
                playerAnim.SetFloat("WalkSpeed", 1);
            }
            else
                playerAnim.SetFloat("WalkSpeed", 0);
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
        vidaTxt.text = $"Hp :{PlayerHp}";

    }



    public void TakeDamage(int damage)
    {
        if (!isDead) // Adiciona essa verificação
        {
            PlayerHp -= damage;
            vidaTxt.text = $"Hp :{PlayerHp}";

            // Inicia a animação de levar dano
            playerAnim.SetTrigger("TakeDamage");

            // Aguarda o término da animação de levar dano antes de iniciar a Coroutine para piscar em vermelho
            StartCoroutine(WaitForDamageAnimation());

            if (PlayerHp <= 0)
                Invoke(nameof(die), 0.1f);
        }
    }

    IEnumerator WaitForDamageAnimation()
    {
      // Aguarda até que a animação de levar dano tenha terminado
      yield return new WaitForSeconds(playerAnim.GetCurrentAnimatorStateInfo(0).length);

      // Inicia a Coroutine para piscar em vermelho
      StartCoroutine(SwitchColors());
    }


    void die()
    {
        // Define a flag de morte como true
        isDead = true;

        // Inicia a animação de morte
        playerAnim.SetTrigger("Die");

        InventoryManager.Items.Clear();
        Shooting.ammo = Shooting.defaultAmmo;
        Shooting.maxAmmo = Shooting.defaultAmmo;

        // Obtém a duração da animação de morte
        float deathAnimationDuration = playerAnim.GetCurrentAnimatorStateInfo(0).length;

        // Calcula o tempo total antes de reiniciar (tempo da animação + tempo de espera)
        float totalTimeBeforeRestart = deathAnimationDuration + delayBeforeRestart;

        // Inicia a coroutine para reiniciar a cena após o término da animação e o tempo de espera
        StartCoroutine(RestartSceneAfterDelay(totalTimeBeforeRestart));
    }

    IEnumerator RestartSceneAfterDelay(float delay)
    {
        // Aguarda o término da animação de morte e o tempo de espera
        yield return new WaitForSeconds(delay);

        // Reinicia a cena
        RestartScene();
    }

    void RestartScene()
    {
        // Obtém o índice da cena atual
       // int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Reinicia a cena atual
        SceneManager.LoadScene("Spaceship Small");
    }

    void Update()
    {
        if(isDashing)
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

        // Aguarda um curto período de tempo
        yield return new WaitForSeconds(timeToColor);

        // Restaura a lista de materiais original
        mr.materials = originalMaterials;

    }

    IEnumerator Dash()
    {
        playerAnim.SetTrigger("Dash");
        isDashing = true;
        RaycastHit hit;
        yield return new WaitForSeconds(0.05f);
        if (Physics.Raycast(transform.position, moveLado, out hit, dashDistance))
        {
            // Se o Raycast atingir algo, ajuste a posição final do dash
            transform.position = hit.point;
        }
        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            playerRb.velocity = new Vector3(moveLado.x * Boom, 0f, moveLado.z * Boom);
            yield return null;
            Vector3 moveDirection = new Vector3(playerRb.velocity.x, 0f, playerRb.velocity.z);

         
                if (moveDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime *2000);
            }
        }
            DashCdNow = DashCd;
        yield return new WaitForSeconds(0.4f);
        isDashing = false;
        
    }
}
