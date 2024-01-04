using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFactory : CreatureFactory
{
    [SerializeField]
    private GameObject fishPrefab;
    [SerializeField]
    private GameObject parent;
    [SerializeField]
    private MoneyFactory moneyFactory;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            CreateCreature();
    }
    public override CreatureModel CreateCreature()
    {
        FishModel fish = Instantiate(fishPrefab, new Vector2(rand.Next((int)(canvas.rect.width / -3f), (int)(canvas.rect.width / 3)), rand.Next((int)(canvas.rect.height / -3f), (int)(canvas.rect.height / 3))), Quaternion.Euler(0, 0, 0), parent.transform).GetComponent<FishModel>();
        fish.GetCanvasSize(canvas);
        fish.GetPlayerInfo(playerInfo);
        fish.GetFactory(moneyFactory);
        playerInfo.AddElement("Fish", fish);
        return fish;
    }
}
