using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureModel : EntityModel
{
    protected float orginalSpeed { get { return speed; } }   //�x�s�쥻���t��
    protected int HP;
    protected float maxViewDistance = 480f;   //����
    protected int searchCD;   //�C�j�h�[����@��SearchTarget()
    protected virtual void SearchTarget(List<CreatureModel> targets)   //�M������ؼЪ��Ҧ�
    {

    }
}
