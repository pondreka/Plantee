using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
     public static LevelManager Instance => _instance;

     [SerializeField] private GameObject hexMapPrefab;
     private GameObject mapManager;
     private HexMap mapScript;

     [SerializeField] private GameObject robotPrefab;
     private GameObject robot;
     private Movement robotScript;

     [SerializeField] private GameObject manaBarPrefab;
     private GameObject manaBar;
     private Mana manaScript;

     [SerializeField] private GameObject mouseManagerPrefab;
     private GameObject mouseManager;
     private MouseManager mouseManagerScript;

     [SerializeField] private GameObject cardManagerPrefab;
     private GameObject cardManager;

     private bool roundEnd = false;
     private int roundCount = 0;


     private void Awake()
     {
          if (_instance != null && _instance != this)
          {
               Destroy(_instance.gameObject);
               _instance = this;
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
               Debug.LogError("No hex map prefab assigned to LevelManager script!");
          }
          
          if (robotPrefab == null)
          {
               Debug.LogError("No robot prefab assigned to LevelManager script!");
          }
          
          if (manaBarPrefab == null)
          {
               Debug.LogError("No mana bar prefab assigned to LevelManager script!");
          }
          
          if (mouseManagerPrefab == null)
          {
               Debug.LogError("No mouse manager prefab assigned to LevelManager script!");
          }
          
          if (cardManagerPrefab == null)
          {
               Debug.LogError("No card manager prefab assigned to LevelManager script!");
          }

          mapManager = Instantiate(hexMapPrefab, this.transform, true);
          robot = Instantiate(robotPrefab, this.transform, true);
          manaBar = Instantiate(manaBarPrefab, this.transform, true);
          mouseManager = Instantiate(mouseManagerPrefab,this.transform, true);
          cardManager = Instantiate(cardManagerPrefab,this.transform, true);
          manaScript = manaBar.gameObject.GetComponent<Mana>();
          robotScript = robot.gameObject.GetComponent<Movement>();
          mapScript = mapManager.gameObject.GetComponent<HexMap>();
          mouseManagerScript = mouseManager.gameObject.GetComponent<MouseManager>();

          roundCount = 0;
     }

     // Update is called once per frame
     private void Update()
     {
          if (GetAction() == 0 && !roundEnd)
          {
               RoundEnd();
          }

     }

     //---------- Round Functionality -------------------
     
     //Combines all actions happening at the end of a round
     private void RoundEnd()
     {
          roundCount++;
          StartCoroutine(RoundEnding());
          
          
     }

     //Starts the things happening at the end of the round
     IEnumerator RoundEnding()
     {
          roundEnd = true;
          
          SetRange(-1);
          yield return new WaitWhile(robotScript.IsMoving);
          yield return new WaitForSeconds(2f);
          
          RoundConsequence(roundCount % 10);
          manaScript.SetActions(manaScript.GetMaxActions());
          CardManager.Instance.DrawHand();
          
          yield return new WaitForSeconds(1f);
          roundEnd = false;
     }
     
     //Getter for end of a round
     public bool EndOfRound()
     {
          return roundEnd;
     }
     
     //Round consequences
     private void RoundConsequence(int round)
     {
          List<List<GameObject>> map = GetHexMap();
          switch (round)
          {
               case 0:
                    CardManager.Instance.NewCard(1);
                    break;
               case 1:
                    for (int c = 0; c < map.Count; c++)
                    {
                         for (int r = 0; r < map[c].Count; r++)
                         {
                              if (!map[c][r].GetComponent<HexInteractions>().IsDump())
                              {
                                   map[c][r].GetComponent<HexAttributes>().SetTrash(1);
                              }
                         }
                    }
                    break;
               case 2:
                    for (int c = 0; c < map.Count; c++)
                    {
                         for (int r = 0; r < map[c].Count; r++)
                         {
                              if (!map[c][r].GetComponent<HexInteractions>().IsDump())
                              {
                                   map[c][r].GetComponent<HexAttributes>().SetNutrition(-1);
                              }
                         }
                    }
                    break;
               case 3:
                    CardManager.Instance.NewCard(0);
                    break;
               case 4:
                    for (int c = 0; c < map.Count; c++)
                    {
                         for (int r = 0; r < map[c].Count; r++)
                         {
                              if (!map[c][r].GetComponent<HexInteractions>().IsDump())
                              {
                                   map[c][r].GetComponent<HexAttributes>().SetWater(-1);
                              }
                         }
                    }
                    break;
               case 5:
                    CardManager.Instance.NewCard(1);
                    break;
               case 6:
                    for (int c = 0; c < map.Count; c++)
                    {
                         for (int r = 0; r < map[c].Count; r++)
                         {
                              if (!map[c][r].GetComponent<HexInteractions>().IsDump())
                              {
                                   map[c][r].GetComponent<HexAttributes>().SetToxicity(-1);
                              }
                         }
                    }
                    break;
               case 7:
                    CardManager.Instance.NewCard(0);
                    break;
               case 8:
                    for (int c = 0; c < map.Count; c++)
                    {
                         for (int r = 0; r < map[c].Count; r++)
                         {
                              if (!map[c][r].GetComponent<HexInteractions>().IsDump())
                              {
                                   map[c][r].GetComponent<HexAttributes>().SetTrash(1);
                              }
                         }
                    }
                    break;
               case 9:
                    CardManager.Instance.NewCard(3);
                    break;
          }
          
          //Plant evolution
          for (int c = 0; c < map.Count; c++)
          {
               for (int r = 0; r < map[c].Count; r++)
               {
                    if (map[c][r].GetComponent<HexInteractions>().HasPlant())
                    {
                         map[c][r].transform.GetChild(7).GetComponent<Plant>().RoundCount();
                    }
               }
          }
     }


     //------------ HEX FUNCTIONALITY ------------------
     
     //Setter fot the position of a hex tile
     public void SetHexPosition(GameObject hex, int column, int row)
     {
          hex.gameObject.GetComponent<HexInteractions>().SetPosition(column, row);
     }

     //Getter for the position of a hex tile (column, row)
     public Vector2 GetHexPosition(GameObject hex)
     {
          int column = hex.gameObject.GetComponent<HexInteractions>().GetColumn();
          int row = hex.gameObject.GetComponent<HexInteractions>().GetRow();
          
          return new Vector2(column,row);
     }

     //Setter for all attributes of a hex tile
     public void SetHexAttributes(GameObject hex, int water, int nutrition, int toxicity, int trash)
     {
          hex.gameObject.GetComponent<HexAttributes>().SetAllAttributes(water, nutrition, toxicity, trash);
     }
     
     //Trash can be added to dump
     public bool IsRecyclable(int range, int trashLevel)
     {

          if (range < 0 && GetHexMap()[1][1].gameObject == GetCurrentHex().gameObject
                        && GetHexMap()[1][1].gameObject.GetComponent<Dump>().GetTrashLevel() < trashLevel)
          {
               return true;
          }
          if (range > 0 && GetCurrentHex().GetComponent<HexInteractions>().InRange(GetHexMap()[1][1], range) 
              && GetHexMap()[1][1].GetComponent<Dump>().GetTrashLevel() < trashLevel)
          {
               return true;
          }

          return false;
     }


     //--------------- HEX MAP FUNCTIONALITY ----------------
     
     //Global getter for the hex map
     public List<List<GameObject>> GetHexMap()
     {
          return mapScript.GetAllHex();
     }

     //Returns a list of all hex in range from the current hex
     //turns the  clickable attribute accordingly
     public List<List<bool>> GetHexInRange(int range, GameObject hex)
     {
          return hex.gameObject.GetComponent<HexInteractions>().HexInRange(range);
     }
     
     //Selection of hexes
     public void SelectHexInRange(int range)
     {
          List<List<bool>> inRange = GetHexInRange(range, GetCurrentHex());
          List<List<GameObject>> map = GetHexMap();

          for (int c = 0; c < map.Count; c++)
          {
               for (int r = 0; r < map[c].Count; r++)
               {
                    if (inRange[c][r])
                    {
                         map[c][r].gameObject.GetComponent<HexInteractions>().OutlineOn();
                    }
                    else
                    {
                         map[c][r].gameObject.GetComponent<HexInteractions>().OutlineOff();
                    }
               }
          }
     }
     
     //Returning Hexes in range
     public List<GameObject> GetHexes(int range, GameObject hex)
     {
          List<List<bool>> inRange = GetHexInRange(range, hex);
          List<List<GameObject>> map = GetHexMap();
          List<GameObject> hexes = new List<GameObject>();

          for (int c = 0; c < map.Count; c++)
          {
               for (int r = 0; r < map[c].Count; r++)
               {
                    if (inRange[c][r])
                    {
                         hexes.Add(map[c][r]);
                    }
               }
          }

          return hexes;
     }

     //Global getter for the neighbors of the current hex
     public List<GameObject> GetHexNeighbors(GameObject hex)
     {
          return mapScript.HexNeighbors(hex);
     }


     //-------------- PLAYER FUNCTIONALITY -----------------
     
     //Global getter for the current Hex
     public GameObject GetCurrentHex()
     {
          return robotScript.GetCurrentHex();
     }
     
     //Getter for the position of a hex tile (column, row)
     public Vector2 GetCurrentHexPosition()
     {
          int column = GetCurrentHex().gameObject.GetComponent<HexInteractions>().GetColumn();
          int row = GetCurrentHex().gameObject.GetComponent<HexInteractions>().GetRow();
          
          return new Vector2(column,row);
     }

     //robot movement
     public void MoveToLocation(Vector3 targetPoint)
     {
          robotScript.Move(targetPoint);
     }

     //Getter for robot in movement
     public bool IsMoving()
     {
          return robotScript.IsMoving();
     }


     //------------- MANA FUNCTIONALITY -------------------
     
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


     //------------- MOUSE MANAGER FUNCTIONALITY ------------
     
     //Global getter for the selection of the robot boolean
     //Movement should only be possible if the robot was selected
     public bool RobotSelected()
     {
          return mouseManagerScript.RobotSelected();
     }

     //Global getter for the selection of the card boolean
     public bool CardSelected()
     {
          return mouseManagerScript.CardSelectd();
     }

     //Global setter for the range of hexTiles in mouseManager
     public void SetRange(int range)
     {
          mouseManagerScript.SetRange(range);
     }

     //Global getter for the current card
     public GameObject GetCurrentCard()
     {
          return mouseManagerScript.GetCurrentCard();
     }
     
}
