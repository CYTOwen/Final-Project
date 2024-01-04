using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFactory : MonoBehaviour
{
    [SerializeField]
    protected PlayerInfo playerInfo;
    public virtual void CreateObject(float X,float Y,int level)
    {

    }
}
