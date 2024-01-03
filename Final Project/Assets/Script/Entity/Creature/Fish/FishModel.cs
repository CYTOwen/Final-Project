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
        level = 0;
        healthState = 0;
        maturity = 0;
        medicine = false;
        m_animator.runtimeAnimatorController = fishMeta.Animation;
        timer = 0f;
        timeStamp = new float[4] { timer - turnCD, timer - searchCD, timer, timer - moneyCD };   //[0]：最近一次轉彎時間   [1]：最近一次搜索時間   [2]：最近一次餵食時間   [3]：最近一次產生錢的時間
        turnAnimationTime = 0.5f;
    }
    private void Update()
    {
        Move();
        SetAnimation();
        if (timer - timeStamp[1] > searchCD && timer - timeStamp[2] > eatCD && target == null && healthState != 0)   //定時搜索
        {
            //SearchTarget();
        }
        if (timer - timeStamp[3] > moneyCD && level >= 2)
            Produce();
        timer += Time.deltaTime;   //計時
        Starve();
    }
    protected override void SetAnimation()
    {
        if (timer - timeStamp[0] > turnAnimationTime)   //0.5秒：轉身動畫的長度。若動畫時間有更改，需要一起改動
            m_animator.SetBool("turning", false);
        m_animator.SetInteger("level", level);
        m_animator.SetInteger("healthState", healthState);
    }
    private void Produce()
    {
        timeStamp[3] = timer;
        /*
         * ObjectFactory moneyFactory = new MoneyFactory;
         * moneyFactory.CreateObject(m_rectTransform.anchoredPosition.x,m_rectTransform.anchoredPosition.y,production[level]);
         */
    }
    private void Starve()
    {
        starvingTime = maxStarvingTime - (timer - timeStamp[2]);
        healthState = (int)Math.Ceiling(starvingTime * 3f / maxStarvingTime);
        if (starvingTime <= 0)
            Die();
    }
    protected override void SearchTarget(List<GameObject> targets)   //檢查附近是否有食物
    {
        foreach (GameObject Object in targets)
        {
            /*
             * foodModel food =Object.GetComponent<FoodModel>();
             * RectTransform foodPos=food.tranform as RectTransform;
             * float distance = ViewDistance;   //初始設為視野距離，即超出視野範圍的不考慮
             * if (Math.Abs(Object.transform.position.x - transform.position.x) < distance)
               {
                   distance = Vector2.Distance(m_rectTransform.anchoredPosition, foodPos.anchoredPosition);
                   target = food;
               }
            */
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            EatFood();
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "Medicine")
        {
            TakeMedicine();
            Destroy(collision.gameObject);
        }
    }
    private void EatFood()
    {
        timeStamp[2] = timer;
        /*
         * FoodModel=collision.gameObject.GetComponent<FoodModel>();
         * maturity+=(float)FishModel.getFoodValue();            
         */
        if(maturity>=maturityPerLevel)
            LevelUp();
    }
    private void LevelUp()
    {
        level = (level < fishMeta.MaxLevel && !medicine) ? level + 1 : level;
        maturity -= maturityPerLevel;
    }
    private void TakeMedicine()
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
        }
    }
    protected override void Die()  //死亡動作，之後應該會新增音效之類的東西
    {
        Destroy(gameObject);
    }
}
