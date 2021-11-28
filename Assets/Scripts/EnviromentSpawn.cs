
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnviromentSpawn : MonoBehaviour
{

    List<GameObject> forest = new List<GameObject>();

    List<GameObject> decorations = new List<GameObject>();
    
    GameObject tree;
    GameObject player;

    int[] rotations = new int[4] { 0, 90, 180, 270 };
    float lastDecoration;
    float lastSideDecoration;
    float treeLength;

    int rightPositon = 5;
    int leftPositon = -4;

    List<GameObject> spawnedObjects = new List<GameObject>();
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lastDecoration = 10;
        lastSideDecoration = 0;
        FillDecorations();
        treeLength = 8;
    }
    private void FillDecorations()
    {
        if (SceneManager.GetActiveScene().name == "Forest")
        {
            decorations.Add(Resources.Load("Background/Forest/Tree1") as GameObject);
            decorations.Add(Resources.Load("Background/Forest/Tree2") as GameObject);
        }

        if (SceneManager.GetActiveScene().name == "Ocean")
        {
            decorations.Add(Resources.Load("Background/Ocean/CoralReifWall") as GameObject);

        }
    }

    private void FixedUpdate()
    {
        SpawnDecorations();
        RemoveUnnesseccaryPlatforms();
    }




    private void SpawnDecorations()
    {
        if (Mathf.Abs(lastSideDecoration - player.transform.position.y) < 16)
        {
            GameObject decorationLeft = Instantiate(decorations[Random.Range(0, decorations.Count)]);
            GameObject decorationRight = Instantiate(decorations[Random.Range(0, decorations.Count)]);

            lastSideDecoration += treeLength;
            decorationLeft.transform.position = new Vector3(9.5f, lastSideDecoration, 2.3f);
            decorationLeft.transform.rotation = Quaternion.Euler(0f, rotations[Random.Range(0, rotations.Length)], 0f);
            decorationRight.transform.position = new Vector3(-8f, lastSideDecoration, 0f);
            decorationRight.transform.rotation = Quaternion.Euler(0f, rotations[Random.Range(0, rotations.Length)], 0f);

            decorationLeft.transform.parent = this.transform;
            decorationRight.transform.parent = this.transform;

            spawnedObjects.Add(decorationLeft);
            spawnedObjects.Add(decorationRight);
        }
    }




    private void RemoveUnnesseccaryPlatforms()
    {

        while (spawnedObjects[0] == null)
        {
            spawnedObjects.RemoveAt(0);
        }

        GameObject decoration = spawnedObjects[0];

        if (decoration != null && player.transform.position.y - decoration.transform.position.y > 20)
        {
            Destroy(decoration.gameObject);
            spawnedObjects.RemoveAt(0);
        }
    }
}
