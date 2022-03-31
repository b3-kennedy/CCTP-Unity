using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public MeshFilter meshFilter;
    public List<Vector3> verts;
    public List<int> tris;
    public Vector2[] uvs;


    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


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
