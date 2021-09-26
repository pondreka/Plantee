using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Seed", menuName =  "Cards/Seed")]
public class CardScriptableSeed : ScriptableObject
{
    public int waterValue;
    public int nutritionValue;
    public int toxicityValue;
    public int trashValue;
    public new string name;
    public string action;
    public int actionValue;
    public int actionRange;
    public int evolutionTime;
    public int actionIndex;
}
