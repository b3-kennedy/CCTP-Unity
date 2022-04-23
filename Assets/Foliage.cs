using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[ExecuteInEditMode]
public class Foliage : MonoBehaviour
{
    public GameObject tree;
    public GameObject grass;
    public LayerMask treeLayerMask;

    private void Start()
    {

        //PlaceGrass();
    }

    public void Place()
    {
        Debug.Log("trees");
        PlaceTrees();
    }

    void PlaceTrees()
    {
        string[] lines = System.IO.File.ReadAllLines(GetComponent<GetTextFiles>().treePath);
        foreach (var line in lines)
        {
            string[] split = line.Split(',');
            Vector3 treePos = new Vector3(int.Parse(split[0]), 0, int.Parse(split[1]));
            RaycastHit hit;
            if (Physics.Raycast(new Vector3(treePos.x, 100, treePos.z), Vector3.down, out hit, Mathf.Infinity, treeLayerMask))
            {
                Debug.Log(hit.collider);
                if (hit.collider.tag != "Building")
                {
                    GameObject newTree = Instantiate(tree, new Vector3(treePos.x, hit.point.y, treePos.z), Quaternion.identity);

                }
                

            }


        }
    }

    void PlaceGrass()
    {
        string[] lines = System.IO.File.ReadAllLines(GetComponent<GetTextFiles>().terrainColourGrassPath);
        foreach (var line in lines)
        {
            string[] split = line.Split(',');
            Vector3 grassPos = new Vector3(int.Parse(split[0])*2, 0, int.Parse(split[1])*2);
            //Instantiate(grass, grassPos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
