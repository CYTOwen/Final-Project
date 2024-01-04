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
    [SerializeField]
    private PlayerInfo playerInfo;
    public override void CreateObject(float X, float Y, int level)
    {
        if (level == 1)
        {
            Instantiate(food[0], new Vector2(X, Y), Quaternion.identity, parent.transform);
        }
        else if (level == 2)
        {
            Instantiate(food[1], new Vector2(X, Y), Quaternion.identity, parent.transform);
        }
        else if (level == 3)
        {
            Instantiate(food[2], new Vector2(X, Y), Quaternion.identity, parent.transform);
        }
        else if (level == 4)
        {
            Instantiate(food[3], new Vector2(X, Y), Quaternion.identity, parent.transform);
        }
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
