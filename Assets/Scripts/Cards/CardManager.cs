using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    
    private static CardManager _instance;
    public static CardManager Instance => _instance;
    
    
    [SerializeField] private GameObject cardPrefab;
    private List<GameObject> deck;
    private List<GameObject> discard;
    private List<GameObject> hand;
    

    [Header("Card Deck Initialization")] 
    [SerializeField] private int seeds = 5;
    [SerializeField] private int water = 5;
    [SerializeField] private int nutrition = 3;
    

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
        if (cardPrefab == null)
        {
            Debug.LogError("No card prefab assigned to CardManager!");
        }
        
        InitializeDeck();
    }

    // Update is called once per frame
    void Update()
    {
        
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
            GameObject card = Instantiate(cardPrefab, this.transform, false);
            card.GetComponent<Card>().SetIndex(2);
            deck.Add(card);
        }

        for (int i = 0; i < water; i++)
        {
            GameObject card = Instantiate(cardPrefab, this.transform, false);
            card.GetComponent<Card>().SetIndex(3);
            deck.Add(card);
        }

        for (int i = 0; i < nutrition; i++)
        {
            GameObject card = Instantiate(cardPrefab, this.transform, false);
            card.GetComponent<Card>().SetIndex(0);
            deck.Add(card);
        }
        Shuffle();
        DrawHand();
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
            Card cardScript = hand[c].gameObject.GetComponent<Card>();
            if (!cardScript.IsOnHand())
            {
                cardScript.FlipCard();
                cardScript.OnHand();
            }
            hand[c].transform.localPosition = new Vector3(- 1 - c, hand[c].transform.localPosition.y, hand[c].transform.localPosition.z);
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
        
        discard.Add(card);
        
        if (hand.Remove(card))
        {
            card.gameObject.GetComponent<Card>().OnHand();
        }
        
        card.transform.localPosition = new Vector3(-4.4f, 2.35f, card.transform.localPosition.z);
        
        //TODO: Fix the scaling bug, maybe has something to do with the Flip
        //Debug.Log(card.transform.localScale);
        //card.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        //Debug.Log(card.transform.localScale);
    }

    //Deletes a card if only usable once
    public void DeleteCard(GameObject card)
    {
        if (hand.Remove(card))
        {
            card.gameObject.GetComponent<Card>().OnHand();
        }
        
        Destroy(card);
    }

    //Shuffles the cards form discard stack into the deck
    private void RenewDeck()
    {
        for (int c = 0; c < discard.Count; c++)
        {
            deck.Add(discard[c]);
            discard[c].transform.localPosition = new Vector3(0, 0, 0);
            //discard[c].transform.localScale = new Vector3(1f, 1f, 1f);
            discard[c].gameObject.GetComponent<Card>().FlipCard();
        }
            
        discard.Clear();
        Shuffle();
    }
    
    //Instantiate new card of type index and putting it on the discard
    public void NewCard(int index)
    {
        GameObject card = Instantiate(cardPrefab, this.transform, false);
        Card cardScript = card.GetComponent<Card>();
        cardScript.SetIndex(index);
        cardScript.FlipCard();
        Discard(card);
    }
}
