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
    private void Update()   //���եΨ禡�A�O�o���ѱ�
    {
        if (Input.GetKeyDown(KeyCode.E))
            CreateCreature();
    }
    public override CreatureModel CreateCreature()   //�ͦ��ĤH
    {
        EnemyModel enemy = Instantiate(enemyPrefab, new Vector3((rand.Next(1, 3) * 2 - 3) * canvas.rect.width / 2f, rand.Next((int)canvas.rect.height * -1, (int)canvas.rect.height) / 2f, 0), Quaternion.Euler(0, 0, 0), parent.transform).GetComponent<EnemyModel>();
        enemy.SetEnemyType(enemies[rand.Next(enemies.Length)]);   //�H���M�w�ĤH����
        enemy.GetCanvasSize(canvas);
        enemy.GetPlayerInfo(playerInfo);
        playerInfo.AddElement("enemies", enemy);   //��ͦ�������s�ilist�̭�
        return enemy;
    }
}
