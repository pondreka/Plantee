using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    
    private static CardManager _instance;
    public static CardManager Instance => _instance;
    
    
    public GameObject waterCardPrefab;
    public GameObject nutritionCardPrefab;
    public GameObject trashCardPrefab;
    private List<GameObject> deck;
    private List<GameObject> discard;
    private List<GameObject> hand;
    private List<GameObject> tools;
    private List<GameObject> waters;
    private List<GameObject> nutritions;
    private List<GameObject> seeds;
    private List<GameObject> trashs;
    private List<GameObject> toolHand;


    [Header("Card Deck Initialization")] 
    [SerializeField] private int seed = 5;
    [SerializeField] private int water = 5;
    [SerializeField] private int nutrition = 3;
    [SerializeField] private GameObject[] seedVariants;
    [SerializeField] private GameObject[] actionVariants;
    [SerializeField] private GameObject[] toolVariants;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(_instance.gameObject);
            _instance = this;
        }
        else
        {
            _instance = this;
        }
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        seeds ??= new List<GameObject>();
        waters ??= new List<GameObject>();
        nutritions ??= new List<GameObject>();
        trashs ??= new List<GameObject>();
        tools ??= new List<GameObject>();
        toolHand ??= new List<GameObject>();
        InitializeDeck();
    }

    // Update is called once per frame
    private void Update()
    {
        if (hand.Count == 0)
        {
            DrawHand();
        }
    }

    //Card deck initialization
    private void InitializeDeck()
    {
        if (deck == null)
        {
            deck = new List<GameObject>();
        }
        else
        {
            deck.Clear();
        }
        
        hand ??= new List<GameObject>();
        
        for (int i = 0; i < seed; i++)
        {
            GameObject card = CardSelection(3);
            seeds.Add(card);
            deck.Add(card);
        }

        for (int i = 0; i < water; i++)
        {
            GameObject card = Instantiate(waterCardPrefab, this.transform, false);
            waters.Add(card);
            deck.Add(card);
        }

        for (int i = 0; i < nutrition; i++)
        {
            GameObject card = Instantiate(nutritionCardPrefab, this.transform, false);
            nutritions.Add(card);
            deck.Add(card);
        }


        Shuffle();
        DrawHand();
    }
    
    //Random card selection of a specific card type
    private GameObject CardSelection(int index)
    {
        GameObject card;
        int rand = 0;
        
        switch (index)
        {
            case 0:
                card = Instantiate(waterCardPrefab, this.transform, false);
                waters.Add(card);
                return card;
            case 1:
                card = Instantiate(nutritionCardPrefab, this.transform, false);
                nutritions.Add(card);
                return card;
            case 2:
                card = Instantiate(trashCardPrefab, this.transform, false);
                trashs.Add(card);
                return card;
            case 3:
                rand = Random.Range(0, seedVariants.Length);
                card = Instantiate(seedVariants[rand], this.transform, false);
                seeds.Add(card);
                return card;
            case 4:
                rand = Random.Range(0, actionVariants.Length);
                card = Instantiate(actionVariants[rand], this.transform, false);
                return card;
            case 5:
                rand = Random.Range(0, toolVariants.Length);
                card = Instantiate(toolVariants[rand], this.transform, false);
                tools.Add(card);
                return card;
                
        }

        return null;
    }

    //Shuffles the cards in the deck
    private void Shuffle()
    {
        for (int c = 0; c < deck.Count; c++)
        {
            int newIndex = Random.Range(0, deck.Count);
            GameObject tempCard = deck[c];
            deck[c] = deck[newIndex];
            deck[newIndex] = tempCard;
        }
    }

    //fills the hand at the end of a round
    public void DrawHand()
    {
        if (hand.Count < 5)
        {
            int cardsToDraw = 5 - hand.Count;
            
            if (deck.Count >= cardsToDraw)
            {
                for (int c = 0; c < cardsToDraw; c++)
                {
                    Draw();
                }
            }
            else
            {
                int cardsLeft = cardsToDraw - deck.Count;
                int deckCount = deck.Count;
                for (int c = 0; c < deckCount; c++)
                {
                    Draw();
                }
                RenewDeck();
                for (int c = 0; c < cardsLeft; c++)
                {
                    Draw();
                }
            }
        }

        for (int c = 0; c < hand.Count; c++)
        {
            CardBasic cardScript = hand[c].gameObject.GetComponent<CardBasic>();
            if (!cardScript.IsOnHand())
            {
                cardScript.FlipCard();
                cardScript.OnHand();
            }
            hand[c].transform.localPosition = new Vector3(-11 * c, -59, -13);
        }
    }

    //Draws a full new hand
    public void DrawNewHand()
    {
        for (int i = hand.Count - 1; i >= 0; i--)
        {
            Discard(hand[i]);
        }
        DrawHand();
    }

    //draws one card from the deck and adds it to the hand
    private void Draw()
    {
        GameObject card = deck[0];
        deck.RemoveAt(0);
        hand.Add(card);
    }

    //Puts a card on the discard deck
    //might be a card found on the map or a hand card
    public void Discard(GameObject card)
    {
        discard ??= new List<GameObject>();

        foreach (var c in discard)
        {
            c.gameObject.transform.GetChild(2).GetComponent<Canvas>().sortingOrder = 0;
        }
        
        discard.Add(card);
        
        if (hand.Remove(card))
        {
            card.gameObject.GetComponent<CardBasic>().OnHand();
        }

        if (toolHand.Remove(card))
        {
            card.gameObject.GetComponent<CardTool>().SetOnField();
        }
        
        card.transform.localPosition = new Vector3(-11f, 0f, -10f);
        card.gameObject.transform.GetChild(2).GetComponent<Canvas>().sortingOrder = 1;
        
    }

    //Deletes a card if only usable once
    public void StoreCard(GameObject card)
    {
        if (hand.Remove(card))
        {
            card.gameObject.GetComponent<CardBasic>().OnHand();
            
        }
        
        card.transform.localPosition = new Vector3(-11f, 0f, -10f);
        card.gameObject.SetActive(false);
    }

    //Shuffles the cards form discard stack into the deck
    private void RenewDeck()
    {
        foreach (var c in discard)
        {
            deck.Add(c);
            c.transform.localPosition = new Vector3(0, 0, 0);
            c.gameObject.GetComponent<CardBasic>().FlipCard();
        }
            
        discard.Clear();
        Shuffle();
    }
    
    //Instantiate new card of type index and putting it on the discard
    public void NewCard(int index)
    {
        GameObject card = CardSelection(index);

        if (card == null) return;
        CardBasic cardScript = card.GetComponent<CardBasic>();
        cardScript.FlipCard();
        Discard(card);
    }
    
    //Returns a list with all tools
    public List<GameObject> ToolCards()
    {
        return tools;
    }
    
    //Returns a list with all water cards
    public List<GameObject> WaterCards()
    {
        return waters;
    }
    
    //Returns a list with all nutrition cards
    public List<GameObject> NutritionCards()
    {
        return nutritions;
    }
    
    //Returns a list with all seed cards
    public List<GameObject> SeedCards()
    {
        return seeds;
    }
    
    //Returns a list with all trash cards
    public List<GameObject> TrashCards()
    {
        return trashs;
    }
    
    //Puts tool on its position
    public void PlayToolCard(GameObject card)
    {
        card.transform.localPosition = toolHand.Count switch
        {
            0 => new Vector3(0f, -30f, 0f),
            1 => new Vector3(-11f, -30f, 0f),
            2 => new Vector3(-22f, -30f, 0f),
            _ => card.transform.localPosition
        };
        
        card.GetComponent<CardTool>().SetOnField();
        toolHand.Add(card);
        
        if (hand.Remove(card))
        {
            card.gameObject.GetComponent<CardBasic>().OnHand();
            
        }

        if (toolHand.Count <= 2) return;
        CardTool.SetPlayable(toolHand.Count);

    }

    //Discards a tool from the field and reorders the remaining tools
    public void DiscardToolCard(GameObject card)
    {
        Discard(card);
        for (int i = 0; i < toolHand.Count; i++)
        {
            toolHand[i].transform.localPosition = i switch
            {
                0 => new Vector3(0f, -30f, 0f),
                1 => new Vector3(-11f, -30f, 0f),
                2 => new Vector3(-22f, -30f, 0f),
                _ => toolHand[i].transform.localPosition
            };
        }
        
        CardTool.SetPlayable(toolHand.Count);
    }
}
