using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEditor.Experimental;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.AI;

public class HexMap : MonoBehaviour
{
    [SerializeField] private GameObject hexTilePrefab;

    [Header("Hex tile properties")] 
    public bool edgeUp = false;
    public float tileRadius = 1f;
    
    private float width;
    private float height;

    private List<List<GameObject>> map = new List<List<GameObject>>();

    public NavMeshSurface surface;
    

    // Start is called before the first frame update
    void Start()
    {
        if (hexTilePrefab == null)
        {
            Debug.LogError("No hex prefab assigned to HexMap script!");
        }
        if (surface == null)
        {
            Debug.LogError("No surface assigned to HexMap script!");
        }
        
        Level level = new Level();
        Vector2 mapSize = level.GetMapSize();
        height = mapSize.x;
        width = mapSize.y;
        InitializeMap();
        surface.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    //Generates the map according to width and height and creates a List of HexTiles
    private void InitializeMap()
    {
        for (int column = 0; column < width; column++)
        {
            List<GameObject> cmap = new List<GameObject>();
            for (int row = 0; row < height; row++)
            {
                HexPosition tile = new HexPosition(column, row, tileRadius, edgeUp);
                if (edgeUp)
                {
                    GameObject hexTile = Instantiate(hexTilePrefab, tile.GetPosition(), Quaternion.Euler(-90,90,0), this.transform);
                    hexTile.name = "HexTile_" + column + "_" + row;
                    
                    hexTile.gameObject.GetComponent<Hex>().SetAllAttributes(Random.Range(0,11),Random.Range(0,11), Random.Range(0,11), Random.Range(0,11));
                    hexTile.gameObject.GetComponent<Hex>().SetPosition(column, row);
                    
                    cmap.Add(hexTile);

                }
                else
                {
                    GameObject hexTile = Instantiate(hexTilePrefab, tile.GetPosition(), Quaternion.Euler(-90,0,0), this.transform);
                    hexTile.name = "HexTile_" + column + "_" + row;
                    
                    hexTile.gameObject.GetComponent<Hex>().SetAllAttributes(Random.Range(0,11),Random.Range(0,11), Random.Range(0,11), Random.Range(0,11));
                    hexTile.gameObject.GetComponent<Hex>().SetPosition(column, row);
                    
                    cmap.Add(hexTile);
                }
            }
            map.Add(cmap);
        }
    }

    //Getter for the HexTile list
    public List<List<GameObject>> GetAllHex()
    {
        return map;
    }
    
    //Generates a list with all HexTile neighbors
    public List<GameObject> HexNeighbors(GameObject hex)
    {
        int column = hex.gameObject.GetComponent<Hex>().GetColumn();
        int row = hex.gameObject.GetComponent<Hex>().GetRow();
        
        List<GameObject> neighbors = new List<GameObject>();

        if (row - 1 >= 0)
        {
            neighbors.Add(map[column][row - 1]);
        }

        if (column - 1 >= 0)
        {
            neighbors.Add(map[column - 1][row]);

            if (row + 1 < map[column - 1].Count)
            {
                neighbors.Add(map[column - 1][row + 1]);
            }
        }

        if (row + 1 < map[column].Count)
        {
            neighbors.Add(map[column][row + 1]);
        }

        if (column + 1 < map.Count)
        {
            neighbors.Add(map[column + 1][row]);

            if (row - 1 >= 0)
            {
                neighbors.Add(map[column + 1][row - 1]);
            }
        }
        
        return neighbors;
    }
}
