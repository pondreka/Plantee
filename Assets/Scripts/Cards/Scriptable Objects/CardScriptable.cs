using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName =  "Cards/Basic")]
public class CardScriptable : ScriptableObject
{
    //Represented on card cover
    public new string name;
    public string action;
    public int cost;
    public int cardRange;
    public Sprite foreground; 
    public int actionValue;
    public int actionRange;
    public int index;
    
}
