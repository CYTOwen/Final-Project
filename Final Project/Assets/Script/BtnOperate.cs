using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnOperate : MonoBehaviour
{
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private FishFactory fishFactory;

    [SerializeField] private Button eggBtn;
    [SerializeField] private List<Sprite> eggBtnPic;
    [SerializeField] private Button foodBtn;
    [SerializeField] private List<Sprite> foodBtnPic;
    [SerializeField] private Button mouseBtn;
    [SerializeField] private List<Sprite> mouseBtnPic;

    public void addFishBtn()
    {
        if(playerInfo.getMoney() >= 100)
        {
            //fish factory�{��
            fishFactory.CreateCreature();
            //playerInfo ����
            playerInfo.minusMoney(100);
            //playerInfo.addFish();
        }
        
    }

    public void upgradeFeedBtn()
    {
        //�ɯ�playerInfo�ư�
        if(playerInfo.getFoodLevel() <= 3)
        {
            playerInfo.addFoodLevel();
        }
        //��Ϥ�
        if (playerInfo.getFoodLevel() == 1)
        {
            foodBtn.image.sprite = foodBtnPic[0];
        }
        else if (playerInfo.getFoodLevel() == 2)
        {
            foodBtn.image.sprite = foodBtnPic[1];
        }
        else if (playerInfo.getFoodLevel() == 3)
        {
            foodBtn.image.sprite = foodBtnPic[2];
        }
        else
        {
            foodBtn.image.sprite = foodBtnPic[3];
        }
    }

    public void buyMedicineBtn()
    {
        //�n��ƹ��I��
        playerInfo.buyMedicine();

    }

    public void upgradeMouseBtn()
    {
        if(playerInfo.getMouseAtk() <= 200)
        {
            //upgrade��playerInfo
            playerInfo.addMouseAtk();
        }

        if (playerInfo.getMouseAtk() == 50)
        {
            mouseBtn.image.sprite = mouseBtnPic[0];
        }
        else if (playerInfo.getMouseAtk() == 100)
        {
            mouseBtn.image.sprite = mouseBtnPic[1];
        }
        else if (playerInfo.getMouseAtk() == 150)
        {
            mouseBtn.image.sprite = mouseBtnPic[2];
        }
        else if (playerInfo.getMouseAtk() == 200)
        {
            mouseBtn.image.sprite = mouseBtnPic[3];
        }
        else
        {
            mouseBtn.image.sprite = mouseBtnPic[4];
        }

    }

    public void buyShellBtn()
    {
        //��playerInfo
        if(playerInfo.getShellCount() < 2)
        {
            playerInfo.addShell();
        }

        //��Ϥ�
        if (playerInfo.getShellCount() == 0)
        {
            eggBtn.image.sprite = eggBtnPic[0];
        }
        else if(playerInfo.getShellCount() == 1)
        {
            eggBtn.image.sprite = eggBtnPic[1];
        }
        else
        {
            eggBtn.image.sprite = eggBtnPic[2];
        }
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
