using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FoodFactory : ObjectFactory
{
    [SerializeField]
    private List<GameObject> food;
    [SerializeField]
    private GameObject parent;
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
            CreateObject(0, 300, 1);
    }
    public override void CreateObject(float X, float Y, int level)
    {
        FoodModel f = Instantiate(food[level - 1], new Vector2(X, Y), Quaternion.identity, parent.transform).GetComponent<FoodModel>();
        //playerInfo.AddElement("Food", f);
    }
    bool feed=false;
    

    private void Update()   //測試用函式，記得註解掉
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null)
        {
                feed = true;
            }
        }
        if (Input.GetMouseButtonDown(0) && !feed)
        {
            Vector2 mouse = new Vector2((1600f/556f) * Input.mousePosition.x - 800f, (900f/312f) * Input.mousePosition.y - 450f);
            CreateObject(mouse.x, mouse.y, playerInfo.getFoodLevel());
            Debug.Log(Input.mousePosition.x + ", " + Input.mousePosition.y);
            Debug.Log(mouse.x + ", " + mouse.y);
        }
        feed= false;*/
    }

}
