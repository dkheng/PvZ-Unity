using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantingManagement : MonoBehaviour
{
    #region 变量

    //待种植植物相关
    public GameObject toBePlanted_Object;
    ToBePlanted toBePlanted_Script;

    Card nowCard;  //当前所选植物的卡槽的Card

    #endregion

    #region 系统消息

    // Start is called before the first frame update
    void Start()
    {
        //获取游戏对象和组件
        toBePlanted_Script = toBePlanted_Object.GetComponent<ToBePlanted>();
    }

    #endregion

    #region 私有自定义函数



    #endregion

    #region 公有自定义函数

    //点击按钮选择植物
    public void clickPlant(string plant, Card card)
    {
        nowCard = card;
        toBePlanted_Script.showPlantPreview(plant);
    }

    //种植植物
    public void plant()
    {
        GameObject.Find("Sun Text").GetComponent<SunNumber>().subSun(nowCard.sunNeeded);
        nowCard.cooling();
    }

    #endregion
}
