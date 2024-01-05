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
        enemyAppearTime.Add(30f);   //�Ĥ@�ӼĤH�|�b�C���i��30��ɥX�{
        for (int i = 1; i < 30; i++)
            enemyAppearTime.Add(enemyAppearTime[i - 1] + rand.Next(20, 50));   //���᪺�ĤH�|�H20��50�����j�X�{
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
        timer += Time.deltaTime;   //�p��
    }
    public void Win()
    {
        if (playerInfo.getShellCount() == 3)   //�ˬd�ӧQ����
        {
            SceneManager.LoadScene(2);   //�i�JEndGame Scene
        }
    }
    private void Lose()
    {
        SceneManager.LoadScene(3);
    }
}
