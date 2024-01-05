using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class ObjectModel : EntityModel
{
    //protected float orginalSpeed { get { return speed; } }
    protected float movingSpeed = 50f;
    protected float timer = 0f;
    
    protected virtual void dropdown(GameObject ob)   //object drop down
    {
        if (ob.transform.position.y > -336f)
        {
            ob.transform.Translate(movingSpeed * Time.deltaTime * Vector2.down);
        }
        
    }

    protected virtual void when_object_auto_dissapear(ref float timer,GameObject gameObject) //when object drop to bottom,exist through Fish time and dissapear
    {
        if (gameObject.transform.position.y <= -336f)
        {
            timer += Time.deltaTime;
        }
        if (timer > 5f)
        {
            try
            {
                playerInfo.RemoveElement("Food", gameObject.GetComponent<FoodModel>());
                playerInfo.RemoveElement("Medicine", gameObject.GetComponent<MedicineModel>());
            }
            catch { }
            Destroy(gameObject);
        }
    }
}
