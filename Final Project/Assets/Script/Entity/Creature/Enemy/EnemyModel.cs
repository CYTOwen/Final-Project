using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed public class EnemyModel : CreatureModel
{
    [SerializeField]
    private EnemyMeta enemyMeta;   //¼Ä¤Hªº°Ñ¼Æ
    private float attackCD;
    private bool huntingMode = false;
    [SerializeField]

    void Start()   //ªì©l¤Æ
    {
        SetVaribleValue();
    }
    public void SetEnemyType(EnemyMeta enemyType)   //³]©w¼Ä¤HºØÃþ
    {
        enemyMeta = enemyType;
        SetVaribleValue();
    }
    protected override void SetVaribleValue()   //ÅÜ¼Æªì©l¤Æ
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
        timeStamp = new float[3] { timer - turnCD, timer - searchCD, timer - attackCD };   //[0]¡G³Ìªñ¤@¦¸ÂàÅs®É¶¡   [1]¡G³Ìªñ¤@¦¸·j¯Á®É¶¡   [2]¡G³Ìªñ¤@¦¸§ðÀ»®É¶¡
        AnimationTime = 0.5f;
    }
    // Update is called once per frame
    void Update()
    {
        if (timer - timeStamp[2] > attackCD)   //§ðÀ»CD¤¤¤£·|°Ê
            Move();
        else
            m_rigidbody2D.velocity = Vector2.zero;
        SetAnimation();
        if (timer - timeStamp[1] > searchCD && target == null)   //©w®É·j¯Á
        {
            SearchTarget(playerInfo.GetList("Fish"));
        }
        timer += Time.deltaTime;   //­p®É
    }
    protected override void SearchTarget(List<EntityModel> targets)   //´M§ä§ðÀ»¥Ø¼Ðªº¼Ò¦¡
    {
        foreach (FishModel fish in targets)
        {
            RectTransform fishPos = fish.gameObject.GetComponent<RectTransform>();
            float distance = ViewDistance;   //ªì©l³]¬°µø³¥¶ZÂ÷¡A§Y¶W¥Xµø³¥½d³òªº¤£¦Ò¼{
            if (Vector2.Distance(m_rectTransform.anchoredPosition, fishPos.anchoredPosition) < distance)
            {
                distance = Vector2.Distance(m_rectTransform.anchoredPosition, fishPos.anchoredPosition);
                target = fish;
                huntingMode = true;
            }
        }
        speed = huntingMode ? enemyMeta.Speed * 1.5f : enemyMeta.Speed;   //¬¼Ây®É¥[³t
    }
    private void OnTriggerEnter2D(Collider2D collision)    //¦pªG¸I¨ì³½
    {
        if (collision.gameObject.tag == "Fish")
            Attack(collision.gameObject.GetComponent<FishModel>());
    }
    private void Attack(FishModel fish)
    {
        timeStamp[2] = timer;
        target = null;   //­«»s¥Ø¼Ð¬°µL
        huntingMode = false;
        playerInfo.RemoveElement("Fish", fish);
        m_audioSource.clip = playerInfo.audioClips[1];   //¼½©ñ«ü©w­µ®Ä
        m_audioSource.Play();
        Destroy(fish.gameObject);   //ª½±µºR·´ª«¥ó
    }
    
    private void OnMouseDown()   //ª±®a¥H·Æ¹«ÂIÀ»§ðÀ»¼Ä¤H
    {
        m_audioSource.clip = playerInfo.audioClips[0];   //¼·©ñ«ü©w­µ®Ä
        float dmg = (float)GameObject.Find("Data").GetComponent<PlayerInfo>().getMouseAtk();
        m_audioSource.Play();
        HP -= dmg;  //¼Æ­È¼È©w¡Aµ¥¹CÀ¸¨t²Î³¡¤À§¹¦¨
        if (HP <= 0)
            Die();
    }
    protected override void Die()  //¦º¤`°Ê§@¡A¤§«áÀ³¸Ó·|·s¼W­µ®Ä¤§ÃþªºªF¦è
    {
        Destroy(gameObject);
    }
}
