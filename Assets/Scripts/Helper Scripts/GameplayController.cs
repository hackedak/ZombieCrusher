using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;
    public GameObject[] obstaclePrefabs;
    public GameObject[] zombiePrefabs;
    public Transform[] lanes;
    public float  min_ObstacleDelay = 10f, max_ObstacleDelay = 40f;
    private float halfGroundSize;
    private BaseController playerController;
    void Awake() {
        MakeInstance();    
    }
    void Start()
    {
        halfGroundSize = GameObject.Find("GroundBlock Main").GetComponent<GroundBlock>().halfLength;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseController>();
        StartCoroutine("GenerateObstacles");
    }  

    void MakeInstance(){
        if (instance == null)
        {
            instance = this;
        }else if(instance !=null){       //check without if condition
            Destroy(gameObject);
        }
    }

    IEnumerator GenerateObstacles(){
        float timer = Random.Range(min_ObstacleDelay, max_ObstacleDelay) /playerController.speed.z;
        yield return new WaitForSeconds(timer);
        CreateObstacles(playerController.gameObject.transform.position.z + halfGroundSize);
        StartCoroutine("GenerateObstacles");
    }

    void CreateObstacles(float zPos){
        int r = Random.Range(0, 10);

        if (0 <= r && r<7)
        {
            int obstacleLane = Random.Range(0, lanes.Length);


            AddObstacles(new Vector3(lanes[obstacleLane].transform.position.x, 0f, zPos), Random.Range(0, obstaclePrefabs.Length));
            int zombieLane = 0;

            if (obstacleLane == 0)
            {
                zombieLane = Random.Range(0, 2) == 1? 1:2;

            }else if (obstacleLane == 1)
            {
                zombieLane = Random.Range(0, 2) == 1? 0:2;
            
            }else if (obstacleLane == 2)
            {
                zombieLane = Random.Range(0, 2) == 1? 1:0;
            }
            AddZombies(new Vector3(lanes[zombieLane].transform.position.x, 0.15f, zPos));
        }

        
    }

    void AddObstacles(Vector3 position, int type){
        GameObject obstacle = Instantiate(obstaclePrefabs[type], position, Quaternion.identity);
        bool mirror = Random.Range(0, 2) == 1;

        switch(type){
            case 0:
                obstacle.transform.rotation = Quaternion.Euler(0f, mirror ? -20:20, 0f); 
                break;
            case 1:
                obstacle.transform.rotation = Quaternion.Euler(0f, mirror ? -20:20, 0f); 
                break;
            case 2:
                obstacle.transform.rotation = Quaternion.Euler(0f, mirror ? -1:1, 0f); 
                break;
            case 3:
                obstacle.transform.rotation = Quaternion.Euler(0f, mirror ? -170:170, 0f); 
                break;
        }

        obstacle.transform.position = position;
    }

    void AddZombies(Vector3 pos){
        int count = Random.Range(0, 3) + 1;

        for(int i = 0; i < count; i++){
            Vector3 shift = new Vector3(Random.Range(-0.5f, 0.5f), 0f, Random.Range(1f, 10f) * i);
            Instantiate(zombiePrefabs[Random.Range(0, zombiePrefabs.Length)], pos + shift * i, Quaternion.identity);
        }
    }
} //class
