using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnOperate : MonoBehaviour
{
    [SerializeField] private PlayerInfo playerInfo;

    public void addFishBtn()
    {
        //fish factory�{��
        //playerInfo ����
        playerInfo.addFish();
    }

    public void upgradeFeedBtn()
    {
        //�ɯ�playerInfo�ư�
        playerInfo.addFoodLevel();
    }

    public void buyMedicineBtn()
    {
        //�n��ƹ��I��

    }

    public void upgradeMouseBtn()
    {
        //upgrade��playerInfo
        playerInfo.addMouseLevel();
    }

    public void buyShellBtn()
    {
        //��playerInfo
        playerInfo.addShell();
        //��Ϥ�
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
