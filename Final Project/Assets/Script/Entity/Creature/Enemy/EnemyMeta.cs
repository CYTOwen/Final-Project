using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New enemy", menuName = "Entity/Creature/Enemy")]
public class EnemyMeta : ScriptableObject
{
    public string EnemyName;
    public float Speed;
    public float HP;
    public float AttackCD;   //最短攻擊間隔(sec)，兩次攻擊之間的間隔不能小於此值
    public float ViewDistance;   //視野，敵人只會偵測視野內的物件
    public float SearchCD;   //搜索間隔(sec)，每次嘗試搜索的間隔
    public float TrunCD;   //最短轉身間隔，兩次轉身不能小於此值(除非碰到牆壁)
    public RuntimeAnimatorController Animation;   //掛在敵人上的動畫
}
