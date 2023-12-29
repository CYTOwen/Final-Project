using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : CreatureFactory
{
    [SerializeField]
    private GameObject enemyPrefab;   //�ĤH��prefab
    [SerializeField]
    private GameObject parent;   //�ͦ��ĤH�Ҧb��������
    [SerializeField]
    private EnemyMeta[] enemies;   //�ĤH�����@��
    System.Random rand = new System.Random();

    public override void CreateCreature()   //�ͦ��ĤH
    {
        EnemyModel enemy = Instantiate(gameObject, new Vector2(rand.Next(1, 3) * 1920f - 960f, rand.Next(-500, 501)), Quaternion.Euler(0, 0, 0), parent.transform).GetComponent<EnemyModel>();
        enemy.SetEnemyType(enemies[rand.Next(enemies.Length)]);   //�H���M�w�ĤH����
    }
}
