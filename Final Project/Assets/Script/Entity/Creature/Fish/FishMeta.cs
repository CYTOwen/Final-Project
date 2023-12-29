using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMeta : MonoBehaviour
{
    public string FishName;
    public float MaxStarvingTime;   //�̤j���j�ɶ��A�W�L�|�j��
    public float[] StarvingStage = new float[4];
    public float Maturity;
    public float MoneyTime;
    public int MaxLevel;
    public int[] Production = new int[4];
}
