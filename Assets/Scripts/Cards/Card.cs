using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
using UnityEditorInternal;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private Sprite[] cardFront;
    [SerializeField] private Sprite cardBack;

    private int cardIndex = -1;
    private bool showFront = false;
    private SpriteRenderer spriteRenderer;
    private bool onHand = false;

    [SerializeField] private AnimationCurve scaleCurve;
    [SerializeField] private float duration = 0.5f;

    private Action actionScript;
    private Nutrition nutritionScript;
    private Seed seedScript;
    private Tool toolScript;
    private Trash trashScript;
    private Water waterScript;

    

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = cardBack;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //TODO: Implement card text and attributes
    //TODO: Implement random selection of text for tools and action cards
    
    
    

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

        if (showFront)
        {
            spriteRenderer.sprite = cardFront[cardIndex];
        }
        else
        {
            spriteRenderer.sprite = cardBack;
        }

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
                    spriteRenderer.sprite = cardBack;
                    showFront = false;
                }
                else
                {
                    spriteRenderer.sprite = cardFront[cardIndex];
                    showFront = true;
                }
            }

            yield return new WaitForFixedUpdate();
        }
    }

    //Initializes the card type
    public void SetIndex(int index)
    {
        if (cardIndex == -1)
        {
            cardIndex = index;

            switch (index)
            {
                case 0:
                    nutritionScript = GetComponent<Nutrition>();
                    break;
                case 1:
                    actionScript = GetComponent<Action>();
                    break;
                case 2:
                    seedScript = GetComponent<Seed>();
                    break;
                case 3:
                    waterScript = GetComponent<Water>();
                    break;
                case 4:
                    toolScript = GetComponent<Tool>();
                    break;
                case 5:
                    trashScript = GetComponent<Trash>();
                    break;
            }
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

    //Getter for the card range depending on the card type
    public int GetRange()
    {
        int range = 0;
        
        switch (cardIndex)
        {
            case 0:
                range = nutritionScript.GetRange();
                break;
            case 1:
                range = actionScript.GetRange();
                break;
            case 2:
                range = seedScript.GetRange();
                break;
            case 3:
                range = waterScript.GetRange();
                break;
            case 4:
                range = toolScript.GetRange();
                break;
            case 5:
                range = trashScript.GetRange();
                break;
        }

        return range;
    }
    
    //Returns a boolean, if the card can be played on a hex or not
    public bool IsPlayable(GameObject hexToProve)
    {
        switch (cardIndex)
        {
            case 0:
                return nutritionScript.IsPlayable(hexToProve);
            case 1:
                return actionScript.IsPlayable(hexToProve);
            case 2:
                return seedScript.IsPlayable(hexToProve);
            case 3:
                return waterScript.IsPlayable(hexToProve);
            case 4:
                return toolScript.IsPlayable(hexToProve);
            case 5:
                return trashScript.IsPlayable(hexToProve);
        }

        return true;
    }

    
    //Implements the action of the different card types
    public void CardAction(GameObject hex)
    {
        switch (cardIndex)
        {
            case 0:
                nutritionScript.CardAction(hex);
                break;
            case 1:
                actionScript.CardAction();
                break;
            case 2:
                seedScript.CardAction(hex);
                break;
            case 3:
                waterScript.CardAction(hex);
                break;
            case 4:
                toolScript.CardAction();
                break;
            case 5:
                trashScript.CardAction();
                break;
        }
    }
    
    
}
