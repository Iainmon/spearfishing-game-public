using UnityEditor;
using UnityEngine;
using System.Collections;

public class ChunkController : MonoBehaviour
{

    public int chunksToLoad = 10;

    public int renderDistance = 5;

    private Vector2 renderOffset = new Vector2(0, 0);

    public GameObject player;

    public GameObject chunkTemplate;

    private GameObject[,] chunks;

    void Start() {
        for (int x = 0; x < chunksToLoad; x++) {
            for (int z = 0; z < chunksToLoad; z++) {
                // chunks[x, z] = (GameObject)Instantiate(chunkTemplate, new Vector3(x + Chunk.chunkSize, 0, z + Chunk.chunkSize), Quaternion.identity);
                // chunks[x, z].GetComponent<Chunk>().chunkOffsetX = x;
                // chunks[x, z].GetComponent<Chunk>().chunkOffsetY = z;
                GameObject newChunk = (GameObject)Instantiate(chunkTemplate, new Vector3(x * Chunk.chunkSize, transform.position.y, z * Chunk.chunkSize), Quaternion.identity);
                newChunk.GetComponent<Chunk>().chunkOffsetX = x;
                newChunk.GetComponent<Chunk>().chunkOffsetY = z;
            }
        }
    }
    void fixedUpdate() {
        Vector2 playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);

        Vector2 maxRenderBoundry = new Vector2(playerPosition.x * renderDistance, playerPosition.y * renderDistance);
        
        if (playerPosition.x > renderOffset.x * renderDistance) {
            LoadChunks(renderDistance, true);
        }
        if (playerPosition.y > renderOffset.y * renderDistance) {
            LoadChunks(renderDistance, false);
        }
    }

    void LoadChunks(int count, bool xory) {
        if (xory) {
            for (int y = 0; y < count; y++) {
                GameObject newChunk = (GameObject)Instantiate(chunkTemplate, new Vector3(chunks.GetLength(0)-1 * Chunk.chunkSize, transform.position.y, chunks.GetLength(1)-1 * Chunk.chunkSize), Quaternion.identity);
                
                newChunk.GetComponent<Chunk>().chunkOffsetX = chunks[chunks.GetLength(0)-1, 0].GetComponent<Chunk>().chunkOffsetX + 1;
                newChunk.GetComponent<Chunk>().chunkOffsetY = chunks[0, chunks.GetLength(1)-1].GetComponent<Chunk>().chunkOffsetY;

                chunks[chunks.GetLength(0) - 1, 0] = newChunk;
            }
        } else {
            for (int y = 0; y < count; y++) {
                GameObject newChunk = (GameObject)Instantiate(chunkTemplate, new Vector3(chunks.GetLength(0)-1 * Chunk.chunkSize, transform.position.y, chunks.GetLength(1)-1 * Chunk.chunkSize), Quaternion.identity);
                
                newChunk.GetComponent<Chunk>().chunkOffsetX = chunks[chunks.GetLength(0)-1, 0].GetComponent<Chunk>().chunkOffsetX;
                newChunk.GetComponent<Chunk>().chunkOffsetY = chunks[0, chunks.GetLength(1)-1].GetComponent<Chunk>().chunkOffsetY + 1;

                chunks[0, chunks.GetLength(1) - 1] = newChunk;
            }
        }
    }
}