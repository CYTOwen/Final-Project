using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineFactory : ObjectFactory
{
    [SerializeField]
    private GameObject medicine;
    [SerializeField]
    private GameObject parent;
    public override void CreateObject(float X, float Y, int level)
    {
        Instantiate(medicine, new Vector2(X, Y), Quaternion.identity, parent.transform);
    }
}
