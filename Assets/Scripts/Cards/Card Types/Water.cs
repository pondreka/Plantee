using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private int cardRange = 0;
    private int water = 1;
    private int actionRange = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //TODO: Implement water changing hex attributes
    
    public int GetRange()
    {
        return cardRange;
    }

    public void SetCardRange(int r)
    {
        if (r + cardRange > 0)
        {
            cardRange += r;
        }
        else
        {
            cardRange = 0;
        }
    }
    
    public void SetActionRange(int r)
    {
        if (r + actionRange > 0)
        {
            actionRange += r;
        }
        else
        {
            actionRange = 0;
        }
    }

    public void SetWater(int w)
    {
        if (water + w > 0)
        {
            if (water + w > 3)
            {
                water = 3;
            }
            else
            {
                water += w;
            }
        }
        else
        {
            water = 1;
        }
    }

    public bool IsPlayable(GameObject hex)
    {
        int hexWater = hex.gameObject.GetComponent<HexAttributes>().GetWater();
        if ((hexWater + water > 10 && water == 1) 
            || (hexWater + water > 11 && water == 2)
            || (hexWater + water > 12 && water == 3))
        {
            return false;
        }
        return true;
    }
    
    public void CardAction(GameObject hex)
    {
        List<GameObject> hexes = LevelManager.Instance.GetHexes(actionRange, hex);

        for (int i = 0; i < hexes.Count; i++)
        {
            hexes[i].GetComponent<HexAttributes>().SetWater(water);
        }
        
    }
}
