using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMeta : MonoBehaviour
{
    public string FishName;
    public float MaxStarvingTime;   //最大挨餓時間，超過會餓死
    public float[] StarvingStage = new float[4];
    public float Maturity;
    public float MoneyTime;
    public int MaxLevel;
    public int[] Production = new int[4];
}
