using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
     private static GameManager _instance;
     public static GameManager Instance => _instance;

     [SerializeField] private GameObject hexMapPrefab;
     private GameObject mapManager;

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
          if (hexMapPrefab == null)
          {
               Debug.LogError("No hex map prefab assigned to GameManager script!");
          }
          
          mapManager = Instantiate(hexMapPrefab, this.transform, true);
     }
}
