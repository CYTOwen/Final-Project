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
    [SerializeField]
    private RectTransform canvas;
    System.Random rand = new System.Random();
    private void Update()
    {
        if(Input.anyKeyDown)
            CreateCreature();
    }
    public override void CreateCreature()   //生成敵人
    {
        EnemyModel enemy = Instantiate(enemyPrefab, new Vector3((rand.Next(1, 3) * 2 - 3) * canvas.rect.width / 2f, rand.Next((int)canvas.rect.height * -1, (int)canvas.rect.height) / 2f, 0), Quaternion.Euler(0, 0, 0), parent.transform).GetComponent<EnemyModel>();
        enemy.SetEnemyType(enemies[rand.Next(enemies.Length)]);   //隨機決定敵人種類
        enemy.GetCanvasSize(canvas);
        /*
         * enemyList.Add(enemy);  把生成的物件存進list裡面(名字根據遊戲機制腳本決定)
         * */
    }
}
