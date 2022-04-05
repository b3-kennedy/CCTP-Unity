using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;



public class MeshGenerator : MonoBehaviour
{

    Vector3[] vertices;
    int[] triangles;
    Vector2[] uvs;
    Color[] colors;
    List<Vector2> treeVerts;
    List<Vector2> grassVerts;
    public int xSize;
    public int zSize;
    public Color forestColor;
    public Color grassColor;
    public Color defaultColor;
    public bool isRoad;
    Mesh mesh;
    public Material mat;
    public Material testMat;
    Material currentMat;


    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().material = mat;
        currentMat = GetComponent<MeshRenderer>().material;
        CreateShape();
        UpdateMesh();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentMat.SetFloat("Float", 1);
            Debug.Log(currentMat.GetFloat("Float"));
        }
        
    }

    void CreateShape()
    {

        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                //print("normal: " + y + " | " + "abnormal: " + testy);

                vertices[i] = new Vector3(x, 0, z);


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

        string path = "Assets/Resources/Trees/" + "treearea" + ".txt";
        string[] lines = System.IO.File.ReadAllLines(path);
        treeVerts = new List<Vector2>();

        for (int i = 0; i < lines.Length; i++)
        {
            string[] split = lines[i].Split(',');
            treeVerts.Add(new Vector2(int.Parse(split[0]), int.Parse(split[1])));
        }


        string grassPath = "Assets/Resources/Trees/" + "grassarea" + ".txt";
        string[] grassLines = System.IO.File.ReadAllLines(grassPath);
        grassVerts = new List<Vector2>();

        for (int i = 0; i < grassLines.Length; i++)
        {
            string[] split = grassLines[i].Split(',');
            grassVerts.Add(new Vector2(int.Parse(split[0]), int.Parse(split[1])));
        }


        colors = new Color[vertices.Length];
        uvs = new Vector2[vertices.Length];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                uvs[i] = new Vector2(x, z);

                Vector2 vertPos = new Vector2(x, z);


                if (grassVerts.Contains(vertPos))
                {
                    currentMat.SetInt("Boolean_b78a691b4160461ab7a23dcb12ddb6a3", 0);
                    colors[i] = grassColor;
                }

                else if (treeVerts.Contains(vertPos))
                {
                    currentMat.SetInt("Boolean_b78a691b4160461ab7a23dcb12ddb6a3", 0);
                    colors[i] = forestColor;
                }

                else
                {
                    currentMat.SetInt("Boolean_b78a691b4160461ab7a23dcb12ddb6a3", 1);
                    Debug.Log(currentMat.GetInt("isRoad"));
                    colors[i] = defaultColor;
                }




                //if(vertPos.x == 0 && vertPos.y == 0)
                //{
                //    colors[i] = new Color(255, 0, 0);
                //}

                i++;
            }
        }

    }

    public void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.colors = colors;
        mesh.RecalculateNormals();

    }
}


