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
    private FishModel target = null;   //��w���ؼ�
    private bool huntingMode = false;
    private bool facingLeft = true;   //�Ψӱ�����V�ʵe

    [SerializeField]
    private Rigidbody2D m_rigidbody2D;   //���b�ĤH�W������
    [SerializeField]
    private RectTransform m_rectTransform;   //���b�ĤH�W��RectTransform
    [SerializeField]
    private Animator m_animator;   //���b�ĤH�W���ʵe
    [SerializeField]
    private SpriteRenderer m_spriteRenderer;   //���b�ĤH�W��SpriteRenderer

    private float timer;
    private float timeStamp;   //�ΨӰO���̪�@�����s�ɶ�
    System.Random rand = new System.Random();
    void Start()   //��l��
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
        timeStamp = timer - turnCD;
    }
    // Update is called once per frame
    void Update()
    {
        int x = rand.Next(1000);   //�H����ʡA�C�V��0.3%�����v���s
        if (target == null && ((x > 997 && timer - timeStamp > turnCD) || (m_rectTransform.anchoredPosition.x > 910f && !facingLeft) || (m_rectTransform.anchoredPosition.x < -910f && facingLeft)))   //�ָI����ɪ��ɭԱj�����s
        {
            Turn();
        }
        else 
        {
            Move();
        }
        if (timer - timeStamp > 0.5f)   //0.5��G�ਭ�ʵe�����סC�Y�ʵe�ɶ������A�ݭn�@�_���
            m_animator.SetBool("turning", false);
        timer += Time.deltaTime;
    }
    protected override void Move()
    {
        if (target != null)   //��target����
        {
            RectTransform targetPos = target.GetComponent<RectTransform>();
            m_rigidbody2D.velocity = (targetPos.anchoredPosition - m_rectTransform.anchoredPosition).normalized * speed;
        }
        else   //����
        {
            m_rigidbody2D.velocity = new Vector2(100f * ((facingLeft) ? -1f : 1f), m_rigidbody2D.velocity.y).normalized * speed;
            m_rigidbody2D.AddForce(new Vector2(0, rand.Next(-30, 31)));
        }
        Debug.Log(m_rigidbody2D.velocity);
    }
    private void Turn()
    {
        m_animator.SetBool("turning", true);
        m_spriteRenderer.flipX = facingLeft;
        facingLeft = facingLeft ? false : true;
        timeStamp = timer;
    }
    protected override void SearchTarget(List<GameObject> targets)   //�M������ؼЪ��Ҧ�
    {
        if (target == null)   //�u�b�S���J���ؼЪ��ɭԧ�
        {
            foreach (GameObject creature in targets)
            {
                FishModel fish = creature.GetComponent<FishModel>();
                RectTransform fishPos = fish.GetComponent<RectTransform>();
                float distance = ViewDistance;   //��l�]�������Z���A�Y�W�X�����d�򪺤��Ҽ{
                if (Math.Abs(fish.gameObject.transform.position.x - transform.position.x) < distance)
                {
                    distance = Vector2.Distance(m_rectTransform.anchoredPosition, fishPos.anchoredPosition);
                    target = fish;
                    huntingMode = true;
                }
            }
            speed = huntingMode ? enemyMeta.Speed * 1.5f : enemyMeta.Speed;   //���y�ɥ[�t
        }
        Debug.Log(target);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Fish")
            Attack(collision.gameObject.GetComponent<FishModel>());
    }
    private void Attack(FishModel fish)
    {
        target = null;   //���s�ؼЬ��L
        Destroy(fish.gameObject);   //�����R������
        StartCoroutine(Stop());
    }
    IEnumerator Stop()   //�b����CD�����e�R��ʡA�קK�ĤH�L����s��ñ�
    {
        yield return new WaitForSecondsRealtime(attackCD);
    }
}
