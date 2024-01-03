using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyFactory : ObjectFactory
{
    [SerializeField]
    private List<GameObject> money;
    [SerializeField]
    private GameObject parent;

    public override void CreateObject(float X,float Y, int level)
    {
        if (level == 1)
        {
            Instantiate(money[0], new Vector2(X, Y), Quaternion.identity, parent.transform);
        }
        else if (level == 2)
        {
            Instantiate(money[1], new Vector2(X, Y), Quaternion.identity, parent.transform);
        }
        else if (level == 3)
        {
            Instantiate(money[2], new Vector2(X, Y), Quaternion.identity, parent.transform);
        }
        else if (level == 4)
        {
            Instantiate(money[3], new Vector2(X, Y), Quaternion.identity, parent.transform);
        }
    }
}
