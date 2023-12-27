using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureModel : EntityModel
{
    protected float HP;
    protected float ViewDistance = 480f;   //視野
    protected float searchCD;   //每隔多久執行一次SearchTarget()
    protected virtual void SearchTarget(List<GameObject> targets)   //尋找攻擊目標的模式
    {

    }
}
