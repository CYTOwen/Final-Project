using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using System.Numerics;
using System;

sealed public class EnemyModel : CreatureModel
{
    [SerializeField]
    private EnemyMeta enemyMeta;   //敵人的參數
    private float attackCD;
    private bool huntingMode = false;

    void Start()   //初始化
    {
        SetVaribleValue();
    }
    public void SetEnemyType(EnemyMeta enemyType)   //設定敵人種類
    {
        enemyMeta = enemyType;
        SetVaribleValue();
    }
    protected override void SetVaribleValue()   //變數初始化
    {
        entityName = enemyMeta.EnemyName;
        speed = enemyMeta.Speed;
        HP = enemyMeta.HP;
        attackCD = enemyMeta.AttackCD;
        ViewDistance = enemyMeta.ViewDistance;
        searchCD = enemyMeta.SearchCD;
        turnCD = enemyMeta.TrunCD;
        m_animator.runtimeAnimatorController = enemyMeta.Animation;
        timer = 0;
        timeStamp = new float[2] { timer - turnCD, timer - searchCD };   //[0]：最近一次轉彎時間   [1]：最近一次搜索時間
        turnAnimationTime = 0.5f;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        SetAnimation();
        if (timer - timeStamp[1] > searchCD && target == null)   //定時搜索
        {
            //SearchTarget();
        }
        timer += Time.deltaTime;   //計時
    }
    protected override void SearchTarget(List<GameObject> targets)   //尋找攻擊目標的模式
    {
        foreach (GameObject creature in targets)
        {
            FishModel fish = creature.GetComponent<FishModel>();
            RectTransform fishPos = fish.GetComponent<RectTransform>();
            float distance = ViewDistance;   //初始設為視野距離，即超出視野範圍的不考慮
            if (Math.Abs(creature.transform.position.x - transform.position.x) < distance)
            {
                distance = Vector2.Distance(m_rectTransform.anchoredPosition, fishPos.anchoredPosition);
                target = fish;
                huntingMode = true;
            }
        }
        speed = huntingMode ? enemyMeta.Speed * 1.5f : enemyMeta.Speed;   //狩獵時加速
    }
    private void OnTriggerEnter2D(Collider2D collision)    //如果碰到魚
    {
        if (collision.gameObject.tag == "Fish")
            Attack(collision.gameObject.GetComponent<FishModel>());
    }
    private void Attack(FishModel fish)
    {
        target = null;   //重製目標為無
        Destroy(fish.gameObject);   //直接摧毀物件
        StartCoroutine(Stop());   //攻擊後硬直
    }
    IEnumerator Stop()   //在攻擊CD結束前靜止不動，避免敵人無限制連續亂殺
    {
        yield return new WaitForSecondsRealtime(attackCD);
    }
    private void OnMouseDown()   //玩家以滑鼠點擊攻擊敵人
    {
        HP -= 50f;  //數值暫定，等遊戲系統部分完成
        if (HP <= 0)
            Die();
    }
    protected override void Die()  //死亡動作，之後應該會新增音效之類的東西
    {
        Destroy(gameObject);
    }
}
