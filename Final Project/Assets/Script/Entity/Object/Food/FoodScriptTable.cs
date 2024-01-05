using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New food", menuName = "Entity/Object/Food")]
public class FoodScriptTable : ScriptableObject
{
    public int food_value; //value of food for fish upgrding
    public int food_price; //restriction of food generate number
}
