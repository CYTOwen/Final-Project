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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "Money: " + playerInfo.getMoney();
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null)
            {
                feed = true;
            }
        }
        if (Input.GetMouseButtonDown(0) && !feed && !playerInfo.getBuyMedicineOrNot())
        {
            Vector2 mouse = new Vector2((1600f / 556f) * Input.mousePosition.x - 800f, (900f / 312f) * Input.mousePosition.y - 450f);
            foodFactory.CreateObject(mouse.x, mouse.y, playerInfo.getFoodLevel());
            Debug.Log(Input.mousePosition.x + ", " + Input.mousePosition.y);
            Debug.Log(mouse.x + ", " + mouse.y);
        }
        else if (Input.GetMouseButtonDown(0) && !feed && playerInfo.getBuyMedicineOrNot())
        {
            Vector2 mouse = new Vector2((1600f / 556f) * Input.mousePosition.x - 800f, (900f / 312f) * Input.mousePosition.y - 450f);
            medicineFactory.CreateObject(mouse.x, mouse.y, 1);
            Debug.Log(Input.mousePosition.x + ", " + Input.mousePosition.y);
            Debug.Log(mouse.x + ", " + mouse.y);
            playerInfo.feedMedicine();
        }
        feed = false;
    }
}
