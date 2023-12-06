
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPuzzle : MonoBehaviour
{
    public static BackPuzzle Instance;
    public DatacenterStart[] dataCenters;
    public Animator playerAnim;
    static int simonMax;
    public int DefaultMax;
    static float simonTime;
    public int prizeCount;
    public int DefaultPrizeCount;
    public bool done;
    public bool hasStarted;
    public GameObject computerGO;
    
    [Range(1f, 200f)] public float distancia = 2;
    public GameObject playerGO;

    static List<int> userList, simonList;

    public static bool simonIsSaying;

    private void Awake()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        playerAnim = playerGO.GetComponent<Animator>();

    }


    void Start()
    {
       
       
        Instance = this;
        hasStarted = false;

        DefaultMax = 2;
        DefaultPrizeCount = 0;
        simonMax = 2;
        simonTime = 0.5f;
       
    }

    private IEnumerator PickupAnim()
    {
        PlayerMove.instance.enabled = false;
        playerAnim.SetTrigger("Pickup");
        yield return new WaitForSeconds(playerAnim.GetCurrentAnimatorStateInfo(0).length);
        playerAnim.ResetTrigger("Pickup");
        PlayerMove.instance.enabled = true;
    }
    public  void PlayerAction(DatacenterStart b)
    {
        userList.Add(b.id);
        if (userList[userList.Count - 1] != simonList[userList.Count-1])
        {
           
            Debug.Log("Loose");
            hasStarted = false;

            userList.Clear();
            simonList.Clear();
            simonMax = DefaultMax;
            prizeCount = DefaultPrizeCount;
        }
        else if(userList.Count == simonList.Count)
        {
            if (hasStarted && !done)
            {
                Debug.Log("next level");
                StartCoroutine(SimonSays());
                prizeCount++;
            }
            else if (hasStarted && done)
                Debug.Log("parabéns!");
                
        }

    }
    IEnumerator SimonSays()
    {
        Debug.Log("prepare");
        yield return new WaitForSeconds(3);
        simonIsSaying = true;
        userList = new List<int>();
        simonList = new List<int>();

        for (int i = 0; i < simonMax; i++)
        {
            int rand = Random.Range(0, 4);
            simonList.Add(rand);
            dataCenters[rand].Action();

            yield return new WaitForSeconds(simonTime);
        }

        simonTime -= 0.015f;
        simonMax++;
        simonIsSaying = false;
    }
    IEnumerator despawns()
    {
        yield return new WaitForSeconds(2);
        computerGO.SetActive(true);
        gameObject.SetActive(false);
       
    }

  

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerGO.transform.position);
        if (distanceToPlayer < distancia)
        {
            if (Input.GetKeyDown(KeyCode.E) && !hasStarted)
            {             
                StartCoroutine(PickupAnim());
                StartCoroutine(SimonSays());
                hasStarted = true;
            }
        }

            if ( prizeCount >= 3)
        {
            done = true;
            
                StartCoroutine(despawns());
           
               
        }
    }
}
