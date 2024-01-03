using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureModel : EntityModel
{
    protected float HP;
    protected float speed;   //移動速度
    protected float ViewDistance = 480f;   //視野
    protected float searchCD;   //每隔多久執行一次SearchTarget()
    protected float turnCD;   //最短轉彎週期
    protected EntityModel target;
    protected bool facingLeft = true;   //用來控制轉向動畫


    [SerializeField]
    protected Rigidbody2D m_rigidbody2D;   //掛在生物上的鋼體
    [SerializeField]
    protected RectTransform m_rectTransform;   //掛在生物上的RectTransform
    [SerializeField]
    protected Animator m_animator;   //掛在生物上的動畫
    [SerializeField]
    protected SpriteRenderer m_spriteRenderer;   //掛在生物上的SpriteRenderer

    protected float timer;
    protected float[] timeStamp;   //用來記錄關鍵時刻
    protected float turnAnimationTime;
    protected System.Random rand = new System.Random();
    protected virtual void Move()
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
    }
    private void Walk()
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
    protected virtual void SetAnimation()
    {
        if (timer - timeStamp[0] > turnAnimationTime)   //0.5秒：轉身動畫的長度。若動畫時間有更改，需要一起改動
            m_animator.SetBool("turning", false);
    }

    protected virtual void SetVaribleValue()
    {

    }
    protected virtual void SearchTarget(List<GameObject> targets)   //尋找攻擊目標的模式
    {

    }
    protected virtual void Die()
    {

    }
}
