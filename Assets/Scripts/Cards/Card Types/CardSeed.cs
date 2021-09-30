using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSeed : MonoBehaviour
{
    //Card display
    public CardScriptableSeed card;
    public Text waterValue;
    public Text nutritionValue;
    public Text toxicityValue;
    public Text trashValue;

    //Card functionality
    private CardBasic basicScript;
    [SerializeField] private GameObject plantPrefab;
    private int water;
    private int nutrition;
    private int toxicity;
    private int trash;
    private int evolutionTime;
    private int actionIndex;
    

    // Start is called before the first frame update
    private void Awake()
    {
        basicScript = GetComponent<CardBasic>();
        
        waterValue.text = card.waterValue.ToString();
        nutritionValue.text = card.nutritionValue.ToString();
        toxicityValue.text = card.toxicityValue.ToString();
        trashValue.text = card.trashValue.ToString();
        water = card.waterValue;
        nutrition = card.nutritionValue;
        toxicity = card.toxicityValue;
        trash = card.trashValue;
        evolutionTime = card.evolutionTime;
        actionIndex = card.actionIndex;
        
        basicScript.name.text = card.name;
        basicScript.action.text = card.action;
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
        if (hex.gameObject.GetComponent<HexInteractions>().IsDump()) return false;
        
        HexAttributes hexAttributesScript = hex.gameObject.GetComponent<HexAttributes>();
        
        if (hex.gameObject.GetComponent<HexInteractions>().HasPlant())
        {
            return false;
        }

        return water <= hexAttributesScript.GetWater() && nutrition <= hexAttributesScript.GetNutrition() && 10 - toxicity <= hexAttributesScript.GetToxicity() && trash >= hexAttributesScript.GetTrash();
    }
    
    //Plays a card action
    public void CardAction(GameObject hex)
    {
        if (hex.gameObject.GetComponent<HexInteractions>().HasPlant()) return;
        
        GameObject plant = Instantiate(plantPrefab, hex.transform, false);
        plant.gameObject.transform.localPosition = new Vector3(-0.5f,0.1f,0.2f);
        plant.gameObject.GetComponent<Plant>().SetInstance(water, nutrition, toxicity, trash, 
            basicScript.CurActionRange, evolutionTime, basicScript.CurActionValue, actionIndex, this.gameObject);
        CardManager.Instance.StoreCard(this.gameObject);
        hex.gameObject.GetComponent<HexInteractions>().Plant();

    }
}
