using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData
{
    public int level;  //�ؿ����
    public string levelName;   //�ؿ���

    public string mapSuffix;  //��ͼͼƬ��׺
    public int rowCount;   //�ܹ�����
    public int landRowCount;   //����½��
    public bool isDay;   //�Ƿ����
    public string plantingManagementSuffix;   //��Ӧ����ֲ���������׺
    public string backgroundSuffix;   //��Ӧ�������ֺ�׺

    public List<float> zombieInitPosY;   //���н�ʬ��ʼY��λ��

    public List<string> plantCards;   //����ֲ�￨������
}