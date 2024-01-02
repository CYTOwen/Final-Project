using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyFactory : ObjectFactory
{
    [SerializeField]
    private GameObject money;
    [SerializeField]
    private GameObject parent;
    public override void CreateObject(float X,float Y)
    {
        Instantiate(money,new Vector2(X,Y),Quaternion.identity,parent.transform);
    }
}
