using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{

    int lastPlatformY;
    int lastBackgroundY;
    int lastEnemyY;
    private Transform player;

    List<GameObject> platforms = new List<GameObject>();
    List<GameObject> coolStuff = new List<GameObject>();
    List<GameObject> backGround = new List<GameObject>();
    List<GameObject> enemies = new List<GameObject>();

    int rightPositon = 5;
    int leftPositon = -4;

    List<GameObject> spawnedObjects = new List<GameObject>();


    private void Start()
    {

        lastPlatformY = 0;

        player = GameObject.FindGameObjectWithTag("Player").transform;

        platforms.Add(Resources.Load("Platforms/Platform") as GameObject);
        platforms.Add(Resources.Load("Platforms/PlatformMoving") as GameObject);
        platforms.Add(Resources.Load("Platforms/Platform1Time") as GameObject);


        coolStuff.Add(Resources.Load("Cool Stuff/PortalBlue") as GameObject);
        coolStuff.Add(Resources.Load("Cool Stuff/Up") as GameObject);
        coolStuff.Add(Resources.Load("Cool Stuff/JetpackObj") as GameObject);
        coolStuff.Add(Resources.Load("Cool Stuff/SpringObj") as GameObject);
        

        backGround.Add(Resources.Load("Background/Cloud1") as GameObject);
        backGround.Add(Resources.Load("Background/Cloud2") as GameObject);
        backGround.Add(Resources.Load("Background/Cloud3") as GameObject);
        backGround.Add(Resources.Load("Background/Cloud4") as GameObject);

        enemies.Add(Resources.Load("Enemies/Enemy1") as GameObject);

        InvokeRepeating("EnemySpawn", 0f, 1f);

    }


    void CoolStuffSpawn(GameObject platform)
    {
        if ( Random.Range ( 0, 10 ) == 2 ) // 10% chance
        {
            GameObject thing = Instantiate(coolStuff[Random.Range(0, coolStuff.Count)]);
            thing.transform.parent = platform.transform.GetChild(0);
            thing.transform.position = new Vector3(platform.transform.position.x, platform.transform.position.y + 0.4f);

            if (thing.tag == "Spring" || thing.tag == "Batoot") platform.transform.GetChild(0).GetChild(0).tag = thing.tag;
        }
    }

    void BackgroundSpawn()
    {
        if (Mathf.Abs(player.transform.position.y - lastBackgroundY) < 16f)
        {
            GameObject backgroundObj = Instantiate(backGround[Random.Range(0, backGround.Count)]);
            backgroundObj.transform.rotation = Quaternion.Euler(new Vector3(0f, Random.Range(0, 360)));
            backgroundObj.transform.parent = this.transform;
            backgroundObj.transform.position = new Vector3(Random.Range(leftPositon, rightPositon), lastBackgroundY + Random.Range(3, 7) * 3,7);
           
            lastBackgroundY = (int)backgroundObj.transform.position.y;

            spawnedObjects.Add(backgroundObj);
        }
    }



    private void FixedUpdate()
    {

        if (Mathf.Abs(player.transform.position.y - lastPlatformY) < 15f )
        {
            GameObject platform = Instantiate(platforms[Random.Range(0, platforms.Count)]);
            platform.transform.parent = this.transform;
            platform.transform.position = new Vector3(Random.Range(leftPositon + 1, rightPositon -1), lastPlatformY + Random.Range(3,6)); 

            spawnedObjects.Add(platform);

            if (platform.transform.position.x <= leftPositon + 1  ) // create second platform
            {
                GameObject platform2 = Instantiate(platforms[0]);
                platform2.transform.parent = this.transform;
                platform2.transform.position = new Vector3(Random.Range(0f, rightPositon - 1), platform.transform.position.y);

                spawnedObjects.Add(platform2);
            }
            lastPlatformY = (int) platform.transform.position.y;
            if ( player.childCount == 1 ) CoolStuffSpawn(platform); //if player has nothing equipped - try to spawn cool stuff
        }
        BackgroundSpawn();

        RemoveUnnesseccaryObjects();

    }

    private void EnemySpawn()
    {
        if (player.transform.position.y >= 0 && lastEnemyY - player.transform.position.y < 5)
        {
            GameObject enemy = Instantiate(enemies[Random.Range(0, enemies.Count)]);
            enemy.transform.parent = this.transform;
            enemy.transform.position = new Vector3(Random.Range(leftPositon + 1, rightPositon - 1), player.transform.position.y + Random.Range(15, 20));

            spawnedObjects.Add(enemy);
            lastEnemyY = (int)enemy.transform.position.y;
        }
    }

    private void RemoveUnnesseccaryObjects()
    {
       
        while (spawnedObjects[0] == null)
        {
            spawnedObjects.RemoveAt(0);
        }

        GameObject platform = spawnedObjects[0];

        if (platform != null && player.transform.position.y - platform.transform.position.y > 20)
        {
            Destroy(platform.gameObject);
            spawnedObjects.RemoveAt(0);
        }
    }
}
