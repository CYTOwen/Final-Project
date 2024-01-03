using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineModel : ObjectModel
{
    [SerializeField]
    public MedicineScriptTable medicine;
    public Animator animator;
    private void Start()
    {
        animator.runtimeAnimatorController = medicine.Animation;
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


}
