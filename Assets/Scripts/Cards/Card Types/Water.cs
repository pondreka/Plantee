using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private CardBasic basicScript;

    // Start is called before the first frame update
    void Start()
    {
        basicScript = GetComponent<CardBasic>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Returns if a card can be played on a specific hex
    public bool IsPlayable(GameObject hex)
    {
        if (!hex.gameObject.GetComponent<HexInteractions>().IsDump())
        {
            int water = basicScript.CurActionValue;
        
            int hexWater = hex.gameObject.GetComponent<HexAttributes>().GetWater();
            if ((hexWater + water > 10 && water == 1) 
                || (hexWater + water > 11 && water == 2)
                || (hexWater + water > 12 && water == 3))
            {
                return false;
            }
            return true;
        }

        return false;
    }
    
    //Plays a card action
    public void CardAction(GameObject hex)
    {
        
        List<GameObject> hexes = LevelManager.Instance.GetHexes(basicScript.CurActionRange, hex);

        for (int i = 0; i < hexes.Count; i++)
        {
            hexes[i].GetComponent<HexAttributes>().SetWater(basicScript.CurActionValue);
        }
        
        CardManager.Instance.Discard(this.gameObject);
        
    }
}
