using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    public int level;   //当前关卡序号
    private LevelController levelController;
    public static LevelData levelData;   //当前关卡数据

    public List<GameObject> awakeList;  //待唤醒列表，用于开场剧情结束后唤醒该唤醒的对象

    public GameObject endMenuPanel;   //游戏结束面板
    public GameObject background;   //背景对象
    public GameObject zombieManagement;   //僵尸管理对象
    public GameObject uiManagement;   //UI管理对象

    private void Awake()
    {
        levelController = 
            (LevelController)gameObject.AddComponent(Type.GetType("Level" + level + "Controller"));
        levelController.init();

        //加载背景图片
        background.GetComponent<SpriteRenderer>().sprite =
            Resources.Load<Sprite>("Sprites/Background/Background" + levelData.mapSuffix);
        //设置背景音乐
        background.GetComponent<BGMusicControl>()
            .changeMusic("Music" + levelData.backgroundSuffix);

        //加载对应的种植管理组件
        GameObject pm = Instantiate(
            Resources.Load<GameObject>(
                "Prefabs/PlantingManagement/PlantingManagement" + levelData.plantingManagementSuffix),
            new Vector3(0, 0, 0),
            Quaternion.Euler(0, 0, 0)
        );
        pm.name = "Planting Management";

        //加载UI
        uiManagement.GetComponent<UIManagement>().initUI();

        //加载对话面板
        Instantiate(Resources.Load<UnityEngine.Object>("Prefabs/UI/DialogPanel/DialogPanel-Level" + level),
                    new Vector3(0, 0, 0),
                    Quaternion.Euler(0, 0, 0),
                    GameObject.Find("TopCanvas").transform);
    }

    public void awakeAll()
    {
        foreach (GameObject gameObject in awakeList)
        {
            gameObject.SetActive(true);
        }
        uiManagement.GetComponent<UIManagement>().appear();
        zombieManagement.GetComponent<ZombieManagement>().activate();
        levelController.activate();
    }

    public void gameOver()
    {
        endMenuPanel.GetComponent<EndMenu>().gameOver();
    }

    public void win()
    {
        endMenuPanel.GetComponent<EndMenu>().win();
    }
}