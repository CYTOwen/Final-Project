using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : CreatureFactory
{
    [SerializeField]
    private GameObject enemyPrefab;   //敵人的prefab
    [SerializeField]
    private GameObject parent;   //生成敵人所在的父物件
    [SerializeField]
    private EnemyMeta[] enemies;   //敵人種類一覽
    System.Random rand = new System.Random();

    public override void CreateCreature()   //生成敵人
    {
        EnemyModel enemy = Instantiate(gameObject, new Vector2(rand.Next(1, 3) * 1920f - 960f, rand.Next(-500, 501)), Quaternion.Euler(0, 0, 0), parent.transform).GetComponent<EnemyModel>();
        enemy.SetEnemyType(enemies[rand.Next(enemies.Length)]);   //隨機決定敵人種類
    }
}
