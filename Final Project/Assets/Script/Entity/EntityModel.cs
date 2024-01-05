using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityModel : MonoBehaviour
{
    [SerializeField]
    protected PlayerInfo playerInfo;
    protected string entityName;
    public void GetPlayerInfo(PlayerInfo info)
    {
        playerInfo = info;
    }
}
