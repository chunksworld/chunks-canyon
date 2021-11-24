
using System.Collections.Generic;
using UnityEngine;

public class EnviromentSpawn : MonoBehaviour
{

    List<GameObject> forest = new List<GameObject>();
    
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
        FillForest();
        treeLength = 8;
    }

  
    private void SpawnForest()
    {
        SpawnTree();
        SpawnPlants();
    }

    #region Forest
    private void FillForest()
    {

        forest.Add(Resources.Load("Background/Forest/Liana") as GameObject);
        forest.Add(Resources.Load("Background/Forest/Liana2") as GameObject);
        forest.Add(Resources.Load("Background/Forest/Liana3") as GameObject);
        forest.Add(Resources.Load("Background/Forest/Liana4") as GameObject);

        forest.Add(Resources.Load("Background/Forest/Plant") as GameObject);
        forest.Add(Resources.Load("Background/Forest/Plant2") as GameObject);
        forest.Add(Resources.Load("Background/Forest/Plant3") as GameObject);
        forest.Add(Resources.Load("Background/Forest/Plant4") as GameObject);

        tree = Resources.Load("Background/Forest/Tree") as GameObject;
    }
    private void SpawnTree()
    {
        if (Mathf.Abs(lastSideDecoration - player.transform.position.y) < 16)
        {
            GameObject tree1 = Instantiate(tree);
            GameObject tree2 = Instantiate(tree);

            lastSideDecoration += treeLength;
            tree1.transform.position = new Vector3(9.5f, lastSideDecoration, 2.3f);
            tree1.transform.rotation = Quaternion.Euler(0f, rotations[Random.Range(0, rotations.Length)], 0f);
            tree2.transform.position = new Vector3(-8f, lastSideDecoration, 0f);
            tree2.transform.rotation = Quaternion.Euler(0f, rotations[Random.Range(0, rotations.Length)], 0f);

            tree1.transform.parent = this.transform;
            tree2.transform.parent = this.transform;

            spawnedObjects.Add(tree2);
            spawnedObjects.Add(tree1);
        }
    }

    
    private void SpawnPlants()
    {
        if (Mathf.Abs(player.transform.position.y - lastDecoration) < 12f)
        {

            GameObject decoration1 = Instantiate(forest[Random.Range(0, forest.Count)]);
            GameObject decoration2 = Instantiate(forest[Random.Range(0, forest.Count)]);

            decoration1.transform.position = new Vector3(leftPositon, lastDecoration + Random.Range(3, 6), 1.3f);
            decoration2.transform.position = new Vector3(rightPositon, lastDecoration + Random.Range(3, 6), 3.5f);

            decoration1.transform.parent = this.transform;
            decoration2.transform.parent = this.transform;

            decoration2.transform.rotation = Quaternion.Euler(0f, 180, 0f);
            lastDecoration = decoration1.transform.position.y;

            spawnedObjects.Add(decoration2);
            spawnedObjects.Add(decoration1);

        }
    }
    #endregion

    private void FixedUpdate()
    {
        SpawnForest();
        RemoveUnnesseccaryPlatforms();
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
