using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
     private static GameManager _instance;
     public static GameManager Instance => _instance;


     [SerializeField] private GameObject levelManagerPrefab;
     private GameObject levelManager;

     private bool inLevel = false;

     private int level = 2;
     
     private void Awake()
     {
          if (_instance != null && _instance != this)
          {
               Destroy(this.gameObject);
          }
          else
          {
               _instance = this;
          }
     }

     private void Start()
     {
          if (levelManagerPrefab == null)
          {
               Debug.LogError("No level manager prefab assigned to GameManager script!");
          }

          Instantiate(levelManagerPrefab, this.transform, true);
          inLevel = true;
     }

     private void Update()
     {
          nextLevel();
          if (!inLevel)
          {
               Instantiate(levelManagerPrefab, this.transform, true);
               inLevel = true;
          }
     }

     //Combines all actions which happen at the end of a level
     private void nextLevel()
     {
          /*if (LevelManager.Instance.GetAction() == 0)
          {
               inLevel = false;
               level++;
          }*/
     }

     //returns the current level
     public int GetLevel()
     {
          return level;
     }
}
