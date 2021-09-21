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

    //TODO: Implement plant objects and script
    //TODO: Implement Seeds creating plant objects depending on hex attributes    

    // Start is called before the first frame update
    void Start()
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
        
        basicScript.name.text = card.name;
        basicScript.action.text = card.action;
        basicScript.SetActionValue(card.spreading);
    }

    // Update is called once per frame
    void Update()
    {
    }
   
    //Returns if a card can be played on a specific hex
    public bool IsPlayable(GameObject hex)
    {
        /*
        if (hex.gameObject.GetComponent<HexInteractions>().HasPlant())
        {
            return false;
        }*/
        return true;
    }
    
    //Plays a card action
    public void CardAction(GameObject hex)
    {
        /*
        if (!hex.gameObject.GetComponent<HexInteractions>().HasPlant())
        {
            GameObject plant = Instantiate(plantPrefab, hex.transform, false);
            plant.gameObject.transform.localPosition = new Vector3(-0.25f,0.1f,0.2f);
            plant.gameObject.GetComponent<Plant>().SetAttributes(water, nutrition, toxicity, basicScript.CurActionValue);
            hex.gameObject.GetComponent<HexInteractions>().Plant();
        }*/
        
    }
}
