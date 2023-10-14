using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Controller : LevelController
{
    protected override void Start()
    {
        GameObject.Find("Zombie Management").GetComponent<ZombieManagement>()
            .generateFunc = "generateZombies_level2";
    }

    public override void init()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 2,   //关卡序号
            levelName = "赶尸匠",   //关卡名

            mapSuffix = "_Night_Wall", //地图图片后缀
            rowCount = 5,       //总共几行
            landRowCount = 5,   //几行陆地
            isDay = false,       //是否白天
            plantingManagementSuffix = "_Wall",   //对应的种植管理组件后缀
            backgroundSuffix = "_Night_Wall",   //对应背景音乐后缀

            //各行僵尸初始Y轴位置
            zombieInitPosY = new List<float> { -2.45f, -1.5f, -0.5f, 0.55f, 1.55f },

            //本关植物卡槽序列
            plantCards = new List<string>
            {
                "SunFlower",
                "PeaShooter",
                "WallNut"
            }
        };
    }
}
