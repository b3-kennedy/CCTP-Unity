using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Foliage : MonoBehaviour
{
    public GameObject tree;

    private void Start()
    {
        PlaceTrees();
    }

    void PlaceTrees()
    {
        string[] lines = System.IO.File.ReadAllLines("Assets/Resources/Trees/trees.txt");
        foreach (var line in lines)
        {
            Debug.Log(line);
            string[] split = line.Split(',');
            Vector3 treePos = new Vector3(int.Parse(split[0]), 0, int.Parse(split[1]));
            Instantiate(tree, treePos, Quaternion.identity);
        }
    }

    void PlaceGrass()
    {
        string[] lines = System.IO.File.ReadAllLines("Assets/Resources/Trees/grass.txt");
        foreach (var line in lines)
        {
            Debug.Log(line);
            string[] split = line.Split(',');
            Vector3 treePos = new Vector3(int.Parse(split[0]), 0, int.Parse(split[1]));
            Instantiate(tree, treePos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
