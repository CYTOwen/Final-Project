using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private int tank;
    private int level;
    private int petUnlock;
    private int money;
    private int shellCount;
    private int fishCount;
    private int mouseLevel;
    private int foodLevel;
    [SerializeField]
    private List<EntityModel> Fish;
    [SerializeField]
    private List<EntityModel> Enemies;
    [SerializeField]
    private List<EntityModel> Food;
    [SerializeField]
    private List<EntityModel> Medicine;
    void Start()
    {
        initialize();
    }
    public void initialize()
    {
        tank = 1;
        level = 1;
        money = 100;
        shellCount = 0;
        fishCount = 0;
        petUnlock = 0;
        mouseLevel = 1;
        foodLevel = 1;
    }

    public int getTank() { return tank; }
    public int getLevel() { return level; }
    public int getMoney() { return money; }
    public int getShellCount() { return shellCount; }
    public int getFishCount() { return fishCount; }
    public int getPetUnlock() { return petUnlock; }
    public int getMouseLevel() { return mouseLevel; }
    public int getFoodLevel() { return foodLevel; }

    public void addMoney(int earnMoney)
    {
        money += earnMoney;
    }
    public void addShell()
    {
        shellCount++;
    }
    public void addFish()
    {
        fishCount++;
        //fishes.add()
    }
    public void addPetUnlock()
    {
        petUnlock++;
    }
    public void addMouseLevel()
    {
        mouseLevel++;
    }
    public void addFoodLevel()
    {
        foodLevel++;
    }

    public void resetLevel()
    {
        money = 100;
        shellCount = 0;
        fishCount = 0;
        mouseLevel = 1;
        foodLevel = 1;
    }
    public List<EntityModel> GetList(string name)   //根據名字回傳相應的list
    {
        switch (name)
        {
            case "Fish":
                return Fish;
            case "Enemies":
                return Enemies;
            case "Food":
                return Food;
            case "Medicine":
                return Medicine;
            default:
                return null;
        }
    }
    public void AddElement(string name, EntityModel element)   //根據名字將元素加入相對應的list
    {
        switch (name)
        {
            case "Fish":
                Fish.Add(element);
                break;
            case "Enemies":
                Enemies.Add(element);
                break;
            case "Food":
                Food.Add(element);
                break;
            case "Medicine":
                Medicine.Add(element);
                break;
            default:
                return;
        }
    }
    public void RemoveElement(string name, EntityModel element)   //根據名字將元素加入相對應的list
    {
        switch (name)
        {
            case "Fish":
                Fish.Remove(element);
                break;
            case "Enemies":
                Enemies.Remove(element);
                break;
            case "Food":
                Food.Remove(element);
                break;
            case "Medicine":
                Medicine.Remove(element);
                break;
            default:
                return;
        }
    }
}
