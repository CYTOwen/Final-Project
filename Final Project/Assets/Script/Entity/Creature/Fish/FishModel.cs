using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
    protected override void SearchTarget(List<CreatureModel> targets)   //檢查附近是否有敵人
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
        speed = panic ? orginalSpeed * 2 : orginalSpeed;   //恐慌時加速
    }
    public virtual void Injured(int damage)   //被攻擊的時候觸發
    {
        HP -= damage;
        Debug.Log(string.Format("{0} take {1} damage.", entityName, damage));   //暫時的測試訊息
        if (HP <= 0)
            Die();
    }
    protected void Die()
    {
        Debug.Log(string.Format("{0} is dead.", entityName));   //暫時的測試訊息
    }

}
