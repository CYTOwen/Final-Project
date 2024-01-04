using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishModel : CreatureModel
{
    [SerializeField]
    private FishMeta fishMeta;
    private float eatCD;
    private float maxStarvingTime;
    private float starvingTime;   //捱餓時間
    private float moneyCD;
    private int[] production;
    private int maturityPerLevel;

    [SerializeField]
    private MoneyFactory moneyFactory;

    private int level;
    private int healthState;   //健康狀態     0：正常   1：捱餓   2：生病   3：死亡
    private int maturity;
    private bool medicine;

    private void Start()
    {
        SetVaribleValue();
    }
    protected override void SetVaribleValue()
    {
        entityName = fishMeta.FishName;
        speed = fishMeta.Speed;
        ViewDistance = fishMeta.ViewDistance;
        searchCD = fishMeta.SearchCD;
        turnCD = fishMeta.TurnCD;
        eatCD = fishMeta.EatCD;
        maxStarvingTime = fishMeta.MaxStarvingTime;
        starvingTime = fishMeta.MaxStarvingTime;
        moneyCD = fishMeta.MoneyCD;
        production = fishMeta.Production;
        maturityPerLevel = fishMeta.MaturityPerLevel;
        level = 1;
        healthState = 0;
        maturity = 0;
        medicine = false;
        m_animator.runtimeAnimatorController = fishMeta.Animation;
        timer = 0f;
        timeStamp = new float[5] { timer - turnCD, timer - searchCD, timer, timer - moneyCD, 99999999f };   //[0]：最近一次轉彎時間   [1]：最近一次搜索時間   [2]：最近一次進食時間   [3]：最近一次產生錢的時間   [4]：死亡時間
        AnimationTime = 0.5f;
    }
    public void GetFactory(MoneyFactory factory)
    {
        moneyFactory = factory;
    }
    private void Update()
    {
        Test();
        if (healthState != 3)
        {
            Move();
            SetAnimation();
            if (timer - timeStamp[1] > searchCD && timer - timeStamp[2] > eatCD && target == null && healthState != 0)   //定時搜索
            {
                //Debug.Log(string.Format("{0}  {1}  {2}  {3}", timer - timeStamp[1] > searchCD, timer - timeStamp[2] > eatCD, target, healthState));
                SearchTarget(playerInfo.GetList("Food"));
            }
            if (timer - timeStamp[3] > moneyCD && level >= 2)   //定時生產金幣
                Produce();
            Starve();
        }
        else
            AdjustPos();
        if (timer - timeStamp[4] > 10f)   //死亡超過10秒後屍體消失
            Destroy(gameObject);
        timer += Time.deltaTime;   //計時
    }
    private void Test()   //測試用函式，記得註解掉
    {
        if (Input.GetKeyDown(KeyCode.L))
            LevelUp();
        if (Input.GetKeyDown(KeyCode.C))
        {
            /*
            timeStamp[2] = timer;
            target = null;
            maturity += 30;
            m_animator.SetBool("eating", true);
            if (maturity >= maturityPerLevel)
                LevelUp();
            */
        }
    }
    protected override void SetAnimation()
    {
        if (timer - timeStamp[0] > AnimationTime)   //0.5秒：轉身動畫的長度。若動畫時間有更改，需要一起改動
            m_animator.SetBool("turning", false);
        if (timer - timeStamp[2] > AnimationTime)   //0.5秒：進食動畫的長度。若動畫時間有更改，需要一起改動
            m_animator.SetBool("eating", false);
        m_animator.SetInteger("level", level);
        m_animator.SetInteger("healthState", healthState);
    }
    private void Produce()
    {
        timeStamp[3] = timer;
        moneyFactory.CreateObject(m_rectTransform.anchoredPosition.x, m_rectTransform.anchoredPosition.y, production[level - 1]);
    }
    private void Starve()
    {
        starvingTime = maxStarvingTime - (timer - timeStamp[2]);   //計算捱餓時間
        if (starvingTime <= 0 && healthState != 3)   //根據飢餓時間更新魚的健康狀態
            Die();
        else if (starvingTime <= maxStarvingTime / 3f)
            healthState = 2;
        else if (starvingTime <= maxStarvingTime / 3 * 2)
            healthState = 1;
        else
            healthState = 0;
    }
    protected override void SearchTarget(List<EntityModel> targets)
    {
        float distance = ViewDistance;   //初始設為視野距離，即超出視野範圍的不考慮
        foreach (FoodModel food in targets)   //尋找最近的食物
        {
            if (Vector2.Distance(m_rectTransform.anchoredPosition, food.gameObject.transform.position) < distance)
            {
                distance = Vector2.Distance(m_rectTransform.anchoredPosition, food.gameObject.transform.position);
                target = food;
            }
        }
        if (level == 3 && fishMeta.FishName == "Guppy")   //3級的Guppy還會嘗試尋找星星藥水
            SearchMedicine(playerInfo.GetList("Medicine"), distance);
    }
    private void SearchMedicine(List<EntityModel> targets, float distance)   //尋找最近的藥水
    {
        foreach (MedicineModel medicine in targets)
        {
            if (Vector2.Distance(m_rectTransform.anchoredPosition, medicine.gameObject.transform.position) < distance)
            {
                distance = Vector2.Distance(m_rectTransform.anchoredPosition, medicine.gameObject.transform.position);
                target = medicine;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food" && healthState != 0)   //飢餓的魚碰到食物
        {
            EatFood(collision);
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "Medicine")   //碰到星星藥水
        {
            TakeMedicine();
            Destroy(collision.gameObject);
        }
    }
    private void EatFood(Collider2D collision)   //進食
    {
        timeStamp[2] = timer;
        target = null;
        FoodModel food = collision.gameObject.GetComponent<FoodModel>();
        maturity += food.getFoodValue();            
        m_animator.SetBool("eating", true);
        if(maturity>=maturityPerLevel)
            LevelUp();
    }
    private void LevelUp()   //升級
    {
        if(level<3)
            m_capsuleCollider2D.size += new Vector2(0.2f, 0.2f);   //體型變大，碰撞箱變大
        level = (level < fishMeta.MaxLevel && !medicine) ? level + 1 : level;
        maturity -= maturityPerLevel;
    }
    private void TakeMedicine()   //吃藥
    {
        switch(level)
        {
            case 1:
            case 2:
                Die();
                break;
            case 3:
                medicine = true;
                break;
            default:
                break;
        }
    }
    protected override void Die()  //死亡動作，之後應該會新增音效之類的東西
    {
        healthState = 3;
        m_animator.SetInteger("healthState", healthState);
        timeStamp[4] = timer;
        m_rigidbody2D.AddForce(m_rigidbody2D.velocity * -1);
        m_audioSource.Play();
        playerInfo.RemoveElement("Fish", this);
    }
    private void AdjustPos()   //控制魚死亡後的位置
    {
        if (Math.Abs(m_rectTransform.anchoredPosition.x) >= m_canvas.rect.width / 2f)
            m_rigidbody2D.velocity = new Vector2(0, m_rigidbody2D.velocity.y);
        if (m_rectTransform.anchoredPosition.y <= m_canvas.rect.height / -2f)
        {
            m_rigidbody2D.gravityScale = 0f;
            m_rigidbody2D.velocity = Vector2.zero;
        }
        else if (m_rectTransform.anchoredPosition.y >= m_canvas.rect.height / 2f)
            m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x, 0);
    }
}
