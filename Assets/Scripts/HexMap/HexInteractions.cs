using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexInteractions : MonoBehaviour
{
    private bool isClickable = false;
    private Outline outline;
    
    private int columnPos = -1;
    private int rowPos = -1;

    private bool hasPlant = false;
    private bool notPlayable = false;
    
    //Dump
    [SerializeField] private GameObject dumpPrefab;
    private bool isDump = false;

    // Start is called before the first frame update
    void Start()
    {
        isClickable = false;
        outline = this.gameObject.GetComponent<Outline>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //Getter column position
    public int GetColumn()
    {
        return columnPos;
    }
    
    //Setter column position
    private void SetColumn(int column)
    {
        if (columnPos == -1)
        {
            columnPos = column;
        }
    }
    
    //Getter row position
    public int GetRow()
    {
        return rowPos;
    }
    
    //Setter row position
    private void SetRow(int row)
    {
        if (rowPos == -1)
        {
            rowPos = row;
        }
    }
    
    //Setter for Hex position
    public void SetPosition(int column, int row)
    {
        SetColumn(column);
        SetRow(row);
    }
    
    //A hex is only clickable if an object was selected before
    //that requires an interaction with the hex and the hex is in range
    public bool IsClickable()
    {
        return isClickable;
    }

    //Not clickable for movement
    public void NotClickable()
    {
        isClickable = false;
    }

    //Clickable for movement
    public void Clickable()
    {
        isClickable = true;
    }

    //A plant is currently on the hex
    public bool HasPlant()
    {
        return hasPlant;
    }

    //Changes plant status on hex
    public void Plant()
    {
        hasPlant = !hasPlant;
    }
    
    //Outline On
    public void OutlineOn()
    {
        outline.enabled = true;
    }

    //Outline off
    public void OutlineOff()
    {
        outline.enabled = false;
    }
    
    //Returns a boolean, if a hex is in range or not
    public bool InRange(GameObject hexToProve, int range)
    {
        Vector2 position = LevelManager.GetHexPosition(hexToProve);
        int column = (int) position[0];
        int row = (int) position[1];

        int sumThisHex = columnPos + rowPos;
        int sumOtherHex = column + row;
        int dist = Mathf.Abs(sumOtherHex - sumThisHex);

        if (dist <= range && Mathf.Abs(column - columnPos) <= range && Mathf.Abs(row - rowPos) <= range)
        {
            return true;
        }
            

        return false;
    }
    
    //Returns a boolean list, if a given position is in range or not
    //Marks all hex in range
    public List<List<bool>> HexInRange(int range)
    {
        notPlayable = false;
        List<List<bool>> inRange = new List<List<bool>>();

        List<List<GameObject>> map = LevelManager.Instance.GetHexMap();

        for (int c = 0; c < map.Count; c++)
        {
            List<bool> temp = new List<bool>();
            for (int r = 0; r < map[c].Count; r++)
            {
                
                if (range <= 0)
                {
                    map[c][r].gameObject.GetComponent<HexInteractions>().NotClickable();
                    temp.Add(false);
                }
                else
                {
                    if (InRange(map[c][r], range))
                    {
                        map[c][r].gameObject.GetComponent<HexInteractions>().Clickable();
                    }
                    else
                    {
                        map[c][r].gameObject.GetComponent<HexInteractions>().NotClickable();
                    }

                    temp.Add(InRange(map[c][r], range));

                }

                if (LevelManager.Instance.CardSelected() && !LevelManager.Instance.GetCurrentCard().GetComponent<CardBasic>().IsPlayable(map[c][r]))
                {
                    map[c][r].gameObject.GetComponent<HexInteractions>().NotClickable();
                    temp[r] = false;
                    if (map[c][r] == this.gameObject)
                    {
                        notPlayable = true;
                    }
                }
            }
            inRange.Add(temp);
        }

        //If the current hex can be selected or not depends on movement or interaction with the hex
        //No selection for Movement
        //Selection for Interaction
        if (range == 0 && !LevelManager.Instance.RobotSelected() && !notPlayable)
        {
            Clickable();
            inRange[columnPos][rowPos] = true;
        }
        else if (range > 0 && LevelManager.Instance.RobotSelected())
        {
            NotClickable();
            inRange[columnPos][rowPos] = false;
        }

        return inRange;
    }


    //Create a dump
    public GameObject DumpIt()
    {
        Vector3 position = this.transform.position;
        GameObject dump = Instantiate(dumpPrefab, position, Quaternion.Euler(-90, 0, 0), transform.parent);
        transform.SetParent(dump.transform);
        gameObject.SetActive(false);
        gameObject.GetComponentInParent<HexInteractions>().SetPosition(columnPos, rowPos);
        gameObject.GetComponentInParent<HexInteractions>().DumpTrue();

        return dump;
    }

    //Getter for dump hex
    public bool IsDump()
    {
        return isDump;
    }
    
    //Setter for the dump
    public void DumpTrue()
    {
        isDump = true;
    }
}
