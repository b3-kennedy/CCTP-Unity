using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Building : MonoBehaviour
{
    public MeshFilter meshFilter;
    public List<Vector3> verts;
    List<Vector3> newVerts;
    MeshCollider meshCol;
    public List<int> tris;
    public List<Vector2> uvs;
    public float width;
    public float depth;
    public float area;
    public float rotation;
    public List<Vector3> bottomVerts;
    float range = 1;
    float terrainHeight;
    [HideInInspector] public bool rotate;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("building");
        if(verts.Count > 0)
        {
            meshCol = GetComponent<MeshCollider>();
            Generate();
            Vector3 centre = new Vector3(width / 2, 0, depth / 2);
            RaycastHit centreHit;
            if (rotate)
            {
                if (Physics.Raycast(transform.position + centre, Vector3.down * range, out centreHit))
                {
                    Quaternion rot = Quaternion.FromToRotation(Vector3.up, centreHit.normal);
                    transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, rot.z, transform.rotation.w);
                }
            }

            foreach (var vert in verts)
            {
                RaycastHit hit;
                if (vert.y == 0)
                {
                    if (Physics.Raycast(transform.position + vert, Vector3.down * range, out hit))
                    {
                        if (hit.collider)
                        {
                            terrainHeight = hit.point.y;
                            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
                        }
                    }

                    
                    //if (vertWorldPos.y < terrainHeight)
                    //{
                    //    transform.position = new Vector3(transform.position.x, terrainHeight, transform.position.z);
                    //}
                }
                Vector3 vertWorldPos = vert + transform.position;
                float vertYpos = transform.position.y + vert.y;
                
                if(vertYpos < MeshGenerator.GetTerrainHeight(vertWorldPos))
                {
                    transform.localScale = new Vector3(transform.localScale.x, 2, transform.localScale.z);
                }
            }


        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down * range, out hit))
            {
                if (hit.collider)
                {
                    if (rotate)
                    {
                        Quaternion rot = Quaternion.FromToRotation(Vector3.up, hit.normal);
                        transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, rot.z, transform.rotation.w);
                    }
                    transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
                }
            }
        }
        MeshGenerator.world.GetComponent<BuildingGenerator>().buildingCount += 1;

    }



    // Update is called once per frame
    void Update()
    {

    }


    public void Generate()
    {
        if(verts.Count > 0)
        {
            Mesh mesh = new Mesh();
            
            mesh.vertices = verts.ToArray();
            mesh.triangles = tris.ToArray();
            meshCol.sharedMesh = mesh;
            mesh.uv = uvs.ToArray();

            mesh.RecalculateNormals();
            meshFilter.sharedMesh = mesh;
        }

    }


}
