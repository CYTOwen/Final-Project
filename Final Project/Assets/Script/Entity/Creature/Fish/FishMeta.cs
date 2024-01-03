using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fish", menuName = "Entity/Creature/Fish")]
public class FishMeta : ScriptableObject
{
    public string FishName;
    public float Speed;
    public float ViewDistance;
    public float SearchCD;
    public float TurnCD;
    public float EatCD;
    public float MaxStarvingTime;   //最大挨餓時間，超過會餓死
    public float MoneyCD;   //產生金幣的CD
    public int MaturityPerLevel;
    public int MaxLevel;   //最大等級
    public int[] Production = new int[4];   //每個等級一次生產多少金幣
    public AnimatorController Animation;
}
