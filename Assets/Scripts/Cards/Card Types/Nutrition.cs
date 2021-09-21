using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nutrition : MonoBehaviour
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
        
        int nutrition = basicScript.CurActionValue;
        
        int hexNutrition = hex.gameObject.GetComponent<HexAttributes>().GetNutrition();
        if ((hexNutrition + nutrition > 10 && nutrition  == 1) 
            || (hexNutrition + nutrition > 11 && nutrition == 2)
            || (hexNutrition + nutrition > 12 && nutrition == 3))
        {
            return false;
        }
        return true;
    }
    
    //Plays a card action
    public void CardAction(GameObject hex)
    {
        
        List<GameObject> hexes = LevelManager.Instance.GetHexes(basicScript.CurActionRange, hex);

        for (int i = 0; i < hexes.Count; i++)
        {
            hexes[i].GetComponent<HexAttributes>().SetNutrition(basicScript.CurActionValue);
        }
        
    }
}
