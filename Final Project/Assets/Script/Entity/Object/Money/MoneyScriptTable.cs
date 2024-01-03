using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

[CreateAssetMenu(fileName = "New money", menuName = "Entity/Object/Money")]
public class MoneyScriptTable : ScriptableObject
{
    public int money_value; //value of money
    public AnimatorController Animation;
}
