using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class HexMap : MonoBehaviour
{
    public GameObject hexTilePrefab;

    [Header("Hex tile properties")] 
    public bool edgeUp = false;
    public float tileRadius = 1f;

    [Header("Grid size")] 
    public int width;
    public int height;

    private List<List<GameObject>> map = new List<List<GameObject>>();
    

    // Start is called before the first frame update
    void Start()
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
                    hexTile.name = "Hextile_" + column + "_" + row;

                    cmap.Append(hexTile);

                }
                else
                {
                    GameObject hexTile = Instantiate(hexTilePrefab, tile.GetPosition(), Quaternion.Euler(-90,0,0), this.transform);
                    hexTile.name = "Hextile_" + column + "_" + row;
                    
                    cmap.Append(hexTile);
                }
            }

            map.Append(cmap);
        }
    }
    
    
    
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
