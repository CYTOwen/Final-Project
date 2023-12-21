using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Numerics;
using System;

public class EnemyModel : CreatureModel
{
    protected int attack;
    protected int attackCD;   //攻擊間隔
    protected FishModel target = null;   //選定的目標
    protected bool huntingMode = false;
    protected override void SearchTarget(List<CreatureModel> targets)   //尋找攻擊目標的模式
    {
        foreach (FishModel fish in targets)
        {
            if (Math.Abs(fish.gameObject.transform.position.x - transform.position.x) < maxViewDistance)
            {
                maxViewDistance = Math.Abs(fish.gameObject.transform.position.x - transform.position.x);
                target = fish;
                huntingMode = true;
            }
        }
        speed = huntingMode ? orginalSpeed * 1.5f : orginalSpeed;   //狩獵時加速
    }
    protected void Attack(FishModel fish)
    {
        Debug.Log(string.Format("{0} attacked on {2}", entityName, fish.entityName));
        fish.Injured(attack);
        huntingMode = false;
    }
}
