using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level0Controller : LevelController
{
    public override void init()
    {
        GameManagement.levelData = new LevelData()
        {
            level = 0,   //�ؿ����
            levelName = "�����ݵ�",   //�ؿ���

            mapSuffix = "_Day", //��ͼͼƬ��׺
            rowCount = 5,       //�ܹ�����
            landRowCount = 5,   //����½��
            isDay = true,       //�Ƿ����
            plantingManagementSuffix = "_OriginalLawn",   //��Ӧ����ֲ���������׺
            backgroundSuffix = "_Roco_PetPark",   //��Ӧ�������ֺ�׺

            //���н�ʬ��ʼY��λ��
            zombieInitPosY = new List<float> { -2.3f, -1.25f, -0.35f, 0.7f, 1.7f },

            //����ֲ�￨������
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
