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
    public int Index => index;

    //Card Attributes and attribute getter
    private int curCardRange;
    public int CurCardRange => curCardRange;
    
    private int curActionRange;
    public int CurActionRange => curActionRange;
 
    private int curActionValue;
    public int CurActionValue => curActionValue;

    private int curCost;
    public int CurCost => curCost;
    
    
    void Awake()
    {
        back = transform.GetChild(1).GetComponent<Canvas>();
        front = transform.GetChild(2).GetComponent<Canvas>();
        back.enabled = true;
        front.enabled = false;
        showFront = false;
        onHand = false;
        
        //Not changeable attributes
        name.text = card.name;
        action.text = card.action;
        foreground.sprite = card.foreground;
        index = card.index;
        
        //Changeable attributes
        SetCost(card.cost);
        SetCardRange(card.cardRange);
        SetActionRange(card.actionRange);
        SetActionValue(card.actionValue);
        
        
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
    private IEnumerator Flip()
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
                }
                else
                {
                    front.enabled = true;
                    back.enabled = false;
                }
            }

            yield return new WaitForFixedUpdate();
        }

        showFront = !showFront;
    }
    
    
    //Getter for card on hand or not
    public bool IsOnHand()
    {
        return onHand;
    }
    
    //Defines if the card is on hand or not
    public void OnHand()
    {
        onHand = !onHand;
    }

    
    //Attribute setter
    public void SetCardRange(int range)
    {
        curCardRange = range;
        if (range >= 0 && range < 10)
        {
            transform.GetChild(2).GetChild(6).GetComponent<Image>().enabled = true;
            cardRange.text = range.ToString();
        }
        else
        {
            transform.GetChild(2).GetChild(6).GetComponent<Image>().enabled = false;
            cardRange.text = "";
        }
    }

    public void SetActionRange(int range)
    {
        curActionRange = range;
        if (range >= 0 && range < 10)
        {
            transform.GetChild(2).GetChild(8).GetComponent<Image>().enabled = true;
            actionRange.text = range.ToString();
        }
        else
        {
            transform.GetChild(2).GetChild(8).GetComponent<Image>().enabled = false;
            actionRange.text = "";
        }
        
    }

    public void SetActionValue(int value)
    {
        curActionValue = value;
        actionValue.text = value.ToString();
        
    }

    public void SetCost(int c)
    {
        if (c + curCost < 0)
        {
            curCost = 0;
        }
        else
        {
            curCost += c;
        }
        
        cost.text = curCost.ToString();
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
                return toolScript.IsPlayable();
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
                trashScript.CardAction(hex);
                break;
            case 3:
                CardSeed seedScript = GetComponent<CardSeed>();
                seedScript.CardAction(hex);
                break;
            case 4:
                CardAction actionScript = GetComponent<CardAction>();
                actionScript.Action(hex);
                break;
            case 5:
                CardTool toolScript = GetComponent<CardTool>();
                toolScript.CardAction();
                break;
        }
    }
}


