using UnityEditor;
using UnityEngine;
using System.Collections;

public class TerrainController : MonoBehaviour
{

    public float tiling = 10.0f;
    private Terrain terrain;

    void Start()
    {
        terrain = GetComponent<Terrain>();
        GenerateHeights(terrain, tiling);
    }

    public void GenerateHeights(Terrain terrain, float tileSize)
    {
        float[,] heights = new float[terrain.terrainData.heightmapWidth, terrain.terrainData.heightmapHeight];

        for (int i = 0; i < terrain.terrainData.heightmapWidth; i++)
        {
            for (int k = 0; k < terrain.terrainData.heightmapHeight; k++)
            {
                heights[i, k] = Mathf.PerlinNoise(
                    (
                        (float)i / (float)terrain.terrainData.heightmapWidth
                    ) * tileSize,
                    (
                        (float)k / (float)terrain.terrainData.heightmapHeight
                    ) * tileSize
                    ) / 10.0f;
            }
        }

        terrain.terrainData.SetHeights(0, 0, heights);
    }
}