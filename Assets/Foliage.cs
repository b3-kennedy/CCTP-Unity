using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Foliage : MonoBehaviour
{
    public GameObject tree;
    public GameObject grass;

    private void Start()
    {
        PlaceTrees();
        //PlaceGrass();
    }

    void PlaceTrees()
    {
        string[] lines = System.IO.File.ReadAllLines("Assets/Resources/Trees/trees.txt");
        foreach (var line in lines)
        {
            string[] split = line.Split(',');
            Vector3 treePos = new Vector3(int.Parse(split[0])*2, 0, int.Parse(split[1])*2);
            Instantiate(tree, treePos, Quaternion.identity);
        }
    }

    void PlaceGrass()
    {
        string[] lines = System.IO.File.ReadAllLines("Assets/Resources/Trees/grassarea.txt");
        foreach (var line in lines)
        {
            string[] split = line.Split(',');
            Vector3 grassPos = new Vector3(int.Parse(split[0])*2, 0, int.Parse(split[1])*2);
            Instantiate(grass, grassPos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
