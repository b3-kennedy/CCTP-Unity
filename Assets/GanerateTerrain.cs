using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GanerateTerrain : MonoBehaviour
{

    Vector3[] newVerts = new Vector3[400*400];
    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = gameObject.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        for (var i = 0; i < vertices.Length; i++)
        {
            Debug.Log(vertices[i]);
        }

        UpdateMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void UpdateMesh()
    {
        Mesh mesh = gameObject.GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        mesh.vertices = newVerts;
        mesh.triangles = mesh.triangles;
        mesh.RecalculateNormals();
    }
}
