using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexInteractions : MonoBehaviour
{
    private bool isClickable = false;
    private Outline outline;
    
    private int columnPos = -1;
    private int rowPos = -1;
    
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
    
    //In movement range or not
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
    
    public bool InRange(GameObject hexToProve, int range)
    {
        int column = hexToProve.gameObject.GetComponent<HexInteractions>().GetColumn();
        int row = hexToProve.gameObject.GetComponent<HexInteractions>().GetRow();

        int sumThisHex = columnPos + rowPos;
        int sumOtherHex = column + row;
        int dist = Mathf.Abs(sumOtherHex - sumThisHex);

        if (dist <= range && Mathf.Abs(column - columnPos) <= range && Mathf.Abs(row - rowPos) <= range)
        {
            return true;
        }
            

        return false;
    }
}
