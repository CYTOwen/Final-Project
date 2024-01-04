using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed public class EnemyModel : CreatureModel
{
    [SerializeField]
    private EnemyMeta enemyMeta;   //�ĤH���Ѽ�
    private float attackCD;
    private bool huntingMode = false;

    void Start()   //��l��
    {
        SetVaribleValue();
    }
    public void SetEnemyType(EnemyMeta enemyType)   //�]�w�ĤH����
    {
        enemyMeta = enemyType;
        SetVaribleValue();
    }
    protected override void SetVaribleValue()   //�ܼƪ�l��
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
        timeStamp = new float[3] { timer - turnCD, timer - searchCD, timer - attackCD };   //[0]�G�̪�@�����s�ɶ�   [1]�G�̪�@���j���ɶ�   [2]�G�̪�@�������ɶ�
        AnimationTime = 0.5f;
    }
    // Update is called once per frame
    void Update()
    {
        if (timer - timeStamp[2] > attackCD)   //����CD�����|��
            Move();
        else
            m_rigidbody2D.velocity = Vector2.zero;
        SetAnimation();
        if (timer - timeStamp[1] > searchCD && target == null)   //�w�ɷj��
        {
            SearchTarget(playerInfo.GetList("Fish"));
        }
        timer += Time.deltaTime;   //�p��
    }
    protected override void SearchTarget(List<EntityModel> targets)   //�M������ؼЪ��Ҧ�
    {
        foreach (FishModel fish in targets)
        {
            RectTransform fishPos = fish.gameObject.GetComponent<RectTransform>();
            float distance = ViewDistance;   //��l�]�������Z���A�Y�W�X�����d�򪺤��Ҽ{
            if (Vector2.Distance(m_rectTransform.anchoredPosition, fishPos.anchoredPosition) < distance)
            {
                distance = Vector2.Distance(m_rectTransform.anchoredPosition, fishPos.anchoredPosition);
                target = fish;
                huntingMode = true;
            }
        }
        speed = huntingMode ? enemyMeta.Speed * 1.5f : enemyMeta.Speed;   //���y�ɥ[�t
    }
    private void OnTriggerEnter2D(Collider2D collision)    //�p�G�I�쳽
    {
        if (collision.gameObject.tag == "Fish")
            Attack(collision.gameObject.GetComponent<FishModel>());
    }
    private void Attack(FishModel fish)
    {
        timeStamp[2] = timer;
        target = null;   //���s�ؼЬ��L
        huntingMode = false;
        playerInfo.RemoveElement("Fish", fish);
        Destroy(fish.gameObject);   //�����R������
    }
    
    private void OnMouseDown()   //���a�H�ƹ��I�������ĤH
    {
        m_audioSource.Play();
        HP -= 50f;  //�ƭȼȩw�A���C���t�γ�������
        if (HP <= 0)
            Die();
    }
    protected override void Die()  //���`�ʧ@�A�������ӷ|�s�W���Ĥ������F��
    {
        Destroy(gameObject);
    }
}
