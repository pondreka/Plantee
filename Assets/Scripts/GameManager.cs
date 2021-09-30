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
     private int level = 0;

     [SerializeField] private GameObject victory;
     [SerializeField] private GameObject instructions;
     [SerializeField] private GameObject menu;
     
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
          
     }

     private void Update()
     {
          
     }
     

     //returns the current level
     public int GetLevel()
     {
          return level;
     }

     //Starts the easy level with a click on the button
     public void StartEasyLevel()
     {
          if (inLevel) return;
          level = 2;
          levelManager = Instantiate(levelManagerPrefab, this.transform, true);
          inLevel = true;
          menu.SetActive(false);
     }
     
     //Starts the hard level with a click on the button
     public void StartHardLevel()
     {
          if (inLevel) return;
          level = 3;
          levelManager = Instantiate(levelManagerPrefab, this.transform, true);
          inLevel = true;
          menu.SetActive(false);
     }

     //Ends level by button press and returns to menu
     public void ReturnToMenu()
     {
          menu.SetActive(true);
          levelManager.SetActive(false);
          inLevel = false;
     }

     //Displays the victory logo
     public void Victory()
     {
          menu.SetActive(false);
          levelManager.SetActive(false);
          inLevel = false;
          victory.SetActive(true);
     }

     //Deactivates the victory logo after button press
     public void ReturnAfterVictory()
     {
          menu.SetActive(true);
          victory.SetActive(false);
     }

     public void Instructions()
     {
          menu.SetActive(false);
          instructions.SetActive(true);
     }

     //Deactivates the instructions after button press
     public void ReturnAfterInstructions()
     {
          menu.SetActive(true);
          instructions.SetActive(false);
     }
}
