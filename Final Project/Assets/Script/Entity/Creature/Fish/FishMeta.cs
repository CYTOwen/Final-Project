using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fish", menuName = "Entity/Creature/Fish")]
public class FishMeta : ScriptableObject
{
    public string FishName;
    public int Cost;   //����
    public float Speed;
    public float ViewDistance;   //�����Z��
    public float SearchCD;   //�j��CD
    public float TurnCD;   //���sCD
    public float EatCD;   //�i��CD
    public float MaxStarvingTime;   //�̤j���j�ɶ��A�W�L�|�j��
    public float MoneyCD;   //���ͪ�����CD
    public int MaturityPerLevel;   //�C�����@�ũһݯ�q
    public int MaxLevel;   //�̤j����
    public int[] Production = new int[4];   //�C�ӵ��Ť@���Ͳ��h�֪���
    public AnimatorController Animation;   //�ʵe
}
