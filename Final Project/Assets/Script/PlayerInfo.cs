using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField]
    private List<EntityModel> Fish;
    [SerializeField]
    private List<EntityModel> Enemies;
    [SerializeField]
    private List<EntityModel> Food;
    [SerializeField]
    private List<EntityModel> Medicine;
    public List<AudioClip> audioClips;
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
