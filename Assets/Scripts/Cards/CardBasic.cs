using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardBasic : MonoBehaviour
{
    //Card display
    public CardScriptable card;
    public new Text name;
    public Text action;
    public Text cost;
    public Text cardRange;
    public Image foreground;
    public Text actionValue;
    public Text actionRange;
    
    //Card flipping
    private Canvas back;
    private Canvas front;
    private bool showFront;
    [SerializeField] private AnimationCurve scaleCurve;
    [SerializeField] private float duration = 0.5f;
    
    //Card functionality
    private bool onHand;
    private int index;

    //Card Attributes and attribute getter
    private int curCardRange;
    public int CurCardRange => curCardRange;
    
    private int curActionRange;
    public int CurActionRange => curActionRange;
 
    private int curActionValue;
    public int CurActionValue => curActionValue;

    private int curCost;
    public int CurCost => curCost;
    
    
    //TODO: Implement random selection of text for tools and action cards
    
    void Awake()
    {
        back = transform.GetChild(1).GetComponent<Canvas>();
        front = transform.GetChild(2).GetComponent<Canvas>();
        back.enabled = true;
        front.enabled = false;
        showFront = false;
        onHand = false;
        
        name.text = card.name;
        action.text = card.action;
        cost.text = card.cost.ToString();
        curCost = card.cost;
        cardRange.text = card.cardRange.ToString();
        curCardRange = card.cardRange;
        foreground.sprite = card.foreground;
        actionValue.text = card.actionValue.ToString();
        curActionValue = card.actionValue;
        actionRange.text = card.actionRange.ToString();
        curActionRange = card.actionRange;

        index = card.index;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //Starting the coroutine for card flipping
    public void FlipCard()
    {
        StopCoroutine(Flip());
        StartCoroutine(Flip());
    }

    //Coroutine for the flipping of a card
    //Defines if back or front is shown
    IEnumerator Flip()
    {
        float time = 0f;

        while (time <= 1f)
        {
            float scale = scaleCurve.Evaluate(time);
            time += Time.deltaTime / duration;
            
            Vector3 localScale = transform.localScale;
            localScale.x = scale;
            transform.localScale = localScale;
            
            if (time >= 0.5f)
            {
                if (showFront)
                {
                    front.enabled = false;
                    back.enabled = true;
                    showFront = false;
                }
                else
                {
                    front.enabled = true;
                    back.enabled = false;
                    showFront = true;
                }
            }

            yield return new WaitForFixedUpdate();
        }
    }
    
    //Getter for card on hand or not
    public bool IsOnHand()
    {
        return onHand;
    }
    
    //Defines if the card is on hand or not
    public void OnHand()
    {
        if (onHand)
        {
            onHand = false;
        }
        else
        {
            onHand = true;
        }
    }

    
    //Attribute setter
    public void SetCardRange(int range)
    {
        curCardRange = range;
        cardRange.text = range.ToString();
    }

    public void SetActionRange(int range)
    {
        curActionRange = range;
        actionRange.text = range.ToString();
    }

    public void SetActionValue(int value)
    {
        if (value < 7 && value >= 0)
        {
            curActionValue = value;
            actionValue.text = value.ToString();
        }
    }

    public void SetCost(int c)
    {
        curCost = c;
        cost.text = c.ToString();
    }
    
    
    //Returns a boolean, if the card can be played on a hex or not
    public bool IsPlayable(GameObject hexToProve)
    {
        
        switch (index)
        {
            case 0:
                Water waterScript = GetComponent<Water>();
                return waterScript.IsPlayable(hexToProve);
            case 1:
                Nutrition nutritionScript = GetComponent<Nutrition>();
                return nutritionScript.IsPlayable(hexToProve);
            case 2:
                Trash trashScript = GetComponent<Trash>();
                return trashScript.IsPlayable(hexToProve);
            case 3:
                CardSeed seedScript = GetComponent<CardSeed>();
                return seedScript.IsPlayable(hexToProve);
            case 4:
                CardAction actionScript = GetComponent<CardAction>();
                return actionScript.IsPlayable(hexToProve);
            case 5:
                CardTool toolScript = GetComponent<CardTool>();
                return toolScript.IsPlayable(hexToProve);
        }

        return true;
    }

    
    //Implements the action of the different card types
    public void CardAction(GameObject hex)
    {
        switch (index)
        {
            case 0:
                Water waterScript = GetComponent<Water>();
                waterScript.CardAction(hex);
                break;
            case 1:
                Nutrition nutritionScript = GetComponent<Nutrition>();
                nutritionScript.CardAction(hex);
                break;
            case 2:
                Trash trashScript = GetComponent<Trash>();
                trashScript.CardAction();
                break;
            case 3:
                CardSeed seedScript = GetComponent<CardSeed>();
                seedScript.CardAction(hex);
                break;
            case 4:
                CardAction actionScript = GetComponent<CardAction>();
                actionScript.Action();
                break;
            case 5:
                CardTool toolScript = GetComponent<CardTool>();
                toolScript.CardAction();
                break;
        }
    }
}


