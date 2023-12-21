using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
    protected override void SearchTarget(List<CreatureModel> targets)   //�ˬd����O�_���ĤH
    {
        enemies.Clear();
        panic = false;
        foreach (EnemyModel enemy in targets)
        {
            if (Math.Abs(enemy.gameObject.transform.position.x - transform.position.x) < maxViewDistance)
            {
                enemies.Add(enemy);
                panic = true;
            }
        }
        speed = panic ? orginalSpeed * 2 : orginalSpeed;   //���W�ɥ[�t
    }
    public virtual void Injured(int damage)   //�Q�������ɭ�Ĳ�o
    {
        HP -= damage;
        Debug.Log(string.Format("{0} take {1} damage.", entityName, damage));   //�Ȯɪ����հT��
        if (HP <= 0)
            Die();
    }
    protected void Die()
    {
        Debug.Log(string.Format("{0} is dead.", entityName));   //�Ȯɪ����հT��
    }

}
