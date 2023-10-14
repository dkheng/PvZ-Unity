using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Controller : LevelController
{
    protected override void Start()
    {
        //������ѩЧ��
        Instantiate(Resources.Load<Object>("Prefabs/Effects/Weather/Snow"),
                    new Vector3(0, 4, 0),
                    Quaternion.Euler(-90, 0, 0));
    }

    public override void init()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 4,   //�ؿ����
            levelName = "���ر���",   //�ؿ���

            mapSuffix = "_Ice", //��ͼͼƬ��׺
            rowCount = 5,       //�ܹ�����
            landRowCount = 5,   //����½��
            isDay = true,       //�Ƿ����
            plantingManagementSuffix = "_Ice",   //��Ӧ����ֲ���������׺
            backgroundSuffix = "_Ice",   //��Ӧ�������ֺ�׺

            //���н�ʬ��ʼY��λ��
            zombieInitPosY = new List<float> { -2.168f, -1.24f, -0.2f, 0.85f, 1.82f },

            //����ֲ�￨������
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
