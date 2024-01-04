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
    private float starvingTime;   //���j�ɶ�
    private float moneyCD;
    private int[] production;
    private int maturityPerLevel;

    [SerializeField]
    private MoneyFactory moneyFactory;

    private int level;
    private int healthState;   //���d���A     0�G���`   1�G���j   2�G�ͯf   3�G���`
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
        timeStamp = new float[5] { timer - turnCD, timer - searchCD, timer, timer - moneyCD, 99999999f };   //[0]�G�̪�@�����s�ɶ�   [1]�G�̪�@���j���ɶ�   [2]�G�̪�@���i���ɶ�   [3]�G�̪�@�����Ϳ����ɶ�   [4]�G���`�ɶ�
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
            if (timer - timeStamp[1] > searchCD && timer - timeStamp[2] > eatCD && target == null && healthState != 0)   //�w�ɷj��
            {
                //Debug.Log(string.Format("{0}  {1}  {2}  {3}", timer - timeStamp[1] > searchCD, timer - timeStamp[2] > eatCD, target, healthState));
                SearchTarget(playerInfo.GetList("Food"));
            }
            if (timer - timeStamp[3] > moneyCD && level >= 2)   //�w�ɥͲ�����
                Produce();
            Starve();
        }
        else
            AdjustPos();
        if (timer - timeStamp[4] > 10f)   //���`�W�L10���������
            Destroy(gameObject);
        timer += Time.deltaTime;   //�p��
    }
    private void Test()   //���եΨ禡�A�O�o���ѱ�
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
        if (timer - timeStamp[0] > AnimationTime)   //0.5��G�ਭ�ʵe�����סC�Y�ʵe�ɶ������A�ݭn�@�_���
            m_animator.SetBool("turning", false);
        if (timer - timeStamp[2] > AnimationTime)   //0.5��G�i���ʵe�����סC�Y�ʵe�ɶ������A�ݭn�@�_���
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
        starvingTime = maxStarvingTime - (timer - timeStamp[2]);   //�p�ⱺ�j�ɶ�
        if (starvingTime <= 0 && healthState != 3)   //�ھڰ��j�ɶ���s�������d���A
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
        float distance = ViewDistance;   //��l�]�������Z���A�Y�W�X�����d�򪺤��Ҽ{
        foreach (FoodModel food in targets)   //�M��̪񪺭���
        {
            if (Vector2.Distance(m_rectTransform.anchoredPosition, food.gameObject.transform.position) < distance)
            {
                distance = Vector2.Distance(m_rectTransform.anchoredPosition, food.gameObject.transform.position);
                target = food;
            }
        }
        if (level == 3 && fishMeta.FishName == "Guppy")   //3�Ū�Guppy�ٷ|���մM��P�P�Ĥ�
            SearchMedicine(playerInfo.GetList("Medicine"), distance);
    }
    private void SearchMedicine(List<EntityModel> targets, float distance)   //�M��̪��Ĥ�
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
        if (collision.tag == "Food" && healthState != 0)   //���j�����I�쭹��
        {
            EatFood(collision);
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "Medicine")   //�I��P�P�Ĥ�
        {
            TakeMedicine();
            Destroy(collision.gameObject);
        }
    }
    private void EatFood(Collider2D collision)   //�i��
    {
        timeStamp[2] = timer;
        target = null;
        FoodModel food = collision.gameObject.GetComponent<FoodModel>();
        maturity += food.getFoodValue();            
        m_animator.SetBool("eating", true);
        if(maturity>=maturityPerLevel)
            LevelUp();
    }
    private void LevelUp()   //�ɯ�
    {
        if(level<3)
            m_capsuleCollider2D.size += new Vector2(0.2f, 0.2f);   //�髬�ܤj�A�I���c�ܤj
        level = (level < fishMeta.MaxLevel && !medicine) ? level + 1 : level;
        maturity -= maturityPerLevel;
    }
    private void TakeMedicine()   //�Y��
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
    protected override void Die()  //���`�ʧ@�A�������ӷ|�s�W���Ĥ������F��
    {
        healthState = 3;
        m_animator.SetInteger("healthState", healthState);
        timeStamp[4] = timer;
        m_rigidbody2D.AddForce(m_rigidbody2D.velocity * -1);
        m_audioSource.Play();
        playerInfo.RemoveElement("Fish", this);
    }
    private void AdjustPos()   //������`�᪺��m
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
