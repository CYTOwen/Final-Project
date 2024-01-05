using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed public class EnemyModel : CreatureModel
{
    [SerializeField]
    private EnemyMeta enemyMeta;   //敵人的詳細資料
    private float attackCD;
    private bool huntingMode = false;
    [SerializeField]

    void Start()   //初始化
    {
        SetVaribleValue();
    }
    public void SetEnemyType(EnemyMeta enemyType)   //設定敵人種類
    {
        enemyMeta = enemyType;
        SetVaribleValue();
    }
    protected override void SetVaribleValue()   //初始化變數
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
        timeStamp = new float[3] { timer - turnCD, timer - searchCD, timer - attackCD };   //[0]：上次轉彎時間   [1]：上次搜尋時間   [2]：上次攻擊時間
        AnimationTime = 0.5f;
    }
    // Update is called once per frame
    void Update()
    {
        if (timer - timeStamp[2] > attackCD)   //攻擊後停止一段時間
            Move();
        else
            m_rigidbody2D.velocity = Vector2.zero;
        SetAnimation();
        if (timer - timeStamp[1] > searchCD && target == null)   //定時搜尋
        {
            SearchTarget(playerInfo.GetList("Fish"));
        }
        timer += Time.deltaTime;   //­計時
    }
    protected override void SearchTarget(List<EntityModel> targets)   //尋找魚
    {
        foreach (FishModel fish in targets)
        {
            RectTransform fishPos = fish.gameObject.GetComponent<RectTransform>();
            float distance = ViewDistance;   //大於視野距離的不會被敵人看到
            if (Vector2.Distance(m_rectTransform.anchoredPosition, fishPos.anchoredPosition) < distance && fish.GetHealthState != 3)   //不尋找死魚
            {
                distance = Vector2.Distance(m_rectTransform.anchoredPosition, fishPos.anchoredPosition);
                target = fish;
                huntingMode = true;
            }
        }
        speed = huntingMode ? enemyMeta.Speed * 1.5f : enemyMeta.Speed;   //狩獵時加速
    }
    private void OnTriggerEnter2D(Collider2D collision)    //碰到魚
    {
        if (collision.gameObject.tag == "Fish")
            Attack(collision.gameObject.GetComponent<FishModel>());
    }
    private void Attack(FishModel fish)
    {
        timeStamp[2] = timer;
        target = null;   //­重設target
        huntingMode = false;
        playerInfo.RemoveElement("Fish", fish);
        if (fish.GetHealthState != 3)
        {
            m_audioSource.clip = playerInfo.audioClips[1];   //播放特定音效
            m_audioSource.Play();
        }
        Destroy(fish.gameObject);   //摧毀魚
    }
    
    private void OnMouseDown()   //被滑鼠點擊會受傷
    {
        m_audioSource.clip = playerInfo.audioClips[0];   //¼·©ñ«ü©w­µ®Ä
        float dmg = (float)GameObject.Find("Data").GetComponent<PlayerInfo>().getMouseAtk();
        m_audioSource.Play();
        HP -= dmg;
        if (HP <= 0)
            Die();
    }
    protected override void Die()
    {
        Destroy(gameObject);
    }
}
