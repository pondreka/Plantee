using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    private int mapHeight;
    private int mapWidth;
    private int levelCount = 3;
    
    private void LevelGenerator()
    {
        mapHeight = Random.Range(1 * levelCount, 2 * levelCount);
        mapWidth = Random.Range(1 * levelCount, 2 * levelCount);
        levelCount++;
    }

    public Vector2 GetMapSize()
    {
        LevelGenerator();
        return new Vector2(mapHeight, mapWidth);
    }
}
