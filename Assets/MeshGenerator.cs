using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;



public class MeshGenerator : MonoBehaviour
{

    Vector3[] vertices;
    int[] triangles;
    public int xSize;
    public int zSize;
    Mesh mesh;
    public Material mat;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().material = mat;
        CreateShape();
        UpdateMesh();
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



    }

    public void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

    }
}


