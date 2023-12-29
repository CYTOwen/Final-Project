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
    private FishModel target = null;   //選定的目標
    private bool huntingMode = false;
    private bool facingLeft = true;   //用來控制轉向動畫

    [SerializeField]
    private Rigidbody2D m_rigidbody2D;   //掛在敵人上的鋼體
    [SerializeField]
    private RectTransform m_rectTransform;   //掛在敵人上的RectTransform
    [SerializeField]
    private Animator m_animator;   //掛在敵人上的動畫
    [SerializeField]
    private SpriteRenderer m_spriteRenderer;   //掛在敵人上的SpriteRenderer

    private float timer;
    private float[] timeStamp = new float[2];   //用來記錄關鍵時刻   [0]：最近一次轉彎時間  [1]：最近一次搜索時間
    System.Random rand = new System.Random();
    void Start()   //初始化
    {
        SetVaribleValue();
    }
    public void SetEnemyType(EnemyMeta enemyType)   //設定敵人種類
    {
        enemyMeta = enemyType;
        SetVaribleValue();
    }
    private void SetVaribleValue()
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
        timeStamp[0] = timer - turnCD;
        timeStamp[1] = timer - searchCD;
    }
    // Update is called once per frame
    void Update()
    {
        int x = rand.Next(1000);   //隨機行動，每幀有0.3%的機率轉彎
        if (target == null && ((x > 997 && timer - timeStamp[0] > turnCD) || (m_rectTransform.anchoredPosition.x > 910f && !facingLeft) || (m_rectTransform.anchoredPosition.x < -910f && facingLeft)))   //快碰到邊界的時候強制轉彎
        {
            Turn();
        }
        else 
        {
            Walk();
        }
        if (timer - timeStamp[0] > 0.5f)   //0.5秒：轉身動畫的長度。若動畫時間有更改，需要一起改動
            m_animator.SetBool("turning", false);
        if (timer - timeStamp[1] > searchCD && target == null)   //定時搜索
        {

        }
        timer += Time.deltaTime;
    }
    protected override void Walk()
    {
        if (target != null)   //朝target移動
        {
            RectTransform targetPos = target.GetComponent<RectTransform>();
            m_rigidbody2D.velocity = (targetPos.anchoredPosition - m_rectTransform.anchoredPosition).normalized * speed;
        }
        else   //平移
        {
            m_rigidbody2D.velocity = new Vector2(100f * ((facingLeft) ? -1f : 1f), m_rigidbody2D.velocity.y).normalized * speed;
            m_rigidbody2D.AddForce(new Vector2(0, rand.Next(-30, 31)));
        }
    }
    private void Turn()
    {
        m_animator.SetBool("turning", true);
        m_spriteRenderer.flipX = facingLeft;
        facingLeft = facingLeft ? false : true;
        timeStamp[0] = timer;
    }
    protected override void SearchTarget(List<GameObject> targets)   //尋找攻擊目標的模式
    {
        foreach (GameObject creature in targets)
        {
            FishModel fish = creature.GetComponent<FishModel>();
            RectTransform fishPos = fish.GetComponent<RectTransform>();
            float distance = ViewDistance;   //初始設為視野距離，即超出視野範圍的不考慮
            if (Math.Abs(fish.gameObject.transform.position.x - transform.position.x) < distance)
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
        StartCoroutine(Stop());
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
    private void Die()  //死亡動作，之後應該會新增音效之類的東西
    {
        Destroy(gameObject);
    }
}
