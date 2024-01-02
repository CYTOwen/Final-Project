using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

[CreateAssetMenu(fileName = "New medicine", menuName = "Entity/Object/Medicine")]
public class MedicineScriptTable : ScriptableObject
{
    public AnimatorController Animation;
}
