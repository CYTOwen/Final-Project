using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FoodFactory : ObjectFactory
{
    [SerializeField]
    private List<GameObject> food;
    [SerializeField]
    private GameObject parent;
    public override void CreateObject(float X, float Y, int level)
    {
        FoodModel f = Instantiate(food[level - 1], new Vector2(X, Y), Quaternion.identity, parent.transform).GetComponent<FoodModel>();
        f.GetPlayerInfo(playerInfo);
        playerInfo.AddElement("Food", f);
    }
}
