using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable]
public class BuildingData
{
    public Vector3 position;
    public List<Vector3> verts = new List<Vector3>();
    public List<int> tris = new List<int>();
    public List<Vector2> uvs = new List<Vector2>();
    public float width;
    public float depth;
    public float area;
    public float rotation;
}

public class BuildingGenerator : MonoBehaviour
{
    [Header("Building Type")] 
    public BuildingType type;
    public enum BuildingType { VERTS, BOUNDING, AREA };


    public GameObject building;

    

    [HideInInspector] public List<BuildingData> buildings;
    private List<Vector3> verts;
    private List<int> tris;
    private Vector2[] uvs;
    [HideInInspector] public int buildingCount;
    


    [Header("Prefabs")]
    public GameObject brickHouse;
    public GameObject brickHouseMedium;
    public GameObject largeApartment;
    public GameObject storageBuilding;
    public GameObject hut;
    [Header("Prefab Options")]
    public bool useImageDimensions;
    public bool rotateBuildings;

    string directoryVerts;
    string directoryTris;


    private void Start()
    {
        //GetVerts();
        //uvs = new Vector2[verts.Count];
        //GetTris();
        //SetUVs();
        ////GenerateYVerts();

        //Generate();




    }

    public void StartBuildingCreation()
    {
        GetBuildingData();


        if (type != BuildingType.AREA)
        {
            CreateBuilding();
        }

    }


    void CreateBuilding()
    {
        foreach (var b in buildings)
        {
            GameObject newBuilding = Instantiate(building);
            Building buildingScript = newBuilding.GetComponent<Building>();
            
            buildingScript.verts = b.verts;
            buildingScript.tris = b.tris;
            buildingScript.width = b.width;
            buildingScript.depth = b.depth;
            buildingScript.area = b.area;
            buildingScript.uvs = b.uvs;
            buildingScript.rotate = rotateBuildings;
            if (type != BuildingType.AREA)
            {
                newBuilding.transform.position = new Vector3(b.position.x - (b.width/2), 0, b.position.z - (b.depth/2));
            }
            else
            {
                newBuilding.transform.position = b.position;
            }
        }

    }


    void BuildingArea()
    {
        int index = 0;

        string[] areaFiles = Directory.GetFiles(GetComponent<GetTextFiles>().boundingArea, "*.txt");

        foreach (var file in areaFiles)
        {
            if(index < buildings.Count)
            {
                string[] lines = System.IO.File.ReadAllLines(file);

                string fileName = Path.GetFileName(file);
                string[] nameSplit1 = fileName.Split('.');
                string[] nameSplit2 = nameSplit1[0].Split('(');
                string[] nameSplit3 = nameSplit2[1].Split(')');
                string[] nameSplit4 = nameSplit3[0].Split(',');

                buildings[index].rotation = -float.Parse(nameSplit4[2]);


                Vector3 pos = new Vector3(float.Parse(nameSplit4[0]), 0, float.Parse(nameSplit4[1]));

                buildings[index].position = pos;

                buildings[index].width = float.Parse(lines[0]);
                buildings[index].depth = float.Parse(lines[1]);

                buildings[index].area = buildings[index].width * buildings[index].depth;
                index++;
            }


            
        }
    }

    void AreaBuildings()
    {
        string[] areaFiles = Directory.GetFiles(GetComponent<GetTextFiles>().boundingArea, "*.txt");

        for (int i = 0; i < areaFiles.Length; i++)
        {
            BuildingData building = new BuildingData();
            buildings.Add(building);
        }

        BuildingArea();

        foreach (var building in buildings)
        {

            if(building.area <= 1000 && building.area >= 500)
            {
                GameObject house = Instantiate(brickHouse, building.position, Quaternion.identity);

                if (useImageDimensions)
                {
                    house.transform.localScale = new Vector3(building.width / 10, building.width / 10, building.depth / 5);
                }
                house.transform.eulerAngles = new Vector3(transform.eulerAngles.x, building.rotation, transform.eulerAngles.z);

            }
            else if(building.area < 500 && building.area > 200)
            {
                GameObject house = Instantiate(brickHouseMedium, building.position, Quaternion.identity);
                if (useImageDimensions)
                {
                    house.transform.localScale = new Vector3(building.width / 10, building.width / 10, building.depth / 5);
                }
                house.transform.eulerAngles = new Vector3(transform.eulerAngles.x, building.rotation, transform.eulerAngles.z);

            }
            else if(building.area > 1000 && building.area <= 5000)
            {
                GameObject house = Instantiate(largeApartment, building.position, Quaternion.identity);
                if (useImageDimensions)
                {
                    house.transform.localScale = new Vector3(building.width / 10, building.width / 10, building.depth / 5);
                }
                house.transform.eulerAngles = new Vector3(transform.eulerAngles.x, building.rotation, transform.eulerAngles.z);
            }
            else if(building.area > 5000)
            {
                GameObject house = Instantiate(storageBuilding, building.position, Quaternion.identity);
                if (useImageDimensions)
                {
                    house.transform.localScale = new Vector3(building.width / 10, building.width / 10, building.depth / 5);
                }
                house.transform.eulerAngles = new Vector3(transform.eulerAngles.x, building.rotation, transform.eulerAngles.z);
                house.transform.localScale = new Vector3(6, 6, 6);
            }
        }

    }

    void GetBuildingData()
    {
        int vertIndex = 0;
        int triIndex = 0;

        if (type == BuildingType.BOUNDING)
        {
            directoryVerts = GetComponent<GetTextFiles>().boundingVerts;
            directoryTris = GetComponent<GetTextFiles>().boundingTris;
            
        }
        else if(type == BuildingType.VERTS)
        {
            directoryVerts = GetComponent<GetTextFiles>().normalVerts;
            directoryTris = GetComponent<GetTextFiles>().normalTris;
        }
        else if(type == BuildingType.AREA)
        {
            AreaBuildings();
            return;
        }


        
        string[] vertFiles = Directory.GetFiles(directoryVerts,"*.txt");
        string[] triFiles = Directory.GetFiles(directoryTris, "*.txt");

        

        for (int i = 0; i < vertFiles.Length; i++)
        {
            BuildingData building = new BuildingData();
            buildings.Add(building);
        }

        
        foreach (var file in vertFiles)
        {
            string[] lines = System.IO.File.ReadAllLines(file);

            string fileName = Path.GetFileName(file);
            string[] nameSplit1 = fileName.Split('.');
            string[] nameSplit2 = nameSplit1[0].Split('(');
            string[] nameSplit3 = nameSplit2[1].Split(')');
            string[] nameSplit4 = nameSplit3[0].Split(',');

            

            Vector3 pos = new Vector3(float.Parse(nameSplit4[0]), 0, float.Parse(nameSplit4[1]));

            buildings[vertIndex].position = pos;

            foreach (var line in lines)
            {
                string[] split = line.Split(',');
                Vector3 vert = new Vector3(float.Parse(split[0]), float.Parse(split[1]), float.Parse(split[2]));
                buildings[vertIndex].verts.Add(vert);
                buildings[vertIndex].uvs.Add(new Vector2(vert.x, vert.z));
            }
            vertIndex++;
        }

        foreach (var file in triFiles)
        {
            string[] lines = System.IO.File.ReadAllLines(file);

            foreach (var line in lines)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    buildings[triIndex].tris.Add(int.Parse(line));
                }
            }
            triIndex++;
        }

        BuildingArea();
        

    }



}
