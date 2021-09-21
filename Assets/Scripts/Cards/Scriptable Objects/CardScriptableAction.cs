using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Action", menuName =  "Cards/Action")]
public class CardScriptableAction : ScriptableObject
{
    public string action;
    public int cardRange;
    public int actionValue;
    public int actionRange;
}
