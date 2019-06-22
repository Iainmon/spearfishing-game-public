using UnityEngine;
using System.Collections;
public class TerrainObjectPlacer : MonoBehaviour
{
    public Terrain terrain; // add current terrain
    public GameObject objectToPlace; // this object will be placed on terrain
    public float objectRotationOffset = -90;
    public int numberOfObjects; // number of how many objects will be created
    public int posMin; // minimum y position
    public int posMax; // maximum x position
    public bool posMaxIsTerrainHeight; // the maximum height is the terrain height
    private int numberOfPlacedObjects; // number of the plaed objects
    private int terrainWidth; // terrain size x axis
    private int terrainLength; // terrain size z axis
    private int terrainPosX; // terrain position x axis
    private int terrainPosZ; // terrain position z axis
                             // Use this for initialization
    void Start()
    {
        terrainWidth = (int)terrain.terrainData.size.x; // get terrain size x
        terrainLength = (int)terrain.terrainData.size.z; // get terrain size z
        terrainPosX = (int)terrain.transform.position.x; // get terrain position x
        terrainPosZ = (int)terrain.transform.position.z; // get terrain position z
        if (posMaxIsTerrainHeight == true)
        {
            posMax = (int)terrain.terrainData.size.y;
        }
    }
    // Update is called once per frame
    void Update()
    {
        // numberOfPlacedObjects is smaller than numberOfObjects
        if (numberOfPlacedObjects < numberOfObjects)
        {
            PlaceObject(); // call function placeObject
        }
    }
    // Create objects on the terrain with random positions
    void PlaceObject()
    {
        int posx = Random.Range(terrainPosX, terrainPosX + terrainWidth); // generate random x position
        int posz = Random.Range(terrainPosZ, terrainPosZ + terrainLength); // generate random z position
        float posy = Terrain.activeTerrain.SampleHeight(new Vector3(posx, 0, posz)); // get the terrain height at the random position
        if (posy < posMax && posy > posMin)
        {
            PlaceCoralHead(posx, posy, posz);
            numberOfPlacedObjects++;
        }
        else
        {
            PlaceObject();
        }
    }

    void PlaceCoralHead(float xPos, float yPos, int zPos) {
        int coralHeadDensity = Random.Range(1, 6);
        for (int i = 0; i < coralHeadDensity; i++) {

            int coralOffsetX = Random.Range(-10, 10);
            int coralOffsetZ = Random.Range(-10, 10);

            float coralSize = Random.Range(0.1f, 1.2f);
            //coralSize = Mathf.Lerp(0.1f, 1.2f, Mathf.InverseLerp(coralSize, 1, 100));

            GameObject newObject = (GameObject)Instantiate(objectToPlace, new Vector3(xPos + coralOffsetX, yPos + terrain.transform.position.y, zPos + coralOffsetZ), Quaternion.identity); // create object
            
            newObject.transform.localScale = new Vector3(coralSize, coralSize, coralSize);

            if (objectRotationOffset != 0) {
                newObject.transform.eulerAngles = new Vector3(objectRotationOffset, 0, 0);
            }
        }
    }
}