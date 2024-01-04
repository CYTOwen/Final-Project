using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureFactory : MonoBehaviour
{
    [SerializeField]
    protected PlayerInfo playerInfo;
    [SerializeField]
    protected RectTransform canvas;
    protected System.Random rand = new System.Random();
    public virtual CreatureModel CreateCreature()
    {
        return null;
    }
}
