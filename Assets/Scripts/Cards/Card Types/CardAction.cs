using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CardAction : MonoBehaviour
{
    public CardScriptableAction card;
    private CardBasic basicScript;
    private int actionIndex;


    //TODO: Implement different action texts and functionalities that can be random selected
    //TODO: Implement actions changing other cards
    
    // Start is called before the first frame update
    private void Start()
    {
        basicScript = GetComponent<CardBasic>();
        
        basicScript.action.text = card.action;
        basicScript.SetActionRange(card.actionRange);
        basicScript.SetActionValue(card.actionValue);
        basicScript.SetCardRange(card.cardRange);

        actionIndex = card.actionIndex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //Returns if a card can be played on a specific hex
    public bool IsPlayable(GameObject hex)
    {
        return !hex.GetComponent<HexInteractions>().IsDump() && basicScript.CurCost <= LevelManager.Instance.GetAction();
    }

    //Plays a card action
    public void Action(GameObject hex)
    {
        List<GameObject> hexes = LevelManager.Instance.GetHexes(basicScript.CurActionRange, hex);
        List<GameObject> tools = CardManager.Instance.ToolCards();
        
        GameObject dump = null;
        foreach (var h in hexes.Where(h => h.GetComponent<HexInteractions>().IsDump()))
        {
            dump = h;
        }

        hexes.Remove(dump);

        switch (actionIndex)
        {
            case 0:
                foreach (var h in hexes)
                {
                    h.gameObject.GetComponent<HexAttributes>().SetWater(basicScript.CurActionValue);
                }
                break;
            case 1:
                hex.gameObject.GetComponent<HexAttributes>().SetToxicity(basicScript.CurActionValue);
                break;
            case 2:
                CardManager.Instance.DrawNewHand();
                break;
            case 3:
                foreach (var c in tools)
                {
                    c.GetComponent<CardBasic>().SetCost(-basicScript.CurActionValue);
                }
                break;
            case 4:
                foreach (var h in hexes.Where(h => h.GetComponent<HexInteractions>().HasPlant()))
                {
                    h.transform.GetChild(7).GetComponent<Plant>().SetStage(basicScript.CurActionValue);
                }
                break;
            case 5:
                Trash.SetActionValue(basicScript.CurActionValue);
                
                break;
        }
        CardManager.Instance.Discard(this.gameObject); 
    }
}
