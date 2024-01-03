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
    public float MaxStarvingTime;   //�̤j���j�ɶ��A�W�L�|�j��
    public float MoneyCD;   //���ͪ�����CD
    public int MaturityPerLevel;
    public int MaxLevel;   //�̤j����
    public int[] Production = new int[4];   //�C�ӵ��Ť@���Ͳ��h�֪���
    public AnimatorController Animation;
}
