using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    private float timer;
    [SerializeField]
    private List<float> enemyAppearTime;

    [SerializeField]
    private EnemyFactory enemyFactory;
    [SerializeField]
    private PlayerInfo playerInfo;

    System.Random rand = new System.Random();
    private int count;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        count = 0;
        SetEnemyAppearTime();
    }
    private void SetEnemyAppearTime()
    {
        enemyAppearTime.Add(30f);   //第一個敵人會在遊戲進行30秒時出現
        for (int i = 1; i < 30; i++)
            enemyAppearTime.Add(enemyAppearTime[i - 1] + rand.Next(20, 50));   //之後的敵人會以20到50秒的間隔出現
    }
    // Update is called once per frame
    void Update()
    {
        if (count < enemyAppearTime.Count && timer >= enemyAppearTime[count])
        {
            count++;
            enemyFactory.CreateCreature();
        }
        if (playerInfo.getMoney() < 100 && playerInfo.GetList("Fish").Count == 0)
            Lose();
        timer += Time.deltaTime;   //計時
    }
    public void Win()
    {
        if (playerInfo.getShellCount() == 3)   //檢查勝利條件
        {
            SceneManager.LoadScene(2);   //進入EndGame Scene
        }
    }
    private void Lose()
    {
        SceneManager.LoadScene(3);
    }
}
