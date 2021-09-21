using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardTool : MonoBehaviour
{
    public CardScriptableTool card;
    private CardBasic basicScript;
    
    
    //TODO: Implement tool card positions
    //TODO: Implement tools changing values of other cards
    
    // Start is called before the first frame update
    void Start()
    {
        basicScript = GetComponent<CardBasic>();
        
        basicScript.action.text = card.action;
        basicScript.name.text = card.name;
        basicScript.SetActionValue(card.actionValue);
        basicScript.SetActionRange(card.actionRange);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //Returns if a card can be played on a specific hex
    public bool IsPlayable(GameObject hex)
    {
        return true;
    }

    //Plays a card action
    public void CardAction()
    {
        
    }
}
