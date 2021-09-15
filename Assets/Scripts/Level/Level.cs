using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    private int mapHeight;
    private int mapWidth;

    private void LevelGenerator()
    {
        int levelCount = GameManager.Instance.GetLevel();
        mapHeight = Random.Range(levelCount, Mathf.Min(2 * levelCount, 5));
        mapWidth = Random.Range(Mathf.Min(levelCount * levelCount, 5), Mathf.Min(3 * levelCount, 7));
    }

    public Vector2 GetMapSize()
    {
        LevelGenerator();
        return new Vector2(mapHeight, mapWidth);
    }
}
