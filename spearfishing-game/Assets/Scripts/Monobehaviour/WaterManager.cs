using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterManager : MonoBehaviour
{
    public static float waveHeight = 0.5f;
    public static float waveFrequency = 0.5f;
    public static float waveLength = 0.75f;
    public static Vector3 waveOriginPosition = new Vector3(0.0f, 0.0f, 0.0f);

    MeshFilter meshFilter;
    Mesh mesh;
    Vector3[] vertices;

    private void Awake() {
        //Get the Mesh Filter of the gameobject
        meshFilter = GetComponent<MeshFilter>();
    }

    void Start() {
        CreateMeshLowPoly(meshFilter);
    }
    
    MeshFilter CreateMeshLowPoly(MeshFilter mf) {
        mesh = mf.sharedMesh;

        //Get the original vertices of the gameobject's mesh
        Vector3[] originalVertices = mesh.vertices;

        //Get the list of triangle indices of the gameobject's mesh
        int[] triangles = mesh.triangles;

        //Create a vector array for new vertices 
        Vector3[] vertices = new Vector3[triangles.Length];

        //Assign vertices to create triangles out of the mesh
        for (int i = 0; i < triangles.Length; i++) {
            vertices[i] = originalVertices[triangles[i]];
            triangles[i] = i;
        }

        //Update the gameobject's mesh with new vertices
        mesh.vertices = vertices;
        mesh.SetTriangles(triangles, 0);
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        this.vertices = mesh.vertices;

        return mf;
    }

    void FixedUpdate() {
        GenerateWaves();
    }


    public static float GetOceanY(Vector3 pos) {
        float distance = Vector2.Distance(new Vector2(pos.x, pos.z), new Vector2(waveOriginPosition.x, waveOriginPosition.z));
        distance = (distance % waveLength) / waveLength;
        float y = waveHeight * Mathf.Sin(Time.time * Mathf.PI * 2.0f * waveFrequency
        + (Mathf.PI * 2.0f * distance));
        return y;
    }
    

    void GenerateWaves() {
        for (int i = 0; i < vertices.Length; i++) {
            Vector3 v = vertices[i];

            //Oscilate the wave height via sine to create a wave effect
            v.y = GetOceanY(v);

            //Update the vertex
            vertices[i] = v;
        }

        //Update the mesh properties
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        mesh.MarkDynamic();
        meshFilter.mesh = mesh;
    }
}
