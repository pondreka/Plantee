using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    private int water = -1;
    private int nutrition = -1;
    private int toxicity = -1;
    private int trash = -1;


    
    //Getter water
    public int GetWater()
    {
        return water;
    }
    
    //Setter water
    //Adjustment of the attribute by the given value
    //value might be positive or negative
    public void SetWater(int value)
    {
        if (water == -1)
        {
            water = value;
        }
        else if (water + value < 0)
        {
            water = 0;
        }
        else if (water + value > 10)
        {
            water = 10;
        }
        else
        {
            water += value;
        }
    }
    
    
    //Getter nutrition
    public int GetNutrition()
    {
        return nutrition;
    }
    
    //Setter nutrition
    //Adjustment of the attribute by the given value
    //value might be positive or negative
    public void SetNutrition(int value)
    {
        if (nutrition == -1)
        {
            nutrition = value;
        }
        else if (nutrition + value < 0)
        {
            nutrition = 0;
        }
        else if (nutrition + value > 10)
        {
            nutrition = 10;
        }
        else
        {
            nutrition += value;
        }
    }
    
    
    //Getter toxicity
    public int GetToxicity()
    {
        return toxicity;
    }
    
    //Setter toxicity
    //Adjustment of the attribute by the given value
    //value might be positive or negative
    public void SetToxicity(int value)
    {
        if (toxicity == -1)
        {
            toxicity = value;
        }
        else if (toxicity + value < 0)
        {
            toxicity = 0;
        }
        else if (toxicity + value > 10)
        {
            toxicity = 10;
        }
        else
        {
            toxicity += value;
        }
    }
    
    
    //Getter trash
    public int GetTrash()
    {
        return trash;
    }
    
    //Setter trash
    //Adjustment of the attribute by the given value
    //value might be positive or negative
    public void SetTrash(int value)
    {
        if (trash == -1)
        {
            trash = value;
        }
        else if (trash + value < 0)
        {
            trash = 0;
        }
        else if (trash + value > 10)
        {
            trash = 10;
        }
        else
        {
            trash += value;
        }
    }

    
    //Setter for all Attributes at a time
    //For initialisation or updating
    public void SetAllAttributes(int wa, int nu, int to, int tr)
    {
        SetWater(wa);
        SetNutrition(nu);
        SetToxicity(to);
        SetTrash(tr);
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
