using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnOperate : MonoBehaviour
{
    [SerializeField] private PlayerInfo playerInfo;

    public void addFishBtn()
    {
        //fish factory程式
        //playerInfo 改變
        playerInfo.addFish();
    }

    public void upgradeFeedBtn()
    {
        //升級playerInfo事務
        playerInfo.addFoodLevel();
    }

    public void buyMedicineBtn()
    {
        //要改滑鼠點擊

    }

    public void upgradeMouseBtn()
    {
        //upgrade改playerInfo
        playerInfo.addMouseLevel();
    }

    public void buyShellBtn()
    {
        //改playerInfo
        playerInfo.addShell();
        //改圖片
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
