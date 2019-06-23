using UnityEditor;
using UnityEngine;
using System.Collections;

public class Chunk : MonoBehaviour
{
    public static int chunkSize = 10;

    public int chunkOffsetX = 0;
    public int chunkOffsetY = 0;

    //Vector3[] newVertices;
    Vector2[] newUV;
    int[] newTriangles;

    void Start() {
        
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;

        for (int i = 0; i < chunkSize; i++)
        {
            for (int k = 0; k < chunkSize; k++)
            {
                float actualX = (float)(i + (chunkOffsetX * chunkSize));
                float actualY = (float)(k + (chunkOffsetY * chunkSize));

                vertices[i * chunkSize + k].y = Mathf.PerlinNoise(actualX, actualY);
            }
        }

        mesh.vertices = vertices;
        mesh.;
        //mesh.uv = newUV;
        //mesh.triangles = newTriangles;
    }

    public Vector3[,] GenerateHeights(Vector3[,] vertices, float tileSize)
    {
        //float[,] heights = new float[terrain.terrainData.heightmapWidth, terrain.terrainData.heightmapHeight];


        return vertices;
        //terrain.terrainData.SetHeights(0, 0, heights);
    }
}