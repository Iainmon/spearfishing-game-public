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
    private Vector2 v2SampleStart = new Vector2(21.2019f, 21.2019f);

    void Start()
    {

        v2SampleStart.x = chunkOffsetX * chunkSize + 0.1f;
        v2SampleStart.y = chunkOffsetY * chunkSize + 0.1f;
        Noise();
    }

    void Noise()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        Vector3[] vertices = mf.mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            float xCoord = v2SampleStart.x + vertices[i].x;
            float zCoord = v2SampleStart.y + vertices[i].z;
            
            float noiseValue = Mathf.PerlinNoise(xCoord * 0.15f, zCoord * 0.15f);

            vertices[i].y = noiseValue * 10;
            
            print(noiseValue);
        }
        mf.mesh.vertices = vertices;
        mf.mesh.RecalculateBounds();
        mf.mesh.RecalculateNormals();

        GetComponent<MeshCollider>().sharedMesh = null;
        GetComponent<MeshCollider>().sharedMesh = mf.mesh;
    }
}