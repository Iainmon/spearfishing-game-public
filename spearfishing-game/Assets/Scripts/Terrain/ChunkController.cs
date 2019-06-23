using UnityEditor;
using UnityEngine;
using System.Collections;

public class ChunkController : MonoBehaviour
{

    public int chunksToLoad = 10;

    public GameObject chunkTemplate;

    private GameObject[,] chunks;

    void Start() {
        for (int x = 0; x < chunksToLoad; x++) {
            for (int z = 0; z < chunksToLoad; z++) {
                // chunks[x, z] = (GameObject)Instantiate(chunkTemplate, new Vector3(x + Chunk.chunkSize, 0, z + Chunk.chunkSize), Quaternion.identity);
                // chunks[x, z].GetComponent<Chunk>().chunkOffsetX = x;
                // chunks[x, z].GetComponent<Chunk>().chunkOffsetY = z;
                GameObject newChunk = (GameObject)Instantiate(chunkTemplate, new Vector3(x * Chunk.chunkSize, 0, z * Chunk.chunkSize), Quaternion.identity);
                newChunk.GetComponent<Chunk>().chunkOffsetX = x;
                newChunk.GetComponent<Chunk>().chunkOffsetY = z;
            }
        }
    }
    void Update() {

    }
}