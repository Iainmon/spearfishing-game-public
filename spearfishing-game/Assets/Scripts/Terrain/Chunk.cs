using UnityEditor;
using UnityEngine;
using System.Collections;

public class Chunk : MonoBehaviour
{
    public static int chunkSize = 10;

    public int chunkOffsetX = 0;
    public int chunkOffsetY = 0;

    public float seed = 200.0f;

    //Vector3[] newVertices;
    Vector2[] newUV;
    int[] newTriangles;

    public float power = 1.0f;
    public float scale = 1.0f;
    private Vector2 chunkStartLocation = new Vector2(21.2019f, 21.2019f);

    void Start()
    {

        chunkStartLocation.x = chunkOffsetX * chunkSize + 0.1f;
        chunkStartLocation.y = chunkOffsetY * chunkSize + 0.1f;
        Noise();
    }

    void Noise()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        Vector3[] vertices = mf.mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            float xCoord = chunkStartLocation.x + vertices[i].x;
            float zCoord = chunkStartLocation.y + vertices[i].z;
            
            float noiseValue = Mathf.PerlinNoise(xCoord * 0.15f, zCoord * 0.15f);

            vertices[i].y = noiseValue * 10;
            
        }
        mf.mesh.vertices = vertices;
        mf.mesh.RecalculateBounds();
        mf.mesh.RecalculateNormals();

        GetComponent<MeshCollider>().sharedMesh = null;
        GetComponent<MeshCollider>().sharedMesh = mf.mesh;
    }
}