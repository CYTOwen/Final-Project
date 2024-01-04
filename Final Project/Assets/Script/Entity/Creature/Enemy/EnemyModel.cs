using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        timeStamp = new float[3] { timer - turnCD, timer - searchCD, timer - attackCD };   //[0]：最近一次轉彎時間   [1]：最近一次搜索時間   [2]：最近一次攻擊時間
        AnimationTime = 0.5f;
    }
    // Update is called once per frame
    void Update()
    {
        if (timer - timeStamp[2] > attackCD)   //攻擊CD中不會動
            Move();
        else
            m_rigidbody2D.velocity = Vector2.zero;
        SetAnimation();
        if (timer - timeStamp[1] > searchCD && target == null)   //定時搜索
        {
            SearchTarget(playerInfo.GetList("Fish"));
        }
        timer += Time.deltaTime;   //計時
    }
    protected override void SearchTarget(List<EntityModel> targets)   //尋找攻擊目標的模式
    {
        foreach (FishModel fish in targets)
        {
            RectTransform fishPos = fish.gameObject.GetComponent<RectTransform>();
            float distance = ViewDistance;   //初始設為視野距離，即超出視野範圍的不考慮
            if (Vector2.Distance(m_rectTransform.anchoredPosition, fishPos.anchoredPosition) < distance)
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
        timeStamp[2] = timer;
        target = null;   //重製目標為無
        huntingMode = false;
        playerInfo.RemoveElement("Fish", fish);
        Destroy(fish.gameObject);   //直接摧毀物件
    }
    
    private void OnMouseDown()   //玩家以滑鼠點擊攻擊敵人
    {
        m_audioSource.Play();
        HP -= 50f;  //數值暫定，等遊戲系統部分完成
        if (HP <= 0)
            Die();
    }
    protected override void Die()  //死亡動作，之後應該會新增音效之類的東西
    {
        Destroy(gameObject);
    }
}
