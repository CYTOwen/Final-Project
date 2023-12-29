using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishModel : CreatureModel
{
    protected int maturity;   //���������i��
    protected int production;   //�����ͪ������Ĳv
    protected bool panic = false;
    protected List<EnemyModel> enemies;
    protected virtual void FindFood()   //�V���Ҧ�
    {
        //��object���������A�g
    }
    protected override void SearchTarget(List<GameObject> targets)   //�ˬd����O�_���ĤH
    {
        enemies.Clear();
        panic = false;
        foreach (GameObject creature in targets)
        {
            EnemyModel enemy = creature.GetComponent<EnemyModel>();
            if (Math.Abs(enemy.gameObject.transform.position.x - transform.position.x) < ViewDistance)
            {
                enemies.Add(enemy);
                panic = true;
            }
        }
        //speed = panic ? orginalSpeed * 2 : orginalSpeed;   //���W�ɥ[�t
    }
}
