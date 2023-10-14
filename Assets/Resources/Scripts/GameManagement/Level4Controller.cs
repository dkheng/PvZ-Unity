using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Controller : LevelController
{
    protected override void Start()
    {
        //加载下雪效果
        Instantiate(Resources.Load<Object>("Prefabs/Effects/Weather/Snow"),
                    new Vector3(0, 4, 0),
                    Quaternion.Euler(-90, 0, 0));
    }

    public override void init()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 4,   //关卡序号
            levelName = "极地冰川",   //关卡名

            mapSuffix = "_Ice", //地图图片后缀
            rowCount = 5,       //总共几行
            landRowCount = 5,   //几行陆地
            isDay = true,       //是否白天
            plantingManagementSuffix = "_Ice",   //对应的种植管理组件后缀
            backgroundSuffix = "_Ice",   //对应背景音乐后缀

            //各行僵尸初始Y轴位置
            zombieInitPosY = new List<float> { -2.168f, -1.24f, -0.2f, 0.85f, 1.82f },

            //本关植物卡槽序列
            plantCards = new List<string>
            {
                "SunFlower",
                "PeaShooter",
                "WallNut",
                "Squash",
                "TorchWood",
                "SnowKing"
            }
        };
    }

    public override void activate()
    {
        GameObject.Find("Planting Management").AddComponent<Winter>();
    }
}
