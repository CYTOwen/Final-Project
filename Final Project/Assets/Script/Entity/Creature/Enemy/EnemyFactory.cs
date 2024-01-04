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
    private void Update()   //測試用函式，記得註解掉
    {
        if (Input.GetKeyDown(KeyCode.E))
            CreateCreature();
    }
    public override CreatureModel CreateCreature()   //生成敵人
    {
        EnemyModel enemy = Instantiate(enemyPrefab, new Vector3((rand.Next(1, 3) * 2 - 3) * canvas.rect.width / 2f, rand.Next((int)canvas.rect.height * -1, (int)canvas.rect.height) / 2f, 0), Quaternion.Euler(0, 0, 0), parent.transform).GetComponent<EnemyModel>();
        enemy.SetEnemyType(enemies[rand.Next(enemies.Length)]);   //隨機決定敵人種類
        enemy.GetCanvasSize(canvas);
        enemy.GetPlayerInfo(playerInfo);
        playerInfo.AddElement("enemies", enemy);   //把生成的物件存進list裡面
        return enemy;
    }
}
