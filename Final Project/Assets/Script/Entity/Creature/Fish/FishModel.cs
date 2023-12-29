using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishModel : CreatureModel
{
    protected int maturity;   //魚成長的進度
    protected int production;   //魚產生金幣的效率
    protected bool panic = false;
    protected List<EnemyModel> enemies;
    protected virtual void FindFood()   //覓食模式
    {
        //等object部分完成再寫
    }
    protected override void SearchTarget(List<GameObject> targets)   //檢查附近是否有敵人
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
        //speed = panic ? orginalSpeed * 2 : orginalSpeed;   //恐慌時加速
    }
}
