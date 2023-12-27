using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityModel : MonoBehaviour
{
    protected string m_entityName;
    public string entityName { get { return m_entityName; } }
    protected float speed;   
    protected virtual void Move()   
    {
    }
}
