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
     private HexMap mapScript;

     [SerializeField] private GameObject robotPrefab;
     private GameObject robot;
     private Movement robotScript;

     [SerializeField] private GameObject mamaBarPrefab;
     private GameObject manaBar;
     private Mana manaScript;
     

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
          
          if (robotPrefab == null)
          {
               Debug.LogError("No robot prefab assigned to GameManager script!");
          }
          
          if (robotPrefab == null)
          {
               Debug.LogError("No mana bar prefab assigned to GameManager script!");
          }
          
          mapManager = Instantiate(hexMapPrefab, this.transform, true);
          robot = Instantiate(robotPrefab, this.transform, true);
          manaBar = Instantiate(mamaBarPrefab, this.transform, true);
          manaScript = manaBar.gameObject.GetComponent<Mana>();
          robotScript = robot.gameObject.GetComponent<Movement>();
          mapScript = mapManager.gameObject.GetComponent<HexMap>();
     }

     // Update is called once per frame
     private void Update()
     {
          if (GetAction() == 0)
          {
               RoundEnd();
          }
          
     }

     //Global setter for the number of actions
     public void SetAction(int actions)
     {
          manaScript.SetActions(actions);
     }

     //Global getter for the number of actions
     public int GetAction()
     {
          return manaScript.GetActions();
     }
     
     //Deselects all Hex if no actions are left
     public void DeselectAllHex()
     {
          List<List<GameObject>> map = mapScript.GetAllHex();

          for (int c = 0; c < map.Count; c++)
          {
               for (int r = 0; r < map[c].Count; r++)
               {
                    Hex hexScript = map[c][r].gameObject.GetComponent<Hex>();
                    if (hexScript.IsClickable())
                    {
                         hexScript.NotClickable();
                         hexScript.OutlineOff();
                    }
                    
               }
          }
     }

     //Combines all actions happening at the end of a round
     private void RoundEnd()
     {
          DeselectAllHex();
     }
}
