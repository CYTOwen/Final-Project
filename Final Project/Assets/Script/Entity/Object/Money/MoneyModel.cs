using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyModel : ObjectModel
{
    [SerializeField]
    public MoneyScriptTable money;

    private void Update()
    {
        dropdown(gameObject);
        when_object_auto_dissapear(ref timer,gameObject);
    }

    public int getMoneyValue()
    {
        return money.money_value;
    }

    

    void OnMouseDown()
    {
        GameObject.Find("Data").GetComponent<PlayerInfo>().addMoney(getMoneyValue());
        Destroy(gameObject);
    }


}
