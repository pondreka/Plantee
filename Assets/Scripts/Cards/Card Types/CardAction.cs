using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardAction : MonoBehaviour
{
    public CardScriptableAction card;
    private CardBasic basicScript;
    
    
    //TODO: Implement different action texts and functionalities that can be random selected
    //TODO: Implement actions changing other cards
    
    // Start is called before the first frame update
    void Start()
    {
        basicScript = GetComponent<CardBasic>();
        
        basicScript.action.text = card.action;
        basicScript.SetActionRange(card.actionRange);
        basicScript.SetActionValue(card.actionValue);
        basicScript.SetCardRange(card.cardRange);
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
    public void Action()
    {
        CardManager.Instance.Discard(this.gameObject);
    }
}
