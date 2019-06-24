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

    public float power = 300.0f;
    public float scale = 1.0f;
    private Vector2 v2SampleStart = new Vector2(21.2019f, 21.2019f);

    void Start()
    {
        seed += chunkOffsetX * chunkSize;
        print(seed);

        v2SampleStart.x = chunkOffsetX * chunkSize + 0.1f;
        v2SampleStart.y = chunkOffsetY * chunkSize + 0.1f;

        print("x: " + v2SampleStart.x + " y:" + v2SampleStart.y);

        Noise();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //v2SampleStart = new Vector2(Random.Range (0.0f, 100.0f), Random.Range (0.0f, 100.0f));
            //v2SampleStart = new Vector2(chunkOffsetX * chunkSize, chunkOffsetY * chunkSize);
            Noise();
        }
    }

    void Noise()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        Vector3[] vertices = mf.mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            float xCoord = v2SampleStart.x + vertices[i].x;
            float yCoord = v2SampleStart.y + vertices[i].z;
            
            float noiseValue = Mathf.PerlinNoise(xCoord + seed, yCoord + seed) - 0.05f;

            vertices[i].y = noiseValue * power;
            
            print(noiseValue);
        }
        mf.mesh.vertices = vertices;
        mf.mesh.RecalculateBounds();
        mf.mesh.RecalculateNormals();
    }
}