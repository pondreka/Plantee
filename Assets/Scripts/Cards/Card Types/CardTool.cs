using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardTool : MonoBehaviour
{
    public CardScriptableTool card;
    private CardBasic basicScript;
    private bool onField = false;
    private bool changed = false;
    private static bool playable = true;
    private int toolIndex;
    private List<GameObject> waterValue;
    private List<GameObject> waterRange;
    private List<GameObject> nutritions;
    private List<GameObject> seeds;
    private List<GameObject> trashs;
    
    
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
        toolIndex = card.toolIndex;
    }

    // Update is called once per frame
    private void Update()
    {
        if (onField)
        {
            if (changed) return;
            
            switch (toolIndex)
            {
                case 0:
                    waterValue = CardManager.Instance.WaterCards();
                    foreach (var c in waterValue)
                    {
                        c.GetComponent<CardBasic>().SetActionValue(basicScript.CurActionValue);
                    }
                    break;
                case 1:
                    waterRange = CardManager.Instance.WaterCards();
                    foreach (var c in waterRange)
                    {
                        c.GetComponent<CardBasic>().SetCardRange(basicScript.CurActionRange);
                    }
                    break;
                case 2:
                    nutritions = CardManager.Instance.NutritionCards();
                    foreach (var c in nutritions)
                    {
                        c.GetComponent<CardBasic>().SetActionValue(basicScript.CurActionValue);
                    }
                    break;
                case 3:
                    trashs = CardManager.Instance.TrashCards();
                    foreach (var c in trashs)
                    {
                        c.GetComponent<CardBasic>().SetActionRange(basicScript.CurActionRange);
                    }
                    break;
                case 4:
                    seeds = CardManager.Instance.SeedCards();
                    foreach (var c in seeds)
                    {
                        c.GetComponent<CardBasic>().SetCardRange(basicScript.CurActionRange);
                    }

                    break;
                    
            }

            changed = true;
        }
        else
        {
            if (!changed) return;
            
            switch (toolIndex)
            {
                case 0:
                    foreach (var c in waterValue)
                    {
                        c.GetComponent<CardBasic>().SetActionValue(1);
                    }
                    break;
                case 1:
                    foreach (var c in waterRange)
                    {
                        c.GetComponent<CardBasic>().SetCardRange(0);
                    }
                    break;
                case 2:
                    foreach (var c in nutritions)
                    {
                        c.GetComponent<CardBasic>().SetActionValue(1);
                    }
                    break;
                case 3:
                    foreach (var c in trashs)
                    {
                        c.GetComponent<CardBasic>().SetCardRange(-1);
                    }
                    break;
                case 4:
                    foreach (var c in seeds)
                    {
                        c.GetComponent<CardBasic>().SetCardRange(0);
                    }

                    break;
                    
            }
            changed = false;
        }
    }
    
    //Returns if a card can be played on a specific hex
    public bool IsPlayable()
    {
        return basicScript.CurCost <= LevelManager.Instance.GetAction() && playable;
    }

    //Plays a card action
    public void CardAction()
    {
        CardManager.Instance.PlayToolCard(this.gameObject);
        
    }

    //Is needed because only three tools can be played
    public static void SetPlayable(int count)
    {
        playable = count < 3;
    }

    //Changes the state of a tool card if it is played
    public void SetOnField()
    {
        onField = !onField;
    }

    //Returns if a tool card was played
    public bool OnField()
    {
        return onField;
    }
}
