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
    

    [Header("Card Deck Initialization")] 
    [SerializeField] private int seeds = 5;
    [SerializeField] private int water = 5;
    [SerializeField] private int nutrition = 3;
    [SerializeField] private GameObject[] seedVariants;
    [SerializeField] private GameObject[] actionVariants;
    [SerializeField] private GameObject[] toolVariants;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        InitializeDeck();
    }

    // Update is called once per frame
    void Update()
    {
        if (hand.Count == 0)
        {
            DrawHand();
        }
    }

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
        
        if (hand == null)
        {
            hand = new List<GameObject>();
        }
        
        for (int i = 0; i < seeds; i++)
        {
            GameObject card = RandomCardSelection(3);
            deck.Add(card);
        }

        for (int i = 0; i < water; i++)
        {
            GameObject card = Instantiate(waterCardPrefab, this.transform, false);
            deck.Add(card);
        }

        for (int i = 0; i < nutrition; i++)
        {
            GameObject card = Instantiate(nutritionCardPrefab, this.transform, false);
            deck.Add(card);
        }
        Shuffle();
        DrawHand();
    }
    
    //Random card selection of a specific card type
    private GameObject RandomCardSelection(int index)
    {
        GameObject card;
        int rand = 0;
        
        switch (index)
        {
            case 3:
                rand = Random.Range(0, seedVariants.Length);
                card = Instantiate(seedVariants[rand], this.transform, false);
                return card;
            case 4:
                rand = Random.Range(0, actionVariants.Length);
                card = Instantiate(actionVariants[rand], this.transform, false);
                return card;
            case 5:
                rand = Random.Range(0, toolVariants.Length);
                card = Instantiate(toolVariants[rand], this.transform, false);
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
        if (discard == null)
        {
            discard = new List<GameObject>();
        }

        for (int i = 0; i < discard.Count; i++)
        {
            discard[i].gameObject.transform.GetChild(2).GetComponent<Canvas>().sortingOrder = 0;
        }
        
        discard.Add(card);
        
        if (hand.Remove(card))
        {
            card.gameObject.GetComponent<CardBasic>().OnHand();
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
        for (int c = 0; c < discard.Count; c++)
        {
            deck.Add(discard[c]);
            discard[c].transform.localPosition = new Vector3(0, 0, 0);
            discard[c].gameObject.GetComponent<CardBasic>().FlipCard();
        }
            
        discard.Clear();
        Shuffle();
    }
    
    //Instantiate new card of type index and putting it on the discard
    public void NewCard(int index)
    {
        GameObject card = null;
        
        switch (index)
        {
            case 0:
                card = Instantiate(waterCardPrefab, this.transform, false);
                break;
            case 1:
                card = Instantiate(nutritionCardPrefab, this.transform, false);
                break;
            case 2:
                card = Instantiate(trashCardPrefab, this.transform, false);
                break;
            case 3:
                card = RandomCardSelection(index);
                break;
            case 4:
                card = RandomCardSelection(index);
                break;
            case 5:
                card = RandomCardSelection(index);
                break;
        }

        if (card != null)
        {
            CardBasic cardScript = card.GetComponent<CardBasic>();
            cardScript.FlipCard();
            Discard(card);
        }
            
        
    }
}
