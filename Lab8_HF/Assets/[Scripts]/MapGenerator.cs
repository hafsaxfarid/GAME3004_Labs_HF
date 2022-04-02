using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("World Properties")]
    [Range(8, 64)]
    public int width = 8;

    [Range(8, 64)]
    public int height = 8;
    
    [Range(8, 64)]
    public int depth = 8;

    [Header("Scaling Values")]
    [Range(8, 64)]
    
    public float min = 16f;
    [Range(8, 64)]
    public float max = 24f;

    [Header("Tile Properties")]
    public Transform tileParent;
    public GameObject threeDTile;

    [Header("Grid")]
    public List<GameObject> grid;

    private int startWidth;
    private int startHeight;
    private int startDepth;
    private float startMin;
    private float startMax;

    void Start()
    {
        Generate();
    }
    
    void Update()
    {
        if(width != startWidth || height != startHeight || depth != startDepth || min != startMin || max != startMax)
        {
            Generate();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            Generate();
        }
    }

    private void Generate()
    {
        Initialize();
        Reset();
        Regenerate();
    }

    private void Initialize()
    {
        startWidth = width;
        startHeight = height;
        startDepth = depth;
        startMin = min;
        startMax = max;
    }

    private void Regenerate()
    {
        // world generation happens here
        float randomScale = Random.Range(min, max);

        float offsetX = Random.Range(-1024f, 1024f);

        float offsetZ = Random.Range(-1024f, 1024f);

        for (int y = 0; y < height; y++)
        {
            for (int z = 0; z < depth; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    var perlinValue = Mathf.PerlinNoise( (x + offsetX)/randomScale, (z + offsetZ) / randomScale) * depth * 0.5f ;
                    //Debug.Log("Y Val: " + y + " Perlin Value: " + perlinValue);

                    if(y < perlinValue)
                    {
                        // instantiate 3D Tile
                        var tile = Instantiate(threeDTile, new Vector3(x, y, z), Quaternion.identity);
                        tile.transform.SetParent(tileParent);
                        grid.Add(tile);
                    }

                } // x - width
            
            } // z - depth
        
        } // y - height
    } // end of regenerate function

    private void Reset()
    {
        foreach(var tile in grid)
        {
            Destroy(tile);
        }
        grid.Clear();   
    }
}
