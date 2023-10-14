using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData
{
    public int level;  //关卡序号
    public string levelName;   //关卡名

    public string mapSuffix;  //地图图片后缀
    public int rowCount;   //总共几行
    public int landRowCount;   //几行陆地
    public bool isDay;   //是否白天
    public string plantingManagementSuffix;   //对应的种植管理组件后缀
    public string backgroundSuffix;   //对应背景音乐后缀

    public List<float> zombieInitPosY;   //各行僵尸初始Y轴位置

    public List<string> plantCards;   //本关植物卡槽序列
}