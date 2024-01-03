using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodModel : ObjectModel
{
    [SerializeField]
    public FoodScriptTable food;
    public Animator animator;

    private void Start()
    {
        animator.runtimeAnimatorController = food.Animation;
    }

    private void Update()
    {
        dropdown(gameObject);
        when_object_auto_dissapear(ref timer, gameObject);
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fish")
        {
            Destroy(gameObject);
        }
    }

    public int getFoodValue()
    {
        return food.food_value;
    }

    public int getFoodPrice()
    {
        return food.food_price;
    }
}
