using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    private CardBasic cardScript;
    // Start is called before the first frame update
    void Start()
    {
        cardScript = GetComponent<CardBasic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.Instance.IsRecyclable(cardScript.CurActionRange, cardScript.CurActionValue))
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

    public bool IsPlayable(GameObject hex)
    {
        if (hex.gameObject.GetComponent<HexInteractions>().IsDump())
        {
            
            return true;
        }

        return false;
    }
    
    public void CardAction(GameObject hex)
    {
        hex.gameObject.GetComponent<Dump>().AddTrash(this.gameObject);
        CardManager.Instance.StoreCard(this.gameObject);
    }
}
