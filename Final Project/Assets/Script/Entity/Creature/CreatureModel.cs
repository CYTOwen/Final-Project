using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureModel : EntityModel
{
    protected float orginalSpeed { get { return speed; } }   //儲存原本的速度
    protected int HP;
    protected float maxViewDistance = 480f;   //視野
    protected int searchCD;   //每隔多久執行一次SearchTarget()
    protected virtual void SearchTarget(List<CreatureModel> targets)   //尋找攻擊目標的模式
    {

    }
}
