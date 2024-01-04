using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodFactory : ObjectFactory
{
    [SerializeField]
    private List<GameObject> food;
    [SerializeField]
    private GameObject parent;
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
            CreateObject(0, 300, 1);
    }
    public override void CreateObject(float X, float Y, int level)
    {
        FoodModel f = Instantiate(food[level - 1], new Vector2(X, Y), Quaternion.identity, parent.transform).GetComponent<FoodModel>();
        //playerInfo.AddElement("Food", f);
    }
}
