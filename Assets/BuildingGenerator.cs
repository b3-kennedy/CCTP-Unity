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


    private void Start()
    {
        //GetVerts();
        //uvs = new Vector2[verts.Count];
        //GetTris();
        //SetUVs();
        ////GenerateYVerts();

        //Generate();

        GetBuildingData();
        CreateBuilding();
    }


    void CreateBuilding()
    {
        foreach (var b in buildings)
        {
            GameObject newBuilding = Instantiate(building);
            Building buildingScript = newBuilding.GetComponent<Building>();
            newBuilding.transform.position = b.position;
            buildingScript.verts = b.verts;
            buildingScript.tris = b.tris;
        }

        
    }

    void GetBuildingData()
    {
        int vertIndex = 0;
        int triIndex = 0;
        string[] vertFiles = Directory.GetFiles("Assets/Resources/Buildings/verts","*.txt");
        string[] triFiles = Directory.GetFiles("Assets/Resources/Buildings/tris", "*.txt");

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
