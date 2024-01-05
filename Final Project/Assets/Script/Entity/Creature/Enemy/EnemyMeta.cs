using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New enemy", menuName = "Entity/Creature/Enemy")]
public class EnemyMeta : ScriptableObject
{
    public string EnemyName;
    public float Speed;
    public float HP;
    public float AttackCD;   //�̵u�������j(sec)�A�⦸�������������j����p�󦹭�
    public float ViewDistance;   //�����A�ĤH�u�|����������������
    public float SearchCD;   //�j�����j(sec)�A�C�����շj�������j
    public float TrunCD;   //�̵u�ਭ���j�A�⦸�ਭ����p�󦹭�(���D�I�����)
    public RuntimeAnimatorController Animation;   //���b�ĤH�W���ʵe
}
