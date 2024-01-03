using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using System.Numerics;
using System;

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
        timeStamp = new float[2] { timer - turnCD, timer - searchCD };   //[0]�G�̪�@�����s�ɶ�   [1]�G�̪�@���j���ɶ�
        turnAnimationTime = 0.5f;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        SetAnimation();
        if (timer - timeStamp[1] > searchCD && target == null)   //�w�ɷj��
        {
            //SearchTarget();
        }
        timer += Time.deltaTime;   //�p��
    }
    protected override void SearchTarget(List<GameObject> targets)   //�M������ؼЪ��Ҧ�
    {
        foreach (GameObject creature in targets)
        {
            FishModel fish = creature.GetComponent<FishModel>();
            RectTransform fishPos = fish.GetComponent<RectTransform>();
            float distance = ViewDistance;   //��l�]�������Z���A�Y�W�X�����d�򪺤��Ҽ{
            if (Math.Abs(creature.transform.position.x - transform.position.x) < distance)
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
        target = null;   //���s�ؼЬ��L
        Destroy(fish.gameObject);   //�����R������
        StartCoroutine(Stop());   //������w��
    }
    IEnumerator Stop()   //�b����CD�����e�R��ʡA�קK�ĤH�L����s��ñ�
    {
        yield return new WaitForSecondsRealtime(attackCD);
    }
    private void OnMouseDown()   //���a�H�ƹ��I�������ĤH
    {
        HP -= 50f;  //�ƭȼȩw�A���C���t�γ�������
        if (HP <= 0)
            Die();
    }
    protected override void Die()  //���`�ʧ@�A�������ӷ|�s�W���Ĥ������F��
    {
        Destroy(gameObject);
    }
}
