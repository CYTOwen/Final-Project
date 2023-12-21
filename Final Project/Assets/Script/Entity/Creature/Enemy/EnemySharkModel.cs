using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed public class EnemySharkModel : EnemyModel
{
    private EnemySharkModel()
    {
        m_entityName = "Shark";
        speed = 30f;
        HP = 500;
        attack = 10;
        attackCD = 5;
        maxViewDistance = 480f;
        searchCD = 5;
        target = null;
        huntingMode = false;
    }
    // Update is called once per frame
    void Update()
    {
        System.Random rand = new System.Random();   //�H����ʡA�C����10%�����v���s
        int x = rand.Next(100);
        if (x > 90)
        {
            Turn();
        }
        else
        {
            Move();
        }
    }
    protected override void Move()
    {
        //����
    }
    private void Turn()
    {
        //���s
    }
}
