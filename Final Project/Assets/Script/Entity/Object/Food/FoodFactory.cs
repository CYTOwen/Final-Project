using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodFactory : ObjectFactory
{
    [SerializeField]
    private GameObject food;
    [SerializeField]
    private GameObject parent;
    public override void CreateObject(float X, float Y)
    {
        Instantiate(food, new Vector2(X, Y), Quaternion.identity, parent.transform);
    }
}
