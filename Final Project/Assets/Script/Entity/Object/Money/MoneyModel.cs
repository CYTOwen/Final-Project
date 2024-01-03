using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyModel : ObjectModel
{
    [SerializeField]
    public MoneyScriptTable money;
    public Animator animator;

    private void Start()
    {
        animator.runtimeAnimatorController = money.Animation;
    }

    private void Update()
    {
        dropdown(gameObject);
        when_object_auto_dissapear(ref timer,gameObject);
        Debug.Log(timer);
        
    }

    public int getMoneyValue()
    {
        return money.money_value;
    }

    

    void OnMouseDown()
    {
        gameObject.SetActive(false);
    }


}
