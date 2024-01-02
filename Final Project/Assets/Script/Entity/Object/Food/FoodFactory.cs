using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodFactory : ObjectFactory
{
    [SerializeField]
    private List<GameObject> food;
    [SerializeField]
    private GameObject parent;
    public override void CreateObject(float X, float Y, int level)
    {
        if (level == 1)
        {
            Instantiate(food[0], new Vector2(X, Y), Quaternion.identity, parent.transform);
        }
        else if (level == 2)
        {
            Instantiate(food[1], new Vector2(X, Y), Quaternion.identity, parent.transform);
        }
        else if (level == 3)
        {
            Instantiate(food[2], new Vector2(X, Y), Quaternion.identity, parent.transform);
        }
        else if (level == 4)
        {
            Instantiate(food[3], new Vector2(X, Y), Quaternion.identity, parent.transform);
        }
    }
}
