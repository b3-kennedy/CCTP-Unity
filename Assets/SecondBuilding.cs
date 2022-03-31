using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SecondBuilding : MonoBehaviour
{
    public MeshFilter meshFilter;
    public Vector2Int size;
    public List<Vector3> verts;
    public List<int> tris;
    public Vector2[] uvs;


    private void Start()
    {
        GetVerts();
        uvs = new Vector2[verts.Count];
        GetTris();
        SetUVs();
        //GenerateYVerts();

        Generate();
    }

    void GetTris()
    {
        string path = "Assets/Resources/" + "tris" + ".txt";
        string[] lines = System.IO.File.ReadAllLines(path);
        foreach (var line in lines)
        {
            if(!string.IsNullOrEmpty(line))
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
