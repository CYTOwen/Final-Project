using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Numerics;
using System;

public class EnemyModel : CreatureModel
{
    protected int attack;
    protected int attackCD;   //�������j
    protected FishModel target = null;   //��w���ؼ�
    protected bool huntingMode = false;
    protected override void SearchTarget(List<CreatureModel> targets)   //�M������ؼЪ��Ҧ�
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
        speed = huntingMode ? orginalSpeed * 1.5f : orginalSpeed;   //���y�ɥ[�t
    }
    protected void Attack(FishModel fish)
    {
        Debug.Log(string.Format("{0} attacked on {2}", entityName, fish.entityName));
        fish.Injured(attack);
        huntingMode = false;
    }
}
