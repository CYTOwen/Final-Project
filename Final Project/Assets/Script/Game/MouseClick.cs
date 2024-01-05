using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MouseClick : MonoBehaviour
{
    private bool feed = false;
    private bool buyMedicine = false;
    [SerializeField]
    FoodFactory foodFactory;
    [SerializeField]
    MedicineFactory medicineFactory;
    [SerializeField]
    PlayerInfo playerInfo;
    [SerializeField]
    TextMeshProUGUI moneyText;
    [SerializeField]
    RectTransform canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "Money: " + playerInfo.getMoney();
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            if (hit.collider != null)
            {
                feed = true;
            }
        }
        if (feedFoodOrNot())
        {
            Vector2 Pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, Input.mousePosition, gameObject.GetComponent<Canvas>().worldCamera, out Pos);
            foodFactory.CreateObject(Pos.x, Pos.y, playerInfo.getFoodLevel());
            playerInfo.minusMoney(5);
        }
        else if (feedMedicineOrNot())
        {
            Vector2 Pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, Input.mousePosition, Camera.main, out Pos);
            medicineFactory.CreateObject(Pos.x, Pos.y, 1);
            playerInfo.feedMedicine();
        }
        feed = false;
    }

    private bool feedFoodOrNot()
    {
        return Input.GetMouseButtonDown(1) && !feed && !playerInfo.getBuyMedicineOrNot() && playerInfo.getMoney() >= 5;
    }

    private bool feedMedicineOrNot()
    {
        return Input.GetMouseButtonDown(1) && !feed && playerInfo.getBuyMedicineOrNot();
    }
}
