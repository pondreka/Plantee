using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    public int maxActions = 5;
    public int curActions;
    private int actionCount;

    // Start is called before the first frame update
    void Start()
    {
        curActions = maxActions;
        actionCount = maxActions;

    }

    //Getter for the actions
    public int GetActions()
    {
        return curActions;
    }

    //Sets the new value for current actions
    //actions might be positive or negative
    public void SetActions(int actions) 
    {
        if (actions + curActions > maxActions)
        {
            curActions = maxActions;
        }
        else if (actions + curActions < 0)
        {
            curActions = 0;
        }
        else
        {
            curActions += actions;
        }
        
        UpdateActionVisualisation();
    }

    //Updates the mana bar visualisation according to the current actions
    private void UpdateActionVisualisation()
    {
        if (actionCount < curActions)
        {
            for (int i = actionCount; i < curActions; i++)
            {
                this.transform.GetChild(i).gameObject.SetActive(true);
            }
        } 
        else if (actionCount > curActions)
        {
            for (int i = maxActions - 1; i >= curActions; i--)
            {
                this.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        actionCount = curActions;
    }
    
}
