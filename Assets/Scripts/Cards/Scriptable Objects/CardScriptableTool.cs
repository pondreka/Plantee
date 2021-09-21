using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tool", menuName =  "Cards/Tool")]
public class CardScriptableTool : ScriptableObject
{
    public string action;
    public new string name;
    public int actionValue;
    public int actionRange;
}
