using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    private CardBasic cardScript;

    private static int actionValue = -1;
    // Start is called before the first frame update
    void Start()
    {
        cardScript = GetComponent<CardBasic>();
        if (actionValue == -1)
        {
            actionValue = cardScript.CurActionValue;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.Instance.IsRecyclable(cardScript.CurActionRange, actionValue))
        {
            if (cardScript.CurActionRange < 0)
            {
                cardScript.SetCardRange(0);
            }
            else
            {
                cardScript.SetCardRange(cardScript.CurActionRange);  
            }
        }
        else
        {
            cardScript.SetCardRange(-1);
        }
    }

    //Returns if a card can be played on a specific hex
    public bool IsPlayable(GameObject hex)
    {
        if (hex.gameObject.GetComponent<HexInteractions>().IsDump())
        {
            
            return true;
        }

        return false;
    }
    
    //Plays a card action
    public void CardAction(GameObject hex)
    {
        hex.gameObject.GetComponent<Dump>().AddTrash(this.gameObject);
        CardManager.Instance.StoreCard(this.gameObject);
    }
    
    //Setter for the action value
    public static void SetActionValue(int value)
    {
        if (actionValue + value < 10)
        {
            actionValue = 10;
        }
        else if (actionValue + value > 40)
        {
            actionValue = 40;
        }
        else
        {
            actionValue += value;
        }
    }
    
}
