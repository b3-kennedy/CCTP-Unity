using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


//[ExecuteInEditMode]
public class MeshGenerator : MonoBehaviour
{

    Vector3[] vertices;
    int[] triangles;
    Vector2[] uvs;
    Color[] colors;
    List<Vector2> treeVerts;
    List<Vector2> grassVerts;
    List<Vector2> roadVerts;
    public int xSize;
    public int zSize;
    public Color forestColor;
    public Color grassColor;
    public Color roadColor;
    public Color defaultColor;
    public bool isRoad;
    MeshCollider meshCol;
    Mesh mesh;
    public Material mat;
    public Material testMat;
    Material currentMat;
    public static GameObject world;
    bool treesPlaced = false;
    



    public static float GetTerrainHeight(Vector3 pos)
    {
        RaycastHit hit;
        float y = 0;
        if (Physics.Raycast(pos, Vector3.down * 100, out hit))
        {
            if (hit.collider)
            {
                y = hit.point.y;
            }
        }
        return y;
    }


    void Start()
    {
        world = gameObject;
        Debug.Log("terrain");
        mesh = new Mesh();
        meshCol = GetComponent<MeshCollider>();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().material = mat;
        currentMat = GetComponent<MeshRenderer>().material;
        CreateShape();
        UpdateMesh();

        GetComponent<BuildingGenerator>().StartBuildingCreation();



    }


    private void Update()
    {
        if (!treesPlaced)
        {
            if (GetComponent<BuildingGenerator>().buildingCount >= GetComponent<BuildingGenerator>().buildings.Count)
            {
                GetComponent<Foliage>().Place();
                treesPlaced = true;
            }
        }



        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentMat.SetFloat("Float", 1);
            Debug.Log(currentMat.GetFloat("Float"));
        }
        
    }

    void CreateShape()
    {

        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        string elevationPath = "Assets/Resources/" + "elevation" + ".txt";
        string[] elevationLines = System.IO.File.ReadAllLines(elevationPath);
        

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = float.Parse(elevationLines[i]);
                Debug.Log(i);
                //print("normal: " + y + " | " + "abnormal: " + testy);

                

                
                vertices[i] = new Vector3(x, y, z);
                


                i++;
            }
        }



        triangles = new int[xSize * zSize * 6];
        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {

                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }

        string path = GetComponent<GetTextFiles>().terrainColourTreePath;
        string[] lines = System.IO.File.ReadAllLines(path);
        treeVerts = new List<Vector2>();

        for (int i = 0; i < lines.Length; i++)
        {
            string[] split = lines[i].Split(',');
            treeVerts.Add(new Vector2(int.Parse(split[0]), int.Parse(split[1])));
        }


        string grassPath = GetComponent<GetTextFiles>().terrainColourGrassPath;
        string[] grassLines = System.IO.File.ReadAllLines(grassPath);
        grassVerts = new List<Vector2>();

        for (int i = 0; i < grassLines.Length; i++)
        {
            string[] split = grassLines[i].Split(',');
            grassVerts.Add(new Vector2(int.Parse(split[0]), int.Parse(split[1])));
        }

        string roadPath = GetComponent<GetTextFiles>().terrainColourRoadsPath;
        string[] roadLines = System.IO.File.ReadAllLines(roadPath);
        roadVerts = new List<Vector2>();

        for (int i = 0; i < roadLines.Length; i++)
        {
            string[] split = roadLines[i].Split(',');
            roadVerts.Add(new Vector2(int.Parse(split[0]), int.Parse(split[1])));
        }


        colors = new Color[vertices.Length];
        uvs = new Vector2[vertices.Length];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                uvs[i] = new Vector2(x, z);

                Vector2 vertPos = new Vector2(x, z);


                if (roadVerts.Contains(vertPos))
                {
                    colors[i] = roadColor;
                    //treeVerts.Remove(vertPos);
                    //grassVerts.Remove(vertPos);
                }
                else if (treeVerts.Contains(vertPos))
                {
                    colors[i] = forestColor;
                }
                else if (grassVerts.Contains(vertPos))
                {
                    colors[i] = grassColor;
                }
                else
                {
                    colors[i] = defaultColor;
                }




                ////if(vertPos.x == 0 && vertPos.y == 0)
                ////{
                ////    colors[i] = new Color(255, 0, 0);
                ////}

                i++;
            }
        }

    }

    public void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        Debug.Log(mesh.vertices.Length);
        mesh.triangles = triangles;
        meshCol.sharedMesh = mesh;
        mesh.uv = uvs;
        mesh.colors = colors;
        mesh.RecalculateNormals();

    }
}


