using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureModel : EntityModel
{
    protected float HP;
    protected float speed;   //���ʳt��
    protected float ViewDistance = 480f;   //����
    protected float searchCD;   //�C�j�h�[����@��SearchTarget()
    protected float turnCD;   //�̵u���s�g��
    protected EntityModel target;
    protected bool facingLeft = true;   //�Ψӱ�����V�ʵe

    [SerializeField]
    protected RectTransform m_canvas;   //Canvas��RectTransform
    [SerializeField]
    protected Rigidbody2D m_rigidbody2D;   //���b�ͪ��W������
    [SerializeField]
    protected RectTransform m_rectTransform;   //���b�ͪ��W��RectTransform
    [SerializeField]
    protected Animator m_animator;   //���b�ͪ��W���ʵe
    [SerializeField]
    protected SpriteRenderer m_spriteRenderer;   //���b�ͪ��W��SpriteRenderer

    protected float timer;
    protected float[] timeStamp;   //�ΨӰO������ɨ�
    protected float AnimationTime;
    protected System.Random rand = new System.Random();
    public virtual void GetCanvasSize(RectTransform canvas)
    {
        m_canvas = canvas;
    }
    public virtual void GetPlayerInfo(PlayerInfo Info)
    {
        playerInfo = Info;
    }
    protected virtual void Move()
    {
        int x = rand.Next(1000);   //�H����ʡA�C�V��0.3%�����v���s
        if (target == null && ((x > 997 && timer - timeStamp[0] > turnCD) || (m_rectTransform.anchoredPosition.x > m_canvas.rect.width / 2 - 50 && !facingLeft) || (m_rectTransform.anchoredPosition.x < m_canvas.rect.width / -2 + 50 && facingLeft)))   //�ָI����ɪ��ɭԱj�����s
        {
            Turn();
        }
        else
        {
            Walk();
        }
    }
    private void Walk()
    {
        if (target != null)   //��target����
        {
            RectTransform targetPos = target.GetComponent<RectTransform>();
            m_rigidbody2D.velocity = (targetPos.anchoredPosition - m_rectTransform.anchoredPosition).normalized * speed;
        }
        else   //����
        {
            int x = rand.Next(-30, 31);
            m_rigidbody2D.velocity = new Vector2(((facingLeft) ? -1 * speed : speed), m_rigidbody2D.velocity.y);
            m_rigidbody2D.velocity += new Vector2(0, (m_rigidbody2D.velocity.y > 100f || m_rigidbody2D.velocity.y < -100f) ? m_rigidbody2D.velocity.y / -2 : rand.Next(-3, 4));
            if (m_rectTransform.anchoredPosition.y > m_canvas.rect.height / 2f - 50f)
                m_rigidbody2D.AddForce(new Vector2(0, -100f));
            if (m_rectTransform.anchoredPosition.y < m_canvas.rect.height / -2f + 50f)
                m_rigidbody2D.AddForce(new Vector2(0, 100f));
        }
        Debug.Log(Vector3.Magnitude(m_rigidbody2D.velocity));
    }
    private void Turn()
    {
        m_animator.SetBool("turning", true);
        m_spriteRenderer.flipX = facingLeft;
        facingLeft = facingLeft ? false : true;
        timeStamp[0] = timer;
    }
    protected virtual void SetAnimation()
    {
        if (timer - timeStamp[0] > AnimationTime)   //0.5��G�ਭ�ʵe�����סC�Y�ʵe�ɶ������A�ݭn�@�_���
            m_animator.SetBool("turning", false);
    }

    protected virtual void SetVaribleValue()
    {

    }
    protected virtual void SearchTarget(List<EntityModel> targets)   //�M������ؼЪ��Ҧ�
    {

    }
    protected virtual void Die()
    {

    }
    protected IEnumerator Stop(float time)   //�����
    {
        yield return new WaitForSecondsRealtime(time);
    }
}
