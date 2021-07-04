using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    private List<GameObject> cards;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Shuffle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shuffle()
    {
        if (cards == null)
        {
            cards = new List<GameObject>();
        }
        else
        {
            cards.Clear();
        }
        
    }
}
