using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

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


    [Header("Vida")]
    public Text vidaTxt;
    public int PlayerHp;
    public int PlayerMaxHp;

    MeshRenderer mr;
    Color defaultColor;
    public float timeToColor;

    [Header("Dash")]
    public float DashCd;
    public float DashCdNow;
    public float Boom;

 
   


    private void Start()
    {
        instance = this;
        playerRb = GetComponent<Rigidbody>();
        PlayerHp = PlayerMaxHp;
         mr = GetComponent<MeshRenderer>();
         defaultColor = mr.material.color;
    }

    void mouseSpin()
    {   
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit; 

        if (Physics.Raycast(ray,out  hit, 100))
        {
            Lookpos = hit.point;
        }

        lookDir = Lookpos - transform.position;
        lookDir.y = 0; 
        transform.LookAt(transform.position + lookDir, Vector3.up);
     }

   
    void move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 moveLado = new Vector3 (h,0f,v);
        playerRb.AddForce(moveLado * spd, ForceMode.Force);

        if (Input.GetKeyDown(KeyCode.Space) && DashCdNow <= 0)
        {
            playerRb.velocity = Vector3.zero;
            playerRb.AddForce(moveLado * Boom, ForceMode.Impulse);
            DashCdNow = DashCd;
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
        PlayerHp -= damage;
        vidaTxt.text = $"Hp :{PlayerHp}";
        StartCoroutine(SwitchColors());
        if (PlayerHp <= 0)
        {
            Invoke(nameof(die), 0.1f);
           
        }


    }


    void die()
    {   
            Destroy(gameObject);    
    }

    void Update()
    {
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
       
        mr.material.color = Color.red;
        yield return new WaitForSeconds(timeToColor);
        mr.material.color = defaultColor;
      
    }
}
