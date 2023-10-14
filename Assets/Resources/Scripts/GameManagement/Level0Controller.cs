using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level0Controller : LevelController
{
    public override void init()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 0,   //关卡序号
            levelName = "喵喵驾到",   //关卡名

            mapSuffix = "_Day", //地图图片后缀
            rowCount = 5,       //总共几行
            landRowCount = 5,   //几行陆地
            isDay = true,       //是否白天
            plantingManagementSuffix = "_OriginalLawn",   //对应的种植管理组件后缀
            backgroundSuffix = "_Roco_PetPark",   //对应背景音乐后缀

            //各行僵尸初始Y轴位置
            zombieInitPosY = new List<float> { -2.3f, -1.25f, -0.35f, 0.7f, 1.7f },

            //本关植物卡槽序列
            plantCards = new List<string>
            {
                "SunFlower",
                "PeaShooter",
                "WallNut",
                "Squash",
                "TorchWood",
                "MiaoMiao"
            }
        };
    }
}
