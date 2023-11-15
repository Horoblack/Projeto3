
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPuzzle : MonoBehaviour
{
    public static BackPuzzle Instance;
    public DatacenterStart[] dataCenters;
    static int simonMax;
    static float simonTime;
    public int prizeCount;
    public bool done;
    public bool hasStarted;
    public GameObject cardPrefab;
    public GameObject computerGO;
    public bool cardPrefabSpawn;
    
    [Range(1f, 200f)] public float distancia = 2;
    public GameObject playerGO;

    static List<int> userList, simonList;

    public static bool simonIsSaying;

    private void Awake()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        
    }

  
    void Start()
    {
        computerGO = GameObject.FindGameObjectWithTag("Computer");
        computerGO.SetActive(false);
        cardPrefabSpawn = true;
        Instance = this;
        hasStarted = false;

        simonMax = 2;
        simonTime = 0.5f;
       
    }
    public  void PlayerAction(DatacenterStart b)
    {
        userList.Add(b.id);
        if (userList[userList.Count - 1] != simonList[userList.Count-1])
        {
           
            Debug.Log("Loose");
            hasStarted = false;

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

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerGO.transform.position);
        if (distanceToPlayer < distancia)
        {
            if (Input.GetKeyDown(KeyCode.E) && !hasStarted)
            {             
                StartCoroutine(SimonSays());
                hasStarted = true;
            }
        }

            if ( prizeCount >= 2)
        {
            done = true;
            if(cardPrefabSpawn)
            Instantiate(cardPrefab, new Vector3(263.05f, 0.5f, -2.7f), Quaternion.identity);
            cardPrefabSpawn = false;
            computerGO.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
