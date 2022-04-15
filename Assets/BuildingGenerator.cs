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
    public List<BuildingData> buildings;
    public GameObject building;
    
    public MeshFilter meshFilter;
    public Vector2Int size;
    public List<Vector3> verts;
    public List<int> tris;
    public Vector2[] uvs;
    public bool useBoundingBoxGeometry;
    public int buildingCount;

    public enum BuildingType {VERTS, BOUNDING, AREA};

    public BuildingType type;

    public GameObject brickHouse;
    public GameObject brickHouseMedium;
    public GameObject largeApartment;
    public GameObject storageBuilding;
    public GameObject hut;
    public bool useImageDimensions;

    string directory;


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
        Debug.Log("building data");
        GetBuildingData();


        if (type != BuildingType.AREA)
        {
            CreateBuilding();
        }

        if(buildingCount >= buildings.Count)
        {
            GetComponent<Foliage>().Place();
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
            if (type == BuildingType.BOUNDING)
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

        string[] areaFiles = Directory.GetFiles("Assets/Resources/Buildings/boundingarea", "*.txt");

        foreach (var file in areaFiles)
        {
            string[] lines = System.IO.File.ReadAllLines(file);

            string fileName = Path.GetFileName(file);
            string[] nameSplit1 = fileName.Split('.');
            string[] nameSplit2 = nameSplit1[0].Split('(');
            string[] nameSplit3 = nameSplit2[1].Split(')');
            string[] nameSplit4 = nameSplit3[0].Split(',');

            buildings[index].rotation = float.Parse(nameSplit4[2]);


            Vector3 pos = new Vector3(float.Parse(nameSplit4[0]), 0, float.Parse(nameSplit4[1]));

            buildings[index].position = pos;

            buildings[index].width = float.Parse(lines[0]);
            buildings[index].depth = float.Parse(lines[1]);

            buildings[index].area = buildings[index].width * buildings[index].depth;

            index++;
        }
    }

    void AreaBuildings()
    {
        string[] areaFiles = Directory.GetFiles("Assets/Resources/Buildings/boundingarea", "*.txt");

        for (int i = 0; i < areaFiles.Length; i++)
        {
            BuildingData building = new BuildingData();
            buildings.Add(building);
        }

        BuildingArea();

        foreach (var building in buildings)
        {
            if(building.area < 200)
            {
                GameObject house = Instantiate(hut, building.position, Quaternion.identity);

                if (useImageDimensions)
                {
                    house.transform.localScale = new Vector3(building.width / 10, building.width / 10, building.depth / 5);
                }
                house.transform.eulerAngles = new Vector3(transform.eulerAngles.x, building.rotation, transform.eulerAngles.z);
            }
            else if(building.area <= 1000 && building.area >= 500)
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
            directory = "bounding";
            
        }
        else if(type == BuildingType.VERTS)
        {
            directory = "";
        }
        else if(type == BuildingType.AREA)
        {
            AreaBuildings();
            return;
        }


        
        string[] vertFiles = Directory.GetFiles("Assets/Resources/Buildings/"+directory+"verts","*.txt");
        string[] triFiles = Directory.GetFiles("Assets/Resources/Buildings/"+directory+"tris", "*.txt");

        

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

        if(type != BuildingType.VERTS)
        {
            BuildingArea();
        }
        

    }

    void GetTris()
    {
        string path = "Assets/Resources/" + "tris" + ".txt";
        string[] lines = System.IO.File.ReadAllLines(path);
        foreach (var line in lines)
        {
            if (!string.IsNullOrEmpty(line))
            {
                tris.Add(int.Parse(line));
            }
        }
    }

    void GetVerts()
    {
        string path = "Assets/Resources/" + "verts" + ".txt";
        string[] lines = System.IO.File.ReadAllLines(path);
        foreach (var line in lines)
        {
            string[] split = line.Split(',');
            Vector3 vert = new Vector3(float.Parse(split[0]), float.Parse(split[1]), float.Parse(split[2]));
            verts.Add(vert);
        }
    }

    void SetUVs()
    {
        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(verts[i].x, verts[i].z);
        }
    }

    //private void Update()
    //{
    //    Generate();
    //}

    public void Generate()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.uv = uvs;

        mesh.RecalculateNormals();
        meshFilter.sharedMesh = mesh;
    }

}
