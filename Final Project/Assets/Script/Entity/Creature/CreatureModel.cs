using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureModel : EntityModel
{
    protected float HP;
    protected float ViewDistance = 480f;   //����
    protected float searchCD;   //�C�j�h�[����@��SearchTarget()
    protected virtual void SearchTarget(List<GameObject> targets)   //�M������ؼЪ��Ҧ�
    {

    }
}
