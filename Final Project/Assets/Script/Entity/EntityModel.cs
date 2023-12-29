using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityModel : MonoBehaviour
{
    protected string entityName;
    protected float speed;   //移動速度
    protected float turnCD;   //最短轉彎週期
    protected virtual void Walk()   //行動模式
    {
    }
}
