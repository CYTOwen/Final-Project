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
        MoneyModel m = Instantiate(money[level - 1], new Vector2(X, Y), Quaternion.identity, parent.transform).GetComponent<MoneyModel>();
        m.GetPlayerInfo(playerInfo);

    }
}
